using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Quickstart.UI;
using IdentityServer4.Services;

namespace identityserver.Services
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.FindFirst("sub").Value;
            var user = TestUsers.Users.FirstOrDefault(x => x.SubjectId == sub);
            foreach (var claim in user.Claims)
            {
                context.IssuedClaims.Add(claim);
            }
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}