using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ApiContext _context;

		public CategoriesController(ApiContext context)
		{
			_context = context;
		}

		//Burada Category tablosunun verilerini Swagger ile listeliyoruz.
		[HttpGet("CategoryList")]
		public IActionResult CategoryList()
		{
			var values = _context.Categories.ToList();
			return Ok(values);
		}

		//Burada Category tablosuna swagger ile veri ekleme işlemi yaptık.
		[HttpPost("CreateCategory")]
		public IActionResult CreateCategory(Category category)
		{
			_context.Categories.Add(category);
			_context.SaveChanges();
			return Ok("Kategori Ekleme işlemi tamamlandı.");
		}

		//Burada Category tablosundaki verileri silme işlemi yaptık.
		[HttpDelete("DeleteCategory")]
		public IActionResult DeleteCategory(int id)
		{
			var value = _context.Categories.Find(id);
			_context.Categories.Remove(value);
			_context.SaveChanges();
			return Ok("Kategori Silme İşlemi Yapıldı!");
		}

		//Burada Category tablosundaki verileri ID`ye göre getirme işlemi yaptık.
		//"GetCategory" yapmamın sebebi aynı türde bir Attribute (HttpGet) olduğu için isim çakışmasını önlemek.
		[HttpGet("GetCategory")]
		public IActionResult GetCategory(int id)
		{
			var value = _context.Categories.Find(id);
			return Ok(value);
		}

		//Burada Category tablosundaki verileri ID`ye göre bulup, güncelleme işlemi yaptık.
		[HttpPut("UpdateCategory")]
		public IActionResult UpdateCategory(Category category)
		{
			var value = _context.Categories.Find(category.CategoryId);
			value.CategoryName = category.CategoryName;
			_context.SaveChanges();
			return Ok("Kategori Güncelleme İşlemi Tamamlandı.");
		}
	}
}
