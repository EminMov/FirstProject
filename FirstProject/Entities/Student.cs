namespace FirstProject.Entities
{
    public class Student:BaseEntity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public School School { get; set; }
        public int SchoolId { get; set; }
    }
}
