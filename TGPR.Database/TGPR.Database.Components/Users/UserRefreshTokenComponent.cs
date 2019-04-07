using System.Threading.Tasks;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Users;

namespace TGPR.Database.Components.Users
{
    public interface IUserRefreshTokenComponent
    {
        Task AddUserRefreshTokenAsync(string userId, string refreshToken, string token, string client);
        Task<bool> HasUserRefreshTokenAsync(string refreshToken, string token, string client);
    }

    internal class UserRefreshTokenComponent : IUserRefreshTokenComponent
    {
        private readonly IRepositoryFactory _repoFactory;

        public UserRefreshTokenComponent(IRepositoryFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        public async Task AddUserRefreshTokenAsync(string userId, string refreshToken, string token, string client)
        {
            await DeleteExpiredAsync();

            using (var repo = _repoFactory.Create<IUserRefreshTokenRepository>())
            {
                UserRefreshToken entity = await repo.FindByAsync(x => x.UserId == userId
                                                                      && x.Client == client);

                if (entity == null)
                {
                    entity = new UserRefreshToken
                    {
                        UserId = userId,
                        RefreshToken = refreshToken,
                        SecurityToken = token,
                        Client = client
                    };

                    await repo.AddAsync(entity);

                    return;
                }

                await repo.DeleteAsync(entity);

                repo.SaveChanges();

                entity = new UserRefreshToken
                {
                    UserId = userId,
                    RefreshToken = refreshToken,
                    SecurityToken = token,
                    Client = client
                };

                await repo.AddAsync(entity);
            }
        }

        public async Task<bool> HasUserRefreshTokenAsync(string refreshToken, string token, string client)
        {
            await DeleteExpiredAsync();

            using (var repo = _repoFactory.Create<IUserRefreshTokenRepository>())
            {
                UserRefreshToken entity = await repo.FindByAsync(x => x.RefreshToken == refreshToken
                                                                      && x.SecurityToken == token
                                                                      && x.Client == client);

                return entity != null;
            }
        }



        private async Task DeleteExpiredAsync()
        {
            using (var repo = _repoFactory.Create<IUserRefreshTokenRepository>())
            {
                await repo.DeleteExpiredAsync();
            }
        }
    }
}
