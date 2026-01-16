using System.ComponentModel.DataAnnotations;

namespace ApiProjeKampi.WebApi.Dtos.ProductDtos
{
    public class CreateProductsDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
