using System.ComponentModel.DataAnnotations;

namespace CourseItr.Models
{
    public class RatingTask
    {
        [Key]
        public int Id { get; set; }
        public int MTaskId { get; set; }
        public MTask MTask { get; set; }

        public int RatingModelId { get; set; }
        public RatingModel RatingModel { get; set; }
    }
}
