using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UtgKata.Data.Models
{
    public class Customer : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string CustomerRef { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string Address1 { get; set; }

        [MaxLength(255)]
        public string Address2 { get; set; }

        [MaxLength(50)]
        public string Town { get; set; }

        [MaxLength(50)]
        public string County { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MaxLength(20)]
        public string PostCode { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
