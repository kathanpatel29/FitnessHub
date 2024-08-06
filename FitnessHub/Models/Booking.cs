using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessHub.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("DanceClass")]
        public int? DanceClassID { get; set; }
        public virtual DanceClass DanceClass { get; set; }

        [ForeignKey("SwimmingLesson")]
        public int? SwimmingLessonID { get; set; }
        public virtual SwimmingLesson SwimmingLesson { get; set; }

        public DateTime BookingDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string Status { get; set; }
    }

    public class BookingDto
    {
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int? DanceClassID { get; set; }
        public int? SwimmingLessonID { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string Status { get; set; }
    }
}
