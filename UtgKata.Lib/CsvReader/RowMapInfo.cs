// <copyright file="RowMapInfo.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Lib.CsvReader
{
    using System;

    /// <summary>
    /// Row map info is used in the process of mapping proeprties from CSV cells/columns.
    /// </summary>
    public class RowMapInfo
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        /// <value>
        /// The type of the property.
        /// </value>
        public Type PropertyType { get; set; }
    }
}
