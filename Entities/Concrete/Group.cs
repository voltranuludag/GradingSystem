using Core.Entities;

namespace Entities.Concrete
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int GroupUserCount { get; set; }
    }
}