namespace SMS.Core.Dtos
{
     public class StudentModuleDto
    {
        public int Id { get; set; }
        public double Mark {get; set; }
        public int StudentId { get; set; }
        public int ModuleId { get; set; }
        public string Title { get; set; }
    }
}