using Core.Entities;

namespace Entities.DTOs
{
    public class UserSelectedByOperationDto: IDto
    {
        public int UserId { get; set; }
        public int UserOperationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OperationName { get; set; }
        public bool Status { get; set; }
    }
}