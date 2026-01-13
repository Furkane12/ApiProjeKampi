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
        //ApiContext nesnesi dependency injection ile alınıyor
        //Bu sayede veritabanı işlemleri için kullanılabilir
        //readonly alan olarak tanımlanıyor
        //Bu, alanın yalnızca constructor içinde atanabileceği ve sonrasında değiştirilemeyeceği anlamına gelir
        //ApiContext sınıfından _contex isminde field tanımlanıyor
        private readonly ApiContext _context;

        //Constructor
        public CategoriesController(ApiContext context)
        {
            _context = context;
        }

        //Burada Category tablosunun verilerini Swagger ile listeliyoruz.
        [HttpGet("CategoryList")]
        public IActionResult CategoryList()
        {
            try
            {
                var values = _context.Categories.ToList();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }

        //Burada Category tablosuna swagger ile veri ekleme işlemi yaptık.
        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return Ok("Kategori Ekleme işlemi tamamlandı.");
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }

        //Burada Category tablosundaki verileri silme işlemi yaptık.
        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var value = _context.Categories.Find(id);
                // Kontrol: Eğer value null ise, yani belirtilen ID'ye sahip bir kategori bulunamazsa NotFound döndürülür.
                if (value == null)
                {
                    return NotFound("Kategori Bulunamadı.");
                }
                _context.Categories.Remove(value);
                _context.SaveChanges();
                return Ok("Kategori Silme İşlemi Yapıldı!");
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }

        //Burada Category tablosundaki verileri ID`ye göre getirme işlemi yaptık.
        //"GetCategory" yapmamın sebebi aynı türde bir Attribute (HttpGet) olduğu için isim çakışmasını önlemek.
        [HttpGet("GetCategory")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var value = _context.Categories.Find(id);
                // Kontrol: Eğer value null ise, yani belirtilen ID'ye sahip bir kategori bulunamazsa NotFound döndürülür.
                if (value == null)
                {
                    return NotFound("Kategori Bulunamadı.");
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);

            }
        }

        //Burada Category tablosundaki verileri ID`ye göre bulup, güncelleme işlemi yaptık.
        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory(Category category)
        {
            try
            {
                var value = _context.Categories.Find(category.CategoryId);
                // Kontrol: Eğer value null ise, yani belirtilen ID'ye sahip bir kategori bulunamazsa NotFound döndürülür.
                if (value == null)
                {
                    return NotFound("Kategori Bulunamadı.");
                }
                value.CategoryName = category.CategoryName;
                _context.SaveChanges();
                return Ok("Kategori Güncelleme İşlemi Tamamlandı.");
            }
            catch (Exception ex)
            {
                return BadRequest("Hata: " + ex.Message);
            }
        }
    }
}
