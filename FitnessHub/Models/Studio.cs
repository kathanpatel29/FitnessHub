using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessHub.Models
{ 
public class Studio
{
    [Key]
    public int StudioID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public virtual ICollection<DanceClass> DanceClasses { get; set; }
}

public class StudioDto
{
    public int StudioID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    }
}