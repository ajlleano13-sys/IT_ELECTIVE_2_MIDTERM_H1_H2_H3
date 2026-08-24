using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.DTOs
{
    public class AddToCartDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }
    }
}