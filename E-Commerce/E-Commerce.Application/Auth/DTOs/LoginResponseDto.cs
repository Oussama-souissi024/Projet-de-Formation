namespace E_Commerce.Application.Auth.DTOs
{
    public class LoginResponseDto
    {
        // Unique identifier of the authenticated user
        // This is typically the user's ID from the database
        public string ID { get; set; }

        // User's email address
        // Used for communication and identification
        public string Email { get; set; }

        // User's display name in the application
        // Used for personalization throughout the UI
        public string UserName { get; set; }

        // User's role in the system (e.g., "Admin" or "Customer")
        // Used for authorization and access control
        public string Role { get; set; }
    }
}
