using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;

namespace ProEShop.Services.Services.Identity;

public class ApplicationUserManager
    : UserManager<User>, IApplicationUserManager

{
    private readonly DbSet<User> _users;
    public ApplicationUserManager(
        IApplicationUserStore store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<ApplicationUserManager> logger,
        IUnitOfWork unitOfWork)
        : base(
            (UserStore<User, Role, ApplicationDbContext, long, UserClaim, UserRole, UserLogin, UserToken,
                RoleClaim>)store,
            optionsAccessor, passwordHasher, userValidators, passwordValidators,
            keyNormalizer, errors, services, logger)
    {
        _users = unitOfWork.Set<User>();
    }



    #region CustomClass
    public async Task<DateTime?> GetSendSmsLastTimeAsync(string phonenumber)
    {
        var result = await _users.Select(x => new
        {
            x.UserName,
            x.SendSmsLastTime
        }).SingleOrDefaultAsync(x => x.UserName == phonenumber);
        return result?.SendSmsLastTime;
    }
    #endregion
}
