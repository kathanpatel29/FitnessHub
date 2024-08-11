using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessHub.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [ForeignKey("Booking")]
        public int BookingID { get; set; }
        public virtual Booking Booking { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; } // Changed to string for ApplicationUser
        public virtual ApplicationUser User { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class PaymentDto
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public string UserID { get; set; } // Changed to string for ApplicationUser
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
