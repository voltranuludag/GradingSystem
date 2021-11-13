using Core.Entities;

namespace Entities.Concrete
{
    public class GroupHomework : IEntity
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int HomeworkId { get; set; }
        public int UserId { get; set; }
        public int Avarage { get; set; }
        public int ObjectionCount { get; set; }
        public int HomeworkFile { get; set; }
    }
}