// <copyright file="CustomerViewModel.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Models
{
    using System;

    /// <summary>
    /// The customer view model.
    /// </summary>
    /// <seealso cref="UtgKata.Api.Models.AddCustomerViewModel" />
    public class CustomerViewModel : AddCustomerViewModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the created at.</summary>
        /// <value>The created at.</value>
        public DateTime CreatedAt { get; set; }
    }
}
