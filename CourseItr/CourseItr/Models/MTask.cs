using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public string? UserId { get; set; }
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
