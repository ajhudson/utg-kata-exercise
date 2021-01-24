// <copyright file="CsvFileResolvedEventArgs.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Console.Events
{
    using System;

    /// <summary>
    /// Event argments for when a CSV file is resolved.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class CsvFileResolvedEventArgs : EventArgs
    {
        /// <summary>Initializes a new instance of the <see cref="CsvFileResolvedEventArgs" /> class.</summary>
        /// <param name="resolvedFile">The resolved file.</param>
        public CsvFileResolvedEventArgs(string resolvedFile)
        {
            this.ResolvedFile = resolvedFile;
        }

        /// <summary>Gets or sets the resolved file.</summary>
        /// <value>The resolved file.</value>
        public string ResolvedFile { get; set; }
    }
}
