using Core.Entities;

namespace Entities.Concrete
{
    public class UserSection : IEntity
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int UserId { get; set; }
    }
}