// <copyright file="NumberModelsFoundEventArgs.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Console.Events
{
    using System;

    /// <summary>
    ///   Base class for event arguments.
    /// </summary>
    public class NumberModelsFoundEventArgs : EventArgs
    {
        /// <summary>Initializes a new instance of the <see cref="NumberModelsFoundEventArgs" /> class.</summary>
        /// <param name="numberOfModelsFound">The number of models found.</param>
        public NumberModelsFoundEventArgs(int numberOfModelsFound)
        {
            this.NumberOfModelsFound = numberOfModelsFound;
        }

        /// <summary>Gets or sets the number of models found.</summary>
        /// <value>The number of models found.</value>
        public int NumberOfModelsFound { get; set; }
    }
}
