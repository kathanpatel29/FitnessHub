using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FitnessHub.Models
{
    
    public class SwimmingLesson
    {
        [Key]
        public int LessonID { get; set; }

        [ForeignKey("Pool")]
        public int PoolID { get; set; }
        public virtual Pool Pool { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }
        public DateTime Schedule { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }

    public class SwimmingLessonDto
    {
        public int LessonID { get; set; }
        public int PoolID { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }
        public DateTime Schedule { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}