using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.FeatureDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeaturesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ApiContext _context;

		public FeaturesController(IMapper mapper, ApiContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		[HttpGet("FeaturesList")]
		public IActionResult FeaturesList()
		{
			var values = _context.Features.ToList();
			//_mapper.Map ile mapleme işlemi yaptık.
			//Bir listenin T öğesi nereden alacak ResultFeatureDto dan
			//ResultFeatureDto gelen değer ne ile maplenecek? values dan gelen değer ile.
			return Ok(_mapper.Map<List<ResultFeatureDto>>(values));
		}

		[HttpPost("CreateFeature")]
		//value den gelen verileri _mapper.Map kısmı ile Feature tipinde bir değişkene çevirdiğimiz için burada hata almıyor.
		public IActionResult CreateFeature(CreateFeatureDto createFeatureDto)
		{
			var value = _mapper.Map<Feature>(createFeatureDto);
			_context.Features.Add(value);
			_context.SaveChanges();
			return Ok("Ekleme İşlemi Başarılı!");
		}

		[HttpDelete("DeleteFeature")]
		public IActionResult DeleteFeature(int id)
		{
			try
			{
				var value = _context.Features.Find(id);
				if (value == null)
				{
					return NotFound("Değer Bulunamadı!");
				}
				_context.Features.Remove(value);
				_context.SaveChanges();
				return Ok("Değer Silindi!");
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}

		[HttpGet("GetFeature")]
		public IActionResult GetFeature(int id)
		{
			try
			{
				var value = _context.Features.Find(id);
				if (value == null)
				{
					return NotFound("Değer Bulunamadı!");
				}
				return Ok(_mapper.Map<GetByIdFeatureDto>(value));
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}

		[HttpPut("UpdateFeature")]
		public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto,int id)
		{
			try
			{
				var value = _context.Features.Find(id);
				if (value == null)
				{
					return NotFound("Değer Bulunamadı!");
				}

				_mapper.Map(updateFeatureDto, value); 
				_context.SaveChanges();

				return Ok("Güncelleme İşlemi Yapıldı!");
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}
	}
}
