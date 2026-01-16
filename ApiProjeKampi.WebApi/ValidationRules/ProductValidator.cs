using ApiProjeKampi.WebApi.Dtos.ProductDtos;
using ApiProjeKampi.WebApi.Entities;
using FluentValidation;
using AutoMapper;

namespace ApiProjeKampi.WebApi.ValidationRules
{
	//AbstractValidator sınıfından miras alacak bu sınıftan kim miras alacak? Product sınıfım.
	public class ProductValidator : AbstractValidator<CreateProductsDto>
	{
		//FluentValidation`a ait metotları kullanabilmek için constractor oluşturduk.
		public ProductValidator()
		{
			//FluentValidation ile ürettiğimiz nesneler için kural oluşturabiliriz.

			//Ürün İsmi Boş Geçilemez, eğer boş olursa şu mesajı dön.
			RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün İsmi Boş Geçilemez!");
			//Ürün ismi 2 karakterden az olamaz, olursa şu mesajı dön.
			RuleFor(x => x.ProductName).MinimumLength(2).WithMessage("Ürün İsmi 2 Karakterden Fazla olmalıdır!");
			RuleFor(x => x.ProductName).MaximumLength(50).WithMessage("Ürün İsmi 50 Karakterden Fazla olmaz!");

			RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün Fiyatı Boş Geçilemez!");
			RuleFor(X => X.Price).GreaterThan(0).WithMessage("Ürün Fiyatı Negatif Değer Olmaz!");
			RuleFor(x => x.Price).LessThan(10000).WithMessage("Ürün Fiyatı 10.000`den Yüksek Olamaz!");

			RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün Açıklaması Boş Geçilemez!");
			RuleFor(x => x.ProductDescription).MinimumLength(5).WithMessage("Ürün Açıklaması 5 Karakterden Az Olamaz!");
			RuleFor(x => x.ProductDescription).MaximumLength(100).WithMessage("Ürün Açıklaması 100 Karakterden Fazla Olamaz!");
		}
	}
}
