using AutoMapper;
using Messages;
using Products.API.Entities;

namespace Products.API
{
	public class WebProfile : Profile
	{
		public WebProfile()
		{
			CreateMap<DbProduct, Product>();
		}
	}
}
