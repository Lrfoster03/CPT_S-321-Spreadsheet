// <copyright file="InstantiatedCell.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstantiatedCell"/> class.
    /// </summary>
    public class InstantiatedCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstantiatedCell"/> class.
        /// </summary>
        /// <param name="rowIndex">Our row index for our cell.</param>
        /// <param name="columnIndex">Our column index for our cell.</param>
        public InstantiatedCell(int rowIndex, int columnIndex)
            : base(rowIndex, columnIndex)
        {
        }
    }
}
