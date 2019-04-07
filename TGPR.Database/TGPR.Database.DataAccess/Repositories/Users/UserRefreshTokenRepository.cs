using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using TGPR.Database.Authentication.Tokens;
using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Users
{
    public interface IUserRefreshTokenRepository : IRepository<UserRefreshToken>, IRepositoryAsync<UserRefreshToken>
    {
        Task DeleteExpiredAsync();
    }

    internal class UserRefreshTokenRepository : RepositoryBase<UserRefreshToken>, IUserRefreshTokenRepository
    {
        private readonly TokenOptions _tokenOptions;

        public UserRefreshTokenRepository(TgprContext context, IOptions<TokenOptions> tokenOptions)
            : base(context)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public async Task DeleteExpiredAsync()
        {
            DateTime expirationDateTime = DateTime.UtcNow.AddTicks(-1 * _tokenOptions.ValidFor.Ticks);

            List<UserRefreshToken> query = DbSet
                .Where(x => x.CreatedOn <= expirationDateTime)
                .ToList();

            foreach (var refreshToken in query)
            {
                await DeleteAsync(refreshToken);
            }
        }
    }
}
