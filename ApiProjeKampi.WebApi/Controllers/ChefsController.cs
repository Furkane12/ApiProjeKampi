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
            try
            {
                var values = _context.Chefs.ToList();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }
        //Burada Chef tablosuna swagger ile veri ekleme işlemi yaptık.
        [HttpPost("CreateChef")]
        public IActionResult CreateChef(Chef chefs)
        {
            try
            {
                _context.Chefs.Add(chefs);
                _context.SaveChanges();
                return Ok("Şef Sisteme Eklendi!");
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }
        //Burada Chef tablosundaki verileri silme işlemi yaptık.
        [HttpDelete("DeleteChef")]
        public IActionResult DeleteChef(int id)
        {
            try
            {
                var value = _context.Chefs.Find(id);
                // Kontrol: Eğer value null ise, yani belirtilen ID'ye sahip bir şef bulunamazsa NotFound döndürülür.
                if (value == null)
                {
                    return NotFound("Şef Bulunamadı!");
                }
                _context.Chefs.Remove(value);
                _context.SaveChanges();
                return Ok("Şef Sistemden Başarıyla Silindi!");
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }
        //Burada Chef tablosundaki verileri güncelleme işlemi yaptık.
        [HttpPut("UpdateChef")]
        public IActionResult UpdateChef(Chef chefs)
        {
            try
            {
                var values = _context.Chefs.Find(chefs.ChefId);
                // Kontrol: Eğer values null ise, yani belirtilen ID'ye sahip bir şef bulunamazsa NotFound döndürülür.
                if (values == null)
                {
                    return NotFound("Şef Bulunamadı!");
                }
                values.NameSurname = chefs.NameSurname;
                values.Title = chefs.Title;
                values.Description = chefs.Description;
                values.ImageUrl = chefs.ImageUrl;
                _context.SaveChanges();
                return Ok("Şef Sistemde Başarıyla Güncellendi!");
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }
        //Burada Chef tablosundaki verileri ID`ye göre getirme işlemi yaptık.
        [HttpGet("GetChefs")]
        public IActionResult GetChefs(int id)
        {
            try
            {
                var value = _context.Chefs.Find(id);
                // Kontrol: Eğer value null ise, yani belirtilen ID'ye sahip bir şef bulunamazsa NotFound döndürülür.
                if (value == null)
                {
                    return NotFound("Şef Bulunamadı!");
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }
    }
}
