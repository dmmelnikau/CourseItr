using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseItr.Models
{
    public class MTask
    {
        [Key]
        public int Id { get; set; }
        public string Condition { get; set; }
        public string Name { get; set; }
        public int? MathTopicId { get; set; }
        public MathTopic MathTopic { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Correctians { get; set; }
        public List<RatingModel> RatingModels { get; set; } = new List<RatingModel>();
        public List<RatingTask> RatingTasks { get; set; } = new List<RatingTask>();
        public string UserId { get; set; }
        public User User { get; set; }


    }
    public class MathTopic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class FileData
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string ModifiedOn { get; set; }
    }

}
