using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChefsController : ControllerBase
	{
		//ApiContext nesnesi dependency injection ile alınıyor
		//Bu sayede veritabanı işlemleri için kullanılabilir
		//readonly alan olarak tanımlanıyor
		//Bu, alanın yalnızca constructor içinde atanabileceği ve sonrasında değiştirilemeyeceği anlamına gelir
		//ApiContext sınıfından _contex isminde field tanımlanıyor
		private readonly ApiContext _context;

		//Constructor
		public ChefsController(ApiContext context)
		{
			_context = context;
		}

		//Burada Chef tablosunun verilerini Swagger ile listeliyoruz.
		[HttpGet("ChefList")]
		public IActionResult ChefList()
		{
			var values = _context.Chefs.ToList();
			return Ok(values);
		}
		//Burada Chef tablosuna swagger ile veri ekleme işlemi yaptık.
		[HttpPost("CreateChef")]
		public IActionResult CreateChef(Chef chefs)
		{
			_context.Chefs.Add(chefs);
			_context.SaveChanges();
			return Ok("Şef Sisteme Eklendi!");
		}
		//Burada Chef tablosundaki verileri silme işlemi yaptık.
		[HttpDelete("DeleteChef")]
		public IActionResult DeleteChef(int id)
		{
			var value = _context.Chefs.Find(id);
			_context.Chefs.Remove(value);
			_context.SaveChanges();
			return Ok("Şef Sistemden Başarıyla Silindi!");
		}
		//Burada Chef tablosundaki verileri güncelleme işlemi yaptık.
		[HttpPut("UpdateChef")]
		public IActionResult UpdateChef(Chef chefs)
		{
			var values = _context.Chefs.Find(chefs.ChefId);
			values.NameSurname = chefs.NameSurname;
			values.Title = chefs.Title;
			values.Description = chefs.Description;
			values.ImageUrl = chefs.ImageUrl;
			_context.SaveChanges();
			return Ok("Şef Sistemde Başarıyla Güncellendi!");
		}
		//Burada Chef tablosundaki verileri ID`ye göre getirme işlemi yaptık.
		[HttpGet("GetChefs")]
		public IActionResult GetChefs(int id)
		{
			var value = _context.Chefs.Find(id);
			return Ok(value);
		}
	}
}
