using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseItr.Models
{
    public class FilterListViewModel
    {
        public IEnumerable<MTask> MTasks { get; set; }
        public SelectList MathTopics { get; set; }
        public string Name { get; set; }
    }
}
