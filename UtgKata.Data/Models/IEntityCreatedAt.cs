// <copyright file="IEntityCreatedAt.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Data.Models
{
    using System;

    /// <summary>
    /// Interface to be used on entities which require a datetime.
    /// </summary>
    public interface IEntityCreatedAt
    {
        /// <summary>Gets or sets the created at.</summary>
        /// <value>The created at.</value>
        public DateTime CreatedAt { get; set; }
    }
}
