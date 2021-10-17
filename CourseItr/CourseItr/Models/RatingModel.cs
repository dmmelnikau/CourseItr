using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseItr.Models
{
    public class RatingModel
    {
        [Key]
        public int Id { get; set; }
        [HiddenInput]
        public int Rating{ get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<MTask>  MTasks { get; set; } = new List<MTask>();
    }
}
