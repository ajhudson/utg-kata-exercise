// <copyright file="CsvHeadingMapper.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Lib.CsvReader.CustomAttributes
{
    using System;

    /// <summary>
    /// Attribute applied to properties to be mapped from a CSV file.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CsvHeadingMapper : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the heading.
        /// </summary>
        /// <value>
        /// The name of the heading.
        /// </value>
        public string HeadingName { get; set; }
    }
}
