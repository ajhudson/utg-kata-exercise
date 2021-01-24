// <copyright file="AddCustomerViewModel.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Models
{
    /// <summary>Add customer view model.</summary>
    public class AddCustomerViewModel : ViewModelBase
    {
        /// <summary>Gets or sets the customer reference.</summary>
        /// <value>The customer reference.</value>
        public string CustomerRef { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>Gets or sets the address1.</summary>
        /// <value>The address1.</value>
        public string Address1 { get; set; }

        /// <summary>Gets or sets the address2.</summary>
        /// <value>The address2.</value>
        public string Address2 { get; set; }

        /// <summary>Gets or sets the town.</summary>
        /// <value>The town.</value>
        public string Town { get; set; }

        /// <summary>Gets or sets the county.</summary>
        /// <value>The county.</value>
        public string County { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>Gets or sets the post code.</summary>
        /// <value>The post code.</value>
        public string PostCode { get; set; }
    }
}
