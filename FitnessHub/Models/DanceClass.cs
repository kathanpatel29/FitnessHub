using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace FitnessHub.Models
{
    
    public class DanceClass
    {
        [Key]
        public int ClassID { get; set; }

        [ForeignKey("Studio")]
        public int StudioID { get; set; }
        public virtual Studio Studio { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }
        public DateTime Schedule { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }

    public class DanceClassDto
    {
        public int ClassID { get; set; }
        public int StudioID { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }
        public DateTime Schedule { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}