using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Models.DTOs
{
    public class CheckoutFormDTO
    {
        [Required(ErrorMessage = "Customer Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string CustomerName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        public string? CustomerEmail { get; set; }
    }
}