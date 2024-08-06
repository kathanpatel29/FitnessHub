using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessHub.Models
{
    public class Pool
    {
        [Key]
        public int PoolID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }  


        public virtual ICollection<SwimmingLesson> SwimmingLessons { get; set; }
    }

    public class PoolDto
    {
        public int PoolID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }  

    }
}
