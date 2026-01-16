using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;
using ApiProjeKampi.WebApi.ValidationRules;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IValidator _validator;
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ProductsController(IValidator validator, ApiContext context,IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("ProductsList")]
        public IActionResult ProductsList()
        {
            try
            {
                var value = _context.Products.ToList();
                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }

        [HttpPost("CreateProducts")]
        public IActionResult CreateProducts(CreateProductsDto createProductsDto)
        {
            var ResultValidator = new ProductValidator();
            var result = ResultValidator.Validate(createProductsDto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(x => x.ErrorMessage));
            }
            else
            {
                var product = _mapper.Map<Product>(result);
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok("Ekleme Başarılı!");
            }
        }

    }


}
