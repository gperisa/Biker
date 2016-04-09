using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;
using System.Linq;

[assembly: OwinStartup(typeof(BikeGround.Tester.Startup))]

namespace BikeGround.Tester
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ////ConfigureOAuth(app);

            //WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //app.UseWebApi(config);
            ConfigureOAuth(app);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            //{
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/token"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //    Provider = new SimpleAuthorizationServerProvider()
            //};

            // Token Generation
            //app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

        public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            private readonly UserManager<User> UserManager;

            public SimpleAuthorizationServerProvider()
                : this(new UserManager<User>(new UserStore()))
            {
            }

            public SimpleAuthorizationServerProvider(UserManager<User> userManager)
            {
                UserManager = userManager;
            }

            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                var user = await UserManager.FindAsync(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                //if (context.UserName == "test" && context.Password == "test")
                //{
                //    isValidUser = true;
                //}

                //if (!isValidUser)
                //{
                //    context.SetError("invalid_grant", "The user name or password is incorrect.");
                //    return;
                //}

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);
            }
        }

        public class User : IUser
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string PasswordHash { get; set; }
            public string SecurityStamp { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }

            string IUser<string>.Id
            {
                get { return Id.ToString(); }
            }

            public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
            {
                return Task.FromResult(GenerateUserIdentity(manager));
            }

            public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
            {
                //// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                //var userIdentity = manager.CreateIdentity<User, Guid>(this, DefaultAuthenticationTypes.ApplicationCookie);
                //// Add custom user claims here
                //return userIdentity;
                return null;
            }
        }

        public class ApplicationUserManager : UserManager<User>
        {
            public ApplicationUserManager(IUserStore<User> store)
                : base(store)
            {
            }

            public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
            {
                var manager = new ApplicationUserManager(new UserStore());
                // Configure validation logic for usernames
                manager.UserValidator = new UserValidator<User>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                // Configure validation logic for passwords
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                };

                // Configure user lockout defaults
                manager.UserLockoutEnabledByDefault = true;
                manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                manager.MaxFailedAccessAttemptsBeforeLockout = 5;

                // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
                // You can write your own provider and plug it in here.
                manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User>
                {
                    MessageFormat = "Your security code is {0}"
                });
                manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is {0}"
                });
                //manager.EmailService = new EmailService();
                //manager.SmsService = new SmsService();
                var dataProtectionProvider = options.DataProtectionProvider;
                if (dataProtectionProvider != null)
                {
                    manager.UserTokenProvider =
                        new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
                }
                return manager;
            }
        }

        // Configure the application sign-in manager which is used in this application.
        public class ApplicationSignInManager : SignInManager<User, string>
        {
            public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
                : base(userManager, authenticationManager)
            {
            }

            public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
            {
                //return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
                return null;
            }

            public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
            {
                return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
            }
        }

        public class UserStore : IUserStore<User>, IUserLoginStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User> //, IUserLockoutStore<User, int>
        {
            private readonly string connectionString;

            public UserStore(string connectionString)
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new ArgumentNullException("connectionString");

                this.connectionString = connectionString;
            }

            public UserStore()
            {
                this.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }

            public void Dispose()
            {

            }

            #region IUserStore
            public virtual Task CreateAsync(User user)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                return Task.Factory.StartNew(() =>
                {
                    user.Id = Guid.NewGuid().ToString();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                        connection.Execute("insert into Users(UserId, UserName, PasswordHash, SecurityStamp) values(@userId, @userName, @passwordHash, @securityStamp)", user);
                });
            }

            public virtual Task DeleteAsync(User user)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                return Task.Factory.StartNew(() =>
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                        connection.Execute("deete from Users where UserId = @userId", new { user.Id });
                });
            }

            public virtual Task<User> FindByIdAsync(string userId)
            {
                if (string.IsNullOrWhiteSpace(userId))
                    throw new ArgumentNullException("userId");

                Guid parsedUserId;
                if (!Guid.TryParse(userId, out parsedUserId))
                    throw new ArgumentOutOfRangeException("userId", string.Format("'{0}' is not a valid GUID.", new { userId }));

                return Task.Factory.StartNew(() =>
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                        return connection.Query<User>("SELECT * FROM [AspNetUsers] WHERE [Id] = @Id", new { Id = parsedUserId }).SingleOrDefault();
                });
            }

            public virtual Task<User> FindByNameAsync(string userName)
            {
                if (string.IsNullOrWhiteSpace(userName))
                    throw new ArgumentNullException("userName");

                return Task.Factory.StartNew(() =>
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                        return connection.Query<User>("SELECT * FROM [AspNetUsers] WHERE lower(UserName) = lower(@userName)", new { userName }).SingleOrDefault();
                });
            }

            public virtual Task UpdateAsync(User user)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                return Task.Factory.StartNew(() =>
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                        connection.Execute("update Users set UserName = @userName, PasswordHash = @passwordHash, SecurityStamp = @securityStamp where UserId = @userId", user);
                });
            }
            #endregion

            #region IUserLoginStore
            public virtual Task AddLoginAsync(User user, UserLoginInfo login)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                if (login == null)
                    throw new ArgumentNullException("login");

                return Task.Factory.StartNew(() =>
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                        connection.Execute("insert into ExternalLogins(ExternalLoginId, UserId, LoginProvider, ProviderKey) values(@externalLoginId, @userId, @loginProvider, @providerKey)",
                            new { externalLoginId = Guid.NewGuid(), userId = user.Id, loginProvider = login.LoginProvider, providerKey = login.ProviderKey });
                });
            }

            public virtual Task<User> FindAsync(UserLoginInfo login)
            {
                if (login == null)
                    throw new ArgumentNullException("login");

                return Task.Factory.StartNew(() =>
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                        return connection.Query<User>("select u.* from Users u inner join ExternalLogins l on l.UserId = u.UserId where l.LoginProvider = @loginProvider and l.ProviderKey = @providerKey",
                            login).SingleOrDefault();
                });
            }

            public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                return Task.Factory.StartNew(() =>
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                        return (IList<UserLoginInfo>)connection.Query<UserLoginInfo>("select LoginProvider, ProviderKey from ExternalLogins where UserId = @userId", new { user.Id }).ToList();
                });
            }

            public virtual Task RemoveLoginAsync(User user, UserLoginInfo login)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                if (login == null)
                    throw new ArgumentNullException("login");

                return Task.Factory.StartNew(() =>
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                        connection.Execute("delete from ExternalLogins where UserId = @userId and LoginProvider = @loginProvider and ProviderKey = @providerKey",
                            new { user.Id, login.LoginProvider, login.ProviderKey });
                });
            }
            #endregion

            #region IUserPasswordStore
            public virtual Task<string> GetPasswordHashAsync(User user)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                return Task.FromResult(user.PasswordHash);
            }

            public virtual Task<bool> HasPasswordAsync(User user)
            {
                return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
            }

            public virtual Task SetPasswordHashAsync(User user, string passwordHash)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                user.PasswordHash = passwordHash;

                return Task.FromResult(0);
            }

            #endregion

            #region IUserSecurityStampStore
            public virtual Task<string> GetSecurityStampAsync(User user)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                return Task.FromResult(user.SecurityStamp);
            }

            public virtual Task SetSecurityStampAsync(User user, string stamp)
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                user.SecurityStamp = stamp;

                return Task.FromResult(0);
            }

            #endregion

            #region IUserLockoutStore

            public Task<int> GetAccessFailedCountAsync(User user)
            {
                throw new NotImplementedException();
            }

            public Task<bool> GetLockoutEnabledAsync(User user)
            {
                throw new NotImplementedException();
            }

            public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
            {
                throw new NotImplementedException();
            }

            public Task<int> IncrementAccessFailedCountAsync(User user)
            {
                throw new NotImplementedException();
            }

            public Task ResetAccessFailedCountAsync(User user)
            {
                throw new NotImplementedException();
            }

            public Task SetLockoutEnabledAsync(User user, bool enabled)
            {
                throw new NotImplementedException();
            }

            public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
            {
                throw new NotImplementedException();
            }


            public Task<User> FindByIdAsync(int userId)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
    }
}
