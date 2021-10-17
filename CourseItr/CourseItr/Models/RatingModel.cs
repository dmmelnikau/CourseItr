using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseItr.Models
{
    public class RatingModel
    {
        [Key]
        public int Id { get; set; }
        [HiddenInput]
        public int Rating { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<MTask> MTasks { get; set; } = new List<MTask>();
        public List<RatingTask> RatingTasks { get; set; } = new List<RatingTask>();
    }
}
