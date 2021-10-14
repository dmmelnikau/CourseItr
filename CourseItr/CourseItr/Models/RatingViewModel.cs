using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseItr.Models
{
    public class RatingViewModel
    {
        [Key]
        public int Id { get; set; }
        public int Rating { get; set; }
    }
}
