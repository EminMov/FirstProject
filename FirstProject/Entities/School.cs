namespace FirstProject.Entities
{
    public class School:BaseEntity
    {
        public int SchoolNumber { get; set; }
        public string SchoolName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
