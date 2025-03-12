using AutoMapper;
using E_Commerce.Application.Cart.DTOs;
using E_Commerce.Application.Cart.Interfaces;
using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Application.Coupons.DTOs;
using E_Commerce.Application.Coupons.Interfaces;
using E_Commerce.Application.Products.DTOs;
using E_Commerce.Application.Products.Interfaces;
using E_Commerce.Core.Entities.Cart;
using E_Commerce.Core.Entities.Coupon;
using E_Commerce.Core.Entities.Products;
using E_Commerce.Core.Interfaces.Repositories;
using System.Collections.Generic;
using ApplicationException = E_Commerce.Application.Common.Exceptions.ApplicationException;

namespace E_Commerce.Application.Cart.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;

        public CartService(
            ICartRepository cartRepository,
            IProductService productService,
            ICouponService couponService,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productService = productService;
            _couponService = couponService;
            _mapper = mapper;
        }

        public async Task<CartDto?> GetCartByUserIdAsync(string userId)
        {
            try
            {
                var cartHeaderEntity = await _cartRepository.GetCartHeaderByUserIdAsync(userId);
                if (cartHeaderEntity == null)
                {
                    return null;
                }

                // Initialize the CartDto and map the CartHeader from the cartHeaderEntity
                CartDto cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(cartHeaderEntity)

                };

                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>
                                          (_cartRepository.GetListCartDetailsByCartHeaderIdAsync(cartHeaderEntity.Id));


                // Retrieve all products and map them to DTOs
                IEnumerable<ProductDto> productListDto = await _productService.ReadAllAsync();

                cart.CartHeader.CartTotal = 0;

                // Calculate the total for each cart detail
                foreach (var item in cart.CartDetails)
                {
                    item.Product = productListDto.FirstOrDefault(u => u.Id == item.ProductId);
                    if (item.Product != null)
                    {
                        item.Price = (double)item.Product.Price;  // Explicitly set the price from the product
                    }
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }

                // Apply a coupon if one exists
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDto couponDto = await _couponService.GetCouponByCodeAsync(cart.CartHeader.CouponCode);
                    if (couponDto != null && cart.CartHeader.CartTotal > couponDto.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= couponDto.DiscountAmount;
                        cart.CartHeader.Discount = couponDto.DiscountAmount;
                    }
                }

                // Return the final cart
                return cart;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving cart", ex);
            }
        }

        public async Task<string?> UpsertCartAsync(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _cartRepository.GetCartHeaderByUserIdAsync(cartDto.CartHeader.UserId);

                if (cartHeaderFromDb == null)
                {
                    //Create CarHeader 
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);

                    _cartRepository.AddCartHeaderAsync(cartHeader);
                    
                    cartDto.CartDetails.First().CartHeaderId = cartHeader.Id;

                    _cartRepository.AddCartDetailsAsync(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                }
                else
                {
                    //if headernot not null
                    //check if details has seem  product
                    var cartDetailsFromDb = await _cartRepository.GetCartDetailsByCartHeaderIdAndProductId(
                                                                       cartDto.CartHeader.CartHeaderId,
                                                                       cartDto.CartDetails.First().ProductId);
                    if (cartDetailsFromDb == null)
                    {
                        //Create CartDetails
                        await _cartRepository.AddCartDetailsAsync(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    }
                    else
                    {
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.Id;

                        _cartRepository.UpdateCartHeaderAsync(_mapper.Map<CartHeader>(cartDto.CartHeader));
                        _cartRepository.UpdateCartDetailsAsync(_mapper.Map<CartDetails>(cartDto.CartDetails));
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating cart", ex);
            }
        }

        public async Task<string?> ApplyCouponAsync(CartDto cartDto)
        {
            try
            {
                CartHeader? cartHeaderFromDb = await _cartRepository.GetCartHeaderByUserIdAsync(cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    throw new NotFoundException("CartHeader", cartDto.CartHeader.UserId);
                }
                cartHeaderFromDb.CouponCode = cartDto.CartHeader.CouponCode;

                _cartRepository.UpdateCartHeaderAsync(cartHeaderFromDb);

                return "";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error applying coupon", ex);
            }
        }

        public async Task<string?> RemoveFromCartAsync(Guid cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _cartRepository.GetCartDetailsByCartDetailsId(cartDetailsId);
                int totalCountofCartItem = _cartRepository.TotalCountofCartItem(cartDetailsId);
                await _cartRepository.RemoveCartDetailsAsync(cartDetails);

                if (totalCountofCartItem == 1)
                {
                    await _cartRepository.RemoveCartHeaderAsync(cartDetails.CartHeader);
                }
                return "";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error removing item from cart", ex);
            }
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            try
            {
                return await _cartRepository.ClearCartAsync(userId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error clearing cart", ex);
            }
        }

      
    }
}
