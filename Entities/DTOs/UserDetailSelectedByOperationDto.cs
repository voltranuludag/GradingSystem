using Castle.DynamicProxy.Generators.Emitters;
using Core.Entities;

namespace Entities.DTOs
{
    public class UserDetailSelectedByOperationDto: IDto
    {
        public int UserId { get; set; }
        public int UserOperationId { get; set; }
        public int SectionId { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string SectionName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OperationName { get; set; }
        public bool Status { get; set; }
    }
}