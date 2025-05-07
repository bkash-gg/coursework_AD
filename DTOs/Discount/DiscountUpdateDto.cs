using System.ComponentModel.DataAnnotations;

namespace AD_Coursework.DTOs.Discount
{
    public class DiscountUpdateDto
    {
        [Required]
        [Range(0.01, 100)]
        public float DiscountPercentage { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public bool SetBookOnSale { get; set; }
    }
}
