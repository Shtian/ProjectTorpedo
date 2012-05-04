using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CodeFirstMembershipSharp;

namespace ProjectTorpedo.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

    
        [ForeignKey("Owner"), Required]
        public Guid OwnerId { get; set; }
        public virtual User Owner { get; set; }

        [Required]
        public string LoanerName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("Game"), Required]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}