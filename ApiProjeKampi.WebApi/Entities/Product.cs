using System.ComponentModel.DataAnnotations;

namespace ApiProjeKampi.WebApi.Entities
{
    public class Product
    {
        [Key]
        public int ProducyId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
