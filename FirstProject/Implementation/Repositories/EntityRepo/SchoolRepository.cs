using FirstProject.Abstractions.IRepositories.ISchoolRepo;
using FirstProject.Contexts;
using FirstProject.Entities;

namespace FirstProject.Implementation.Repositories.EntityRepo
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(ApllicationContext dbContext) : base(dbContext)
        {

        }
    }
}
