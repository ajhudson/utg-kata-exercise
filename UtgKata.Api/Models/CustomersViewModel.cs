// <copyright file="CustomersViewModel.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// The customers view model.
    /// </summary>
    /// <seealso cref="UtgKata.Api.Models.ViewModelBase" />
    public class CustomersViewModel : ViewModelBase
    {
        /// <summary>Gets or sets the customers.</summary>
        /// <value>The customers.</value>
        public List<CustomerViewModel> Customers { get; set; }
    }
}
