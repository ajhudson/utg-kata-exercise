// <copyright file="Customer.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The customer entity.
    /// </summary>
    /// <seealso cref="UtgKata.Data.Models.BaseEntity" />
    /// <seealso cref="UtgKata.Data.Models.IEntityCreatedAt" />
    public class Customer : BaseEntity, IEntityCreatedAt
    {
        /// <summary>Gets or sets the customer reference.</summary>
        /// <value>The customer reference.</value>
        [Required]
        [MaxLength(50)]
        public string CustomerRef { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>Gets or sets the address1.</summary>
        /// <value>The address1.</value>
        [MaxLength(255)]
        public string Address1 { get; set; }

        /// <summary>Gets or sets the address2.</summary>
        /// <value>The address2.</value>
        [MaxLength(255)]
        public string Address2 { get; set; }

        /// <summary>Gets or sets the town.</summary>
        /// <value>The town.</value>
        [MaxLength(50)]
        public string Town { get; set; }

        /// <summary>Gets or sets the county.</summary>
        /// <value>The county.</value>
        [MaxLength(50)]
        public string County { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        [MaxLength(50)]
        public string Country { get; set; }

        /// <summary>Gets or sets the post code.</summary>
        /// <value>The post code.</value>
        [Required]
        [MaxLength(20)]
        public string PostCode { get; set; }

        /// <summary>Gets or sets the created at.</summary>
        /// <value>The created at.</value>
        public DateTime CreatedAt { get; set; }
    }
}
