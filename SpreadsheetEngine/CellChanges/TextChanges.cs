// <copyright file="TextChanges.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine.Changes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextChanges"/> class.
    /// </summary>
    public class TextChanges : IChanges
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextChanges"/> class.
        /// </summary>
        /// <param name="row">The row value of our changing cell.</param>
        /// <param name="col">The column value of our changing cell.</param>
        /// <param name="value">The string value rep of our changing cell.</param>
        public TextChanges(int row, int col, string value)
        {
            this.Row = row;
            this.Col = col;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets our row value for our changed cell.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets our column value for our changed cell.
        /// </summary>
        public int Col { get; set; }

        /// <summary>
        /// Gets or sets our string value for our changed cell.
        /// </summary>
        public string Value { get; set; }
    }
}
