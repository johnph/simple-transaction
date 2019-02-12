using Transaction.WebApi.Models;
using AutoMapper;
using Transaction.Framework.Extensions;
using Transaction.Framework.Domain;
using Transaction.Framework.Types;

namespace Transaction.WebApi.Mappers
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<TransactionModel, AccountTransaction>()
                 .AfterMap<SetIdentityAction>()
                 .ForAllMembers(opts => opts.Ignore());
                 
            //.ForMember(dest => dest.Amount, opt => opt.MapFrom(o => new Money(o.Amount, o.Currency.TryParseEnum<Currency>())))
            //.ForMember(dest => dest.AccountNumber, opt => opt.Ignore())
            //.ForMember(dest => dest.TransactionType, opt => opt.Ignore());

            CreateMap<TransactionResult, TransactionResultModel>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance.Amount.ToString("N")))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(o => o.Balance.Currency.ToString())); 
        }
    }
}
