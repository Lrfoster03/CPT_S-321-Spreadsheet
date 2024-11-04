// <copyright file="ColorChanges.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine.Changes
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorChanges"/> class.
    /// </summary>
    public class ColorChanges : IChanges
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChanges"/> class.
        /// </summary>
        /// <param name="row">The row value of our changing cell.</param>
        /// <param name="col">The column value of our changing cell.</param>
        /// <param name="color">The uint color rep of our changing cell.</param>
        public ColorChanges(int row, int col, uint color)
        {
            this.Row = row;
            this.Col = col;
            this.Color = color;
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
        /// Gets or sets our color value for our changed cell.
        /// </summary>
        public uint Color { get; set; }
    }
}
