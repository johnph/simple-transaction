namespace Transaction.WebApi.Services
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using Transaction.WebApi.Models;

    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IdentityModel GetIdentity()
        {
            string authorizationHeader = _context.HttpContext.Request.Headers["Authorization"];

            if (authorizationHeader != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = authorizationHeader.Split(" ")[1];
                var paresedToken = tokenHandler.ReadJwtToken(token);

                var account = paresedToken.Claims
                    .Where(c => c.Type == "accountnumber")
                    .FirstOrDefault();

                var name = paresedToken.Claims
                    .Where(c => c.Type == "name")
                    .FirstOrDefault();

                var currency = paresedToken.Claims
                    .Where(c => c.Type == "currency")
                    .FirstOrDefault();

                return new IdentityModel() {
                    AccountNumber = Convert.ToInt32(account.Value),
                    FullName = name.Value,
                    Currency = currency.Value
                };
            }

            throw new ArgumentNullException("accountnumber");
        }
    }
}
