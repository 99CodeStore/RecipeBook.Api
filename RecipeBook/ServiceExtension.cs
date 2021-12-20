using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Data;

namespace RecipeBook
{
    public static class ServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection serviceCollection)
        {
            var builder = serviceCollection.AddIdentityCore<ApiUser>(
                x => x.User.RequireUniqueEmail = true
                );

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), serviceCollection);

            builder.AddEntityFrameworkStores<RecipeBookDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
