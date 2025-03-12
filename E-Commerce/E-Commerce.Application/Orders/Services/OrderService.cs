using AutoMapper;
using E_Commerce.Application.Cart.DTOs;
using E_Commerce.Application.Orders.DTOs;
using E_Commerce.Application.Orders.Interfaces;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Entities.Orders;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Utility;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Application.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderHeaderDto?> CreateOrderAsync(CartDto cartDto)
        {
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);

                orderHeaderDto.OrderTime = DateTime.Now;
                orderHeaderDto.Status = StaticDetails.Status_Pending;
                orderHeaderDto.OrderTotal = Math.Round(orderHeaderDto.OrderTotal, 2);

                OrderHeader orderHeader = _mapper.Map<OrderHeader>(orderHeaderDto);

                await _orderRepository.AddOrderHeaderAsync(orderHeader);

                orderHeaderDto.OrderHeaderId = orderHeader.Id;

                OrderDetailsDto orderDetailsDto = _mapper.Map<OrderDetailsDto>(cartDto.CartDetails);
                OrderDetails orderDetails = _mapper.Map<OrderDetails>(orderDetailsDto);

                await _orderRepository.AddOrderDetailsAsync(orderDetails);

                return orderHeaderDto;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in CreateOrderAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<OrderHeaderDto?>> GetAllOrderAsync(ClaimsPrincipal claimUser)
        {
            try
            {
                IEnumerable<OrderHeader> orderList;
                var user = await _signInManager.UserManager.GetUserAsync(claimUser);
                if (user == null)
                {
                    throw new UnauthorizedAccessException("User not found");
                }

                IEnumerable<OrderHeader> objList;
                if (await _signInManager.UserManager.IsInRoleAsync(user, StaticDetails.RoleAdmin))
                {
                    orderList = await _orderRepository.GetAllAsync();
                }
                else
                {
                    orderList = await _orderRepository.GetAllByUserIdAsync(user.Id);
                }

                return _mapper.Map<IEnumerable<OrderHeaderDto>>(orderList);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving orders: {ex.Message}", ex);
            }
        }

        public async Task<OrderHeaderDto> GetOrderByIdAsync(Guid orderHeaderId)
        {
            try
            {
                OrderHeader orderHeader = await _orderRepository.GetByIdAsync(orderHeaderId);
                return orderHeader != null ? _mapper.Map<OrderHeaderDto>(orderHeader) : null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving order {orderHeaderId}: {ex.Message}", ex);
            }
        }

        public async Task<string?> UpdateOrderStatusAsync(Guid orderHeaderId, string newStatus)
        {
            return await _orderRepository.UpdateStatusAsync(orderHeaderId, newStatus);
        }
    }
}
