using FirstProject.Abstractions.IRepositories.IStudentRepo;
using FirstProject.Contexts;
using FirstProject.Entities;

namespace FirstProject.Implementation.Repositories.EntityRepo
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApllicationContext dbContext) : base(dbContext)
        {
        }
    }
}
