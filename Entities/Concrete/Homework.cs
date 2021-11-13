using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class Homework : IEntity
    {
        public int Id { get; set; }
        public string HomeworkName { get; set; }
        public DateTime FileUploadExDate { get; set; }
        public DateTime PointTakeExDate { get; set; }
    }
}