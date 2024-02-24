using Microsoft.AspNetCore.Identity;

namespace FirstProject.Entities.Identity
{
    public class AppUser : IdentityUser<string> // App user cedveline code terefinden elimiz chatsin deye Identiy userdan toredirik. Type <string> qeyd etdik chunki primary key string tipindedir
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndTime { get; set; }
        
        public class AppRole : IdentityRole<string> // Ident
        {

        }

        public class AppUserRoles : IdentityUserRole<string>
        {

        }
    }
}
