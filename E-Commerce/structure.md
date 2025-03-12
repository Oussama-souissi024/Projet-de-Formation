# E-Commerce Project Structure

## 📂 E-Commerce (Projet Principal MVC)
├── Controllers
│   ├── AuthController.cs
│   ├── CartController.cs
│   ├── CategoryController.cs
│   ├── CouponController.cs
│   ├── OrderController.cs
│   ├── PaymentController.cs
│   └── ProductController.cs
├── Models
│   └── ViewModels
├── Views
│   ├── Auth
│   ├── Cart
│   ├── Category
│   ├── Coupon
│   ├── Order
│   ├── Payment
│   └── Product
├── wwwroot
│   ├── css
│   ├── js
│   └── images
└── Program.cs

## 📂 E-Commerce.Core
├── Common
│   ├── Constants
│   └── Enums
├── Entities
│   ├── Cart
│   │   ├── CartHeader.cs
│   │   └── CartDetails.cs
│   ├── CategoryE
│   │   └── Category.cs
│   ├── Coupon
│   │   └── Coupon.cs
│   ├── Orders
│   │   ├── OrderHeader.cs
│   │   └── OrderDetails.cs
│   ├── Products
│   │   └── Product.cs
│   └── Users
│       └── ApplicationUser.cs
├── Interfaces
│   ├── External
│   │   └── IStripePayment.cs
│   └── Repositories
│       ├── Base
│       │   └── IRepository.cs
│       ├── ICartRepository.cs
│       ├── ICategoryRepository.cs
│       ├── ICouponRepository.cs
│       ├── IOrderRepository.cs
│       └── IProductRepository.cs
└── Utility
    └── Helper classes

## 📂 E-Commerce.Infrastructure
├── Extension
│   └── InfrastructureRegistration.cs
├── External
│   └── Payments
│       └── StripePayment.cs
└── Persistence
    ├── Context
    │   └── ApplicationDbContext.cs
    └── Repositories
        ├── Base
        │   └── Repository.cs
        ├── CartRepository.cs
        ├── CategoryRepository.cs
        ├── CouponRepository.cs
        ├── OrderRepository.cs
        └── ProductRepository.cs

## 📂 E-Commerce.Application
├── Auth
│   ├── DTOs
│   │   ├── LoginRequestDto.cs
│   │   ├── LoginResponseDto.cs
│   │   └── RegistrationRequestDto.cs
│   ├── Interfaces
│   │   └── IAuthService.cs
│   └── Services
│       └── AuthService.cs
├── Cart
│   ├── DTOs
│   │   ├── CartDto.cs
│   │   └── CartDetailsDto.cs
│   ├── Interfaces
│   │   └── ICartService.cs
│   └── Services
│       └── CartService.cs
├── CategoryE
│   ├── DTOs
│   │   └── CategoryDto.cs
│   ├── Interfaces
│   │   └── ICategoryService.cs
│   └── Services
│       └── CategoryService.cs
├── Common
│   ├── Behaviors
│   ├── Exceptions
│   ├── Helpers
│   │   └── FileHelper.cs
│   ├── Mappings
│   └── ServicesRegistration
│       └── ServicesRegistration.cs
├── Coupons
│   ├── DTOs
│   │   └── CouponDto.cs
│   ├── Interfaces
│   │   └── ICouponService.cs
│   └── Services
│       └── CouponService.cs
├── Mailing
│   ├── DTOs
│   │   └── EmailDto.cs
│   ├── Interfaces
│   │   └── IMailingService.cs
│   └── Services
│       └── MailingService.cs
├── Orders
│   ├── DTOs
│   │   ├── OrderHeaderDto.cs
│   │   └── OrderDetailsDto.cs
│   ├── Interfaces
│   │   └── IOrderService.cs
│   └── Services
│       └── OrderService.cs
├── Payments
│   ├── DTOs
│   │   └── StripeRequestDto.cs
│   ├── Interfaces
│   │   └── IPaymentService.cs
│   └── Services
│       └── PaymentService.cs
├── Products
│   ├── DTOs
│   │   └── ProductDto.cs
│   ├── Interfaces
│   │   └── IProductService.cs
│   └── Services
│       └── ProductService.cs
└── Users
    ├── DTOs
    │   └── UserDto.cs
    ├── Interfaces
    │   └── IUserService.cs
    └── Services
        └── UserService.cs

## 📂 Tests
├── E-Commerce.UnitTests
│   ├── Application
│   │   ├── Services
│   │   └── Validators
│   ├── Core
│   │   └── Entities
│   └── Infrastructure
│       ├── External
│       └── Repositories
└── E-Commerce.IntegrationTests
    ├── API
    │   └── Controllers
    └── Infrastructure
        └── Persistence
