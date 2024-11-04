// <copyright file="Cell.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// Our Cell Class.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Our value string that stores what our user actually entered.
        /// </summary>
        protected internal string Value = string.Empty;

        private uint BGColor = 1;

        // Helps us with communicating with spreadsheet / other cells.
        private string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Our text string to store our evaluated text.
        /// </summary>
        private string text = string.Empty;

        /// <summary>
        /// The row index of our cell.
        /// </summary>
        private readonly int rowIndex;

        /// <summary>
        /// The column index of our cell.
        /// </summary>
        private readonly int columnIndex;

        /// <summary>
        /// The name of our cell.
        /// </summary>
        private readonly string cellName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex"> An int index for the specific row our cell is.</param>
        /// <param name="columnIndex"> An int index for the specific column our cell is.</param>
        public Cell(int rowIndex, int columnIndex)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.cellName = this.alpha.Substring(this.GetColIndex(), 1) + (this.GetRowIndex() + 1);
        }

        /// <inheritdoc/>.
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Our GetColIndex method.
        /// </summary>
        /// <returns>Row Index.</returns>
        public int GetRowIndex()
        {
            return this.rowIndex;
        }

        /// <summary>
        /// Our GetColIndex method.
        /// </summary>
        /// <returns>Column Index.</returns>
        public int GetColIndex()
        {
            return this.columnIndex;
        }

        /// <summary>
        /// Our GetText method.
        /// </summary>
        /// <returns>String text.</returns>
        public string GetText()
        {
            return this.text;
        }

        /// <summary>
        /// Our GetValue method.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetValue()
        {
            return this.Value;
        }

        /// <summary>
        /// Our GetColor method.
        /// </summary>
        /// <returns>uInt color.</returns>
        public uint GetColor()
        {
            return this.BGColor;
        }

        /// <summary>
        /// Our GetName method.
        /// </summary>
        /// <returns>Cell name.</returns>
        public string GetName()
        {
            return this.cellName;
        }

        /// <summary>
        /// Our GetColor method.
        /// </summary>
        /// <param name="color">uint color from spreadsheet.</param>
        public void SetColor(uint color)
        {
            this.BGColor = color;

            // Need to let front end know color was changed.
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ColorChanged"));
        }

        /// <summary>
        /// Our SetText method.
        /// </summary>
        /// <param name="value">Accepts user input as String text. EX: 3 + 3. </param>
        /// <param name="text">Accepts evaluated value from user input. EX: 6. </param>
        internal void SetText(string value, string text)
        {
            // Our function should already be processed.
            this.Value = value.ToUpper();

            if (this.text == "! Circular Reference")
            {
                this.text = text;

                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Circular References"));
            }
            else
            {
                if (this.Value != string.Empty && text == string.Empty)
                {
                    this.text = "0";
                }
                else
                {
                    this.text = text;
                }

                // Need to turn this back into form Letter:Number. AKA A26.
                string functionCell = "TextChanged " + this.cellName;

                // Will call back to this to refer to references.
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(functionCell));
            }
        }
    }
}
