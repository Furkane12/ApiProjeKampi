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
        private readonly IValidator<CreateProductsDto> _validator;
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ProductsController(IValidator<CreateProductsDto> validator, ApiContext context, IMapper mapper)
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
                return Ok(_mapper.Map<List<ResultProductsDto>>(value));
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }

        [HttpPost("CreateProducts")]
        public IActionResult CreateProducts(CreateProductsDto createProductsDto)
        {
            var ResultValidator = _validator.Validate(createProductsDto);
            //var ResultValidator = new ProductValidator();
            //var result = ResultValidator.Validate(createProductsDto);

            if (!ResultValidator.IsValid)
            {
                return BadRequest(ResultValidator.Errors.Select(x => x.ErrorMessage));
            }
            else
            {
                var product = _mapper.Map<Product>(createProductsDto);
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok("Ekleme Başarılı!");
            }
        }

        [HttpDelete("DeleteProducts")]
        public IActionResult DeleteProducts(int id)
        {
            try
            {
                var value = _context.Products.Find(id);
                if (value == null)
                {
                    return NotFound("Ürün Bulunamadı!");
                }
                _context.Products.Remove(value);
                _context.SaveChanges();
                return Ok("Ürün Silindi!");
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }

        [HttpPut("UpdateProducts")]
        public IActionResult UpdateProducts(UpdateProductsDto updateProductsDto, int id)
        {
            try
            {
                var value = _context.Products.Find(id);
                if (value == null)
                {
                    return NotFound("Ürün Bulunamadı!");
                }
                _mapper.Map(updateProductsDto, value);
                _context.SaveChanges();
                return Ok("Ürün Güncellendi!");
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts(int id)
        {
            try
            {
                var value = _context.Products.Find(id);
                if (value == null)
                {
                    return NotFound("Ürün Bulunamadı!");
                }
               var dto = _mapper.Map<GetByIdProductsDto>(value);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }

    }
}
