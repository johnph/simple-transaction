namespace Transaction.Framework.Mappers
{
    using AutoMapper;
    using System;
    using Transaction.Framework.Data.Entities;
    using Transaction.Framework.Extensions;
    using Transaction.Framework.Domain;
    using Transaction.Framework.Types;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountSummaryEntity, AccountSummary>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => new Money(o.Balance, o.Currency.TryParseEnum<Currency>())));

            CreateMap<AccountTransaction, AccountTransactionEntity>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(o => DateTime.UtcNow))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(o => o.TransactionType == TransactionType.Deposit ? StringResources.DepositDescription : StringResources.WithdrawDescription))
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(o => o.TransactionType.ToString()))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(o => o.Amount.Amount));

            CreateMap<AccountSummary, AccountSummaryEntity>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(o => o.Balance.Currency.ToString()))
                .ForMember(dest => dest.AccountTransactions, opt => opt.Ignore());

            CreateMap<AccountTransactionEntity, TransactionResult>()
                .ForMember(dest => dest.IsSuccessful, opt => opt.MapFrom(o => o.TransactionId > 0))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(o => o.TransactionId > 0 ? StringResources.TransactionSuccessfull : StringResources.TransactionFailed))
                .ForMember(dest => dest.Balance, opt => opt.Ignore());
            
            CreateMap<AccountSummary, TransactionResult>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance))
                .ForMember(dest => dest.IsSuccessful, opt => opt.MapFrom(o => true))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(o => StringResources.TransactionSuccessfull)); 
        }
    }
}
