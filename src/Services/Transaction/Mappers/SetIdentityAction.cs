using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Framework.Domain;
using Transaction.Framework.Types;
using Transaction.WebApi.Models;
using Transaction.WebApi.Services;
using Transaction.Framework.Extensions;

namespace Transaction.WebApi.Mappers
{
    public class SetIdentityAction : IMappingAction<TransactionModel, AccountTransaction>
    {
        private readonly IIdentityService _identityService;

        public SetIdentityAction(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public void Process(TransactionModel source, AccountTransaction destination)
        {
            var identity = _identityService.GetIdentity();

            destination.AccountNumber = identity.AccountNumber;
            destination.Amount = new Money(source.Amount, identity.Currency.TryParseEnum<Currency>());
        }
    }
}
