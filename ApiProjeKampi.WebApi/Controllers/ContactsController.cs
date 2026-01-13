using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.ContactDtos;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController : ControllerBase
	{
		//ApiContext nesnesi dependency injection ile alınıyor
		//Bu sayede veritabanı işlemleri için kullanılabilir
		//readonly alan olarak tanımlanıyor
		//Bu, alanın yalnızca constructor içinde atanabileceği ve sonrasında değiştirilemeyeceği anlamına gelir
		//ApiContext sınıfından _contex isminde field tanımlanıyor
		public readonly ApiContext _context;

		//Constructor
		public ContactsController(ApiContext context)
		{
			_context = context;
		}

		[HttpGet("ContactList")]
		public IActionResult ContactList()
		{
			try
			{
				var values = _context.Contacts.ToList();
				return Ok(values);
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}

		[HttpPost("CreateContact")]
		public IActionResult CreateContact(CreateContactDto createContactDto)
		{
			//CreateContactDto tipinde bir parametre alınıyor
			//Dto'dan gelen verilerle yeni bir Contact nesnesi oluşturuluyor
			//AutoMapper kullanılmadan manuel olarak alanlar atanıyor
			try
			{
				Contact contact = new Contact();
				contact.Email = createContactDto.Email;
				contact.Adress = createContactDto.Adress;
				contact.Phone = createContactDto.Phone;
				contact.MapLocation = createContactDto.MapLocation;
				contact.OpenHours = createContactDto.OpenHours;
				_context.Contacts.Add(contact);
				_context.SaveChanges();
				return Ok("Ekleme İşlemi Başarılı!");
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}


		[HttpDelete("DeleteContact")]
		//id parametresi ile silinecek Contact kaydının belirlenmesi
		public IActionResult DeleteContact(int id)
		{
			try
			{
				var values = _context.Contacts.Find(id);
				if (values == null)
				{
					return NotFound("Değer Bulunumadı!");
				}
				_context.Contacts.Remove(values);
				_context.SaveChanges();
				return Ok("Silme İşlemi Başarılı!");
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}

		[HttpGet("GetContact")]
		//id parametresi ile getirilecek Contact kaydının belirlenmesi
		public IActionResult GetContact(int id)
		{
			try
			{
				var values = _context.Contacts.Find(id);
				if (values == null)
				{
					return NotFound("Değer Bulunamadı!");
				}
				return Ok(values);
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}

		[HttpPut("UpdateContact")]
		//UpdateContactDto tipinde bir parametre alınıyor
		//Dto'dan gelen verilerle mevcut bir Contact nesnesi güncelleniyor
		public IActionResult UpdateContact(UpdateContactDto updateContactDto)
		{
			try
			{
				Contact contact = new Contact();
				var values = _context.Contacts.Find(contact.ContactId);

				if (values == null)
				{
					return NotFound("Değer Bulunamadı!");
				}
				contact.ContactId = updateContactDto.ContactId;
				contact.Email = updateContactDto.Email;
				contact.Adress = updateContactDto.Adress;
				contact.Phone = updateContactDto.Phone;
				contact.MapLocation = updateContactDto.MapLocation;
				contact.OpenHours = updateContactDto.OpenHours;
				_context.Contacts.Update(contact);
				_context.SaveChanges();
				return Ok("Güncelleme İşlemi Başarılı!");

			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}
	}
}
