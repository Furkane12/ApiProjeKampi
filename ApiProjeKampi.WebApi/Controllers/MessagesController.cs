using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.MessageDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MessagesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ApiContext _context;

		public MessagesController(IMapper mapper, ApiContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		[HttpGet("MessageList")]
		public IActionResult MessageList()
		{
			try
			{
				var value = _context.Messages.ToList();
				return Ok(_mapper.Map<List<ResultMessageDto>>(value));
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}

		[HttpPost("CreateMessage")]
		public IActionResult CreateMessage(CreateMessageDto createMessageDto)
		{
			try
			{
				var value = _mapper.Map<Message>(createMessageDto);
				_context.Messages.Add(value);
				_context.SaveChanges();
				return Ok("Mesaj Eklendi!");
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}

		[HttpDelete("DeleteMessage")]
		public IActionResult DeleteMessage(int id)
		{
			try
			{
				var value = _context.Messages.Find(id);
				if (value == null)
				{
					return NotFound("Değer Bulunamadı!");
				}
				_context.Messages.Remove(value);
				_context.SaveChanges();
				return Ok("Mesaj Silindi!");
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}

		[HttpGet("GetMessage")]
		public IActionResult GetMessage(int id)
		{
			try
			{
				var value = _context.Messages.Find(id);
				if (value == null)
				{
					return NotFound("Mesaj Bulunamadı!");
				}
				return Ok(_mapper.Map<GetByIdMessageDto>(value));
			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}

		[HttpPut("UpdateMessage")]
		public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto, int id)
		{
			try
			{
				var value = _context.Messages.Find(id);
				if (value == null)
				{
					return NotFound("Mesaj Bulunamadı!");
				}
				_mapper.Map(updateMessageDto, value);
				_context.SaveChanges();
				return Ok("Mesaj Güncellendi!");

			}
			catch (Exception ex)
			{
				return BadRequest("Hata: " + ex.Message);
			}
		}
	}
}
