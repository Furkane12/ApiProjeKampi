using ApiProjeKampi.WebApi.Dtos.FeatureDtos;
using ApiProjeKampi.WebApi.Dtos.MessageDtos;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using FluentValidation;

namespace ApiProjeKampi.WebApi.Mapping
{
	public class GeneralMapping : Profile
	{
		public GeneralMapping()
		{
			CreateMap<Feature, ResultFeatureDto>().ReverseMap();
			CreateMap<Feature, CreateFeatureDto>().ReverseMap();
			CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
			CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();

			CreateMap<Message, ResultMessageDto>().ReverseMap();
			CreateMap<Message, CreateMessageDto>().ReverseMap();
			CreateMap<Message, UpdateMessageDto>().ReverseMap();
			CreateMap<Message, GetByIdMessageDto>().ReverseMap();

			CreateMap<Product, ResultProductsDto>().ReverseMap();
			CreateMap<Product,CreateProductsDto>().ReverseMap();
			CreateMap<Product, UpdateProductsDto>().ReverseMap();
			CreateMap<Product,GetByIdProductsDto>().ReverseMap();
		}
	}
}
