using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseItr.Models
{
    public class MathTask
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MathTopicId { get; set; }
        public MathTopic MathTopic { get; set; }
    }
    public class MathTopic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
   
}
