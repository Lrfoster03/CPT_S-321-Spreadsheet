// <copyright file="Spreadsheet.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Xml;
    using System.Xml.Linq;
    using SpreadsheetEngine.Changes;
    using SpreadsheetEngine.Computations;

    /// <summary>
    /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
    /// </summary>
    public class Spreadsheet
    {
        private Cell[,] cells;
        private int columnCount;
        private int rowCount;
        private char[] formulations = new[] { '+', '-', '*', '/', '(', ')' };
        private ExpressionTree et = new ExpressionTree(string.Empty);
        private Workbook wb = new Workbook();
        private string currentCellBeingChanged = string.Empty;

        // Stores our referenced cell in the form < Cell being changed, [Cells being called]>. IE. <A1, [B1, B2]> when we adjust A1 using the formula =B1 + B2;
        // Will recursively call our dictionary to check if we have a circular reference. This will only add cells to our list that reference other cells, AKA no normal formulas.
        private Dictionary<string, HashSet<string>> referencedCells = new Dictionary<string, HashSet<string>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="row">Our row Param determining how many rows our spreadsheet application has.</param>
        /// <param name="col">>Our col Param determining how many columns our spreadsheet application has.</param>
        public Spreadsheet(int row, int col)
        {
            this.cells = new Cell[col, row];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.cells[j, i] = new InstantiatedCell(i, j);

                    // Subcribe to the property changed event
                    this.cells[j, i].PropertyChanged += this.Cell_PropertyChanged;
                }
            }

            this.rowCount = row;
            this.columnCount = col;
        }

        /// <summary>
        /// Our CellProperty Changed Event.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged = (sender, e) => { };

        /// <summary>
        /// Our GetColumnCount method.
        /// </summary>
        /// <returns>Column Count value.</returns>
        public int GetColumnCount()
        {
            return this.columnCount;
        }

        /// <summary>
        /// Our GetRowCount method.
        /// </summary>
        /// <returns>RowCount value.</returns>
        public int GetRowCount()
        {
            return this.rowCount;
        }

        /// <summary>
        /// Our GetRowCount method.
        /// </summary>
        /// <param name="row">The row number for the specified cell.</param>
        /// <param name="col">The column number for the specified cell.</param>
        /// <returns>Specified cell from Spreadsheet.</returns>
        public Cell GetCell(int row, int col)
        {
            if (row > this.rowCount)
            {
                // This is for overflow protection.
                row = this.rowCount - 1;
            }

            if (col > this.columnCount)
            {
                // This is for overflow protection.
                col = this.columnCount - 1;
            }

            if (row < 0)
            {
                row = 0;
            }

            if (col < 0)
            {
                col = 0;
            }

            return this.cells[col, row];
        }

        /// <summary>
        /// Allows us to adjust a specific cell.
        /// </summary>
        /// <param name="row">The row number for the specified cell.</param>
        /// <param name="col">The column number for the specified cell.</param>
        /// <param name="text">Our text entered by our user.</param>
        public void AdjustCell(int row, int col, string? text)
        {
            Cell ourcell = this.GetCell(row, col);

            this.currentCellBeingChanged = ourcell.GetName();

            string formula;
            if (text != null && text.Contains(ourcell.GetName()))
            {
                formula = "! Self Reference";
            }
            else if (string.IsNullOrEmpty(text) || text.Substring(0, 1) != "=")
            {
                formula = text ?? string.Empty;
            }
            else
            {
                // Remove our intiial = and then evaluate.
                formula = this.EvaluateFormula(text.Substring(1));
            }

            ourcell.SetText(text ?? string.Empty, formula);
        }

        /// <summary>
        /// Allows us to adjust the color of a specific cell.
        /// </summary>
        /// <param name="row">The row number for the specified cell.</param>
        /// <param name="col">The column number for the specified cell.</param>
        /// <param name="color">The adjusted color for our cell in uInt form.</param>
        public void AdjustCellColor(int row, int col, uint color)
        {
            this.GetCell(row, col).SetColor(color);
        }

        /// <summary>
        /// Add an undo command to our undo Stack.
        /// </summary>
        /// <param name="change">The specific change we're making. </param>
        public void AddUndo(ArrayList change)
        {
            this.wb.AddUndoStack(change);
        }

        /// <summary>
        /// Add an undo command to our undo Stack.
        /// </summary>
        /// <param name="change">The specific change we're making. </param>
        public void AddRedo(ArrayList change)
        {
            // this.wb.clear();
            this.wb.AddRedoStack(change);
        }

        /// <summary>
        /// Clears our redoStack after adding a new item.
        /// </summary>
        public void ClearRedo()
        {
            this.wb.ClearRedo();
        }

        /// <summary>
        /// Executes our undo commands.
        /// </summary>
        public void Undo()
        {
            ArrayList changes = this.wb.Undo();
            ArrayList redoChanges = new ArrayList();
            foreach (IChanges change in changes)
            {
                // If color change
                if (change.GetType() == typeof(ColorChanges))
                {
                    ColorChanges temp = (ColorChanges)change;

                    // Get the current cells color and then push to redoStack.
                    redoChanges.Add(new ColorChanges(temp.Row, temp.Col, this.GetCell(temp.Row, temp.Col).GetColor()));
                    this.AdjustCellColor(temp.Row, temp.Col, temp.Color);
                }

                // Must be a text change.
                else
                {
                    TextChanges temp = (TextChanges)change;

                    redoChanges.Add(new TextChanges(temp.Row, temp.Col, this.GetCell(temp.Row, temp.Col).GetValue()));
                    this.AdjustCell(temp.Row, temp.Col, temp.Value);
                }
            }

            this.AddRedo(redoChanges);
        }

        /// <summary>
        /// Executes our redo commands.
        /// </summary>
        public void Redo()
        {
            ArrayList changes = this.wb.Redo();
            ArrayList undoChanges = new ArrayList();
            foreach (IChanges change in changes)
            {
                // If color change
                if (change.GetType() == typeof(ColorChanges))
                {
                    ColorChanges temp = (ColorChanges)change;

                    // Get the current cells color and then push to redoStack.
                    undoChanges.Add(new ColorChanges(temp.Row, temp.Col, this.GetCell(temp.Row, temp.Col).GetColor()));
                    this.AdjustCellColor(temp.Row, temp.Col, temp.Color);
                }

                // Must be a text change.
                else
                {
                    TextChanges temp = (TextChanges)change;

                    undoChanges.Add(new TextChanges(temp.Row, temp.Col, this.GetCell(temp.Row, temp.Col).GetValue()));
                    this.AdjustCell(temp.Row, temp.Col, temp.Value);
                }
            }

            this.AddUndo(undoChanges);
        }

        /// <summary>
        /// checks if undoStack is empty.
        /// </summary>
        /// <returns>Boolean if undoStack is empty. </returns>
        public bool UndoStackEmpty()
        {
            return this.wb.UndoStackEmpty();
        }

        /// <summary>
        /// checks if redoStack is empty.
        /// </summary>
        /// <returns>Boolean if redoStack is empty. </returns>
        public bool RedoStackEmpty()
        {
            return this.wb.RedoStackEmpty();
        }

        /// <summary>
        /// Returns the kind of change we would perform.
        /// </summary>
        /// <param name="changeType">A string rep of what change we're making.</param>
        /// <returns>String of change.</returns>
        public string KindOfChange(string changeType)
        {
            if (changeType.Equals("undo"))
            {
                return this.wb.KindOfChangeUndo();
            }
            else
            {
                return this.wb.KindOfChangeRedo();
            }
        }

        /// <summary>
        /// Clears our spreadsheet and sets everything back to default values.
        /// </summary>
        public void ClearSpreadsheet()
        {
            // Clears both stacks.
            this.wb.ClearStacks();
            this.referencedCells = new Dictionary<string, HashSet<string>>();
            for (int i = 0; i < this.rowCount; i++)
            {
                for (int j = 0; j < this.columnCount; j++)
                {
                    this.AdjustCell(i, j, string.Empty);
                    this.AdjustCellColor(i, j, 1);
                }
            }
        }

        /// <summary>
        /// Saves our spreadsheet to an XML File.
        /// </summary>
        /// <param name="stream">A file stream to save our spreadsheet.</param>
        public void Save(Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter w = XmlWriter.Create(stream, settings);

            // Start writing the document
            w.WriteStartDocument();

            w.WriteStartElement("Spreadsheet");

            for (int i = 0; i < this.rowCount; i++)
            {
                for (int j = 0; j < this.columnCount; j++)
                {
                    Cell ourCell = this.GetCell(i, j);

                    // If the cell is changed in some way, then we write it.
                    if (ourCell.GetValue() != string.Empty || ourCell.GetColor() != 1)
                    {
                        // Create a Cell block to write
                        w.WriteStartElement("Cell");

                        w.WriteElementString("Name", ourCell.GetName());
                        w.WriteElementString("Text", ourCell.GetValue());
                        w.WriteElementString("Color", ourCell.GetColor().ToString());

                        w.WriteEndElement(); // </Cell>
                    }
                }
            }

            w.WriteEndElement(); // </Spreadsheet>
            w.Close();
        }

        /// <summary>
        /// Loads our spreadsheet from an XML File.
        /// </summary>
        /// <param name="stream">A file stream to load our spreadsheet.</param>
        public void Load(Stream stream)
        {
            // Clear spreadsheet.
            this.ClearSpreadsheet();

            // Set the reader settings
            XDocument reader = XDocument.Load(stream);

            if (reader.Root != null)
            {
                foreach (XElement tag in reader.Root.Elements("Cell"))
                {
                    // get the cell into memory
                    var nameElement = tag.Element("Name");
                    (int, int) cellData = nameElement != null ? this.GetCellFromFormula(formula: nameElement.Value) : (0, 0);

                    // load the properties of the cell based on the
                    // values of the XML tags
                    // check for a valid text element
                    if (tag.Element("Text") != null)
                    {
                        // load the cell's text
                        var textElement = tag.Element("Text");
                        if (textElement != null)
                        {
                            this.AdjustCell(cellData.Item2 - 1, cellData.Item1 - 1, textElement.Value.ToString());
                        }
                    }

                    // check for a valid background color element
                    if (tag.Element("Color") != null)
                    {
                        // convert the string to a uint for the background color
                        var colorElement = tag.Element("Color");
                        if (colorElement != null)
                        {
                            this.AdjustCellColor(cellData.Item2 - 1, cellData.Item1 - 1, Convert.ToUInt32(colorElement.Value.ToString()));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Allows us to evaluate all our formulas.
        /// </summary>
        /// <param name="formula">The string inperpretation of our formula.</param>
        /// <returns>An evaluation of our string formula.</returns>
        public string EvaluateFormula(string formula)
        {
            double number;

            this.DeleteCircularReferences(this.currentCellBeingChanged);

            // If its an equation formula.
            if (this.formulations.Any(formula.Contains))
            {
                this.et.SetExpression(formula);

                // Now check for variables and add their values to our expression tree.
                string[] variables = formula.Split(this.formulations);

                foreach (string var in variables)
                {
                    // Check if its a variable.
                    if (!double.TryParse(var, out number))
                    {
                        // Check if it is a cell we're dealing with
                        if (var.Length > 1 && var.Length < 4 && double.TryParse(var.Substring(1), out number) && (number < 51.0))
                        {
                            // Set our references to our other cells.
                            this.SetReference(this.currentCellBeingChanged, variables);

                            if (this.CheckCircularReference(var, this.currentCellBeingChanged))
                            {
                                return "! Circular Reference";
                            }
                            else
                            {
                                double.TryParse(this.EvaluateFormula(var), out number);
                                this.et.SetVariable(var, number);
                            }
                        }
                        else
                        {
                            return "! Bad Reference";
                        }
                    }
                }

                return this.et.Evaluate() + string.Empty;
            }

            // Check if its a normal function ex: =A20.
            else if (formula.Length > 1 && formula.Length < 4 && double.TryParse(formula.Substring(1), out number) && (number < 51.0))
            {
                // Set our references to our other cells.
                this.SetReference(this.currentCellBeingChanged, new string[] { formula });

                // Process function (Accepts input in form =rowcol. EX: =A26 )
                (int, int) formcell = this.GetCellFromFormula(formula);

                if (this.CheckCircularReference(formula, this.currentCellBeingChanged))
                {
                    return "! Circular Reference";
                }
                else
                {
                    // Now set our text to the new text (AKA interpretation of formula)
                    return this.GetCell(formcell.Item2 - 1, formcell.Item1 - 1).GetText();
                }
            }
            else
            {
                // Otherwise we return our result.
                return "! Bad Reference";
            }
        }

        private void SetReference(string cellName, string[] vars)
        {
            foreach (string variableName in vars)
            {
                if (!this.referencedCells.ContainsKey(variableName))
                {
                    this.referencedCells[variableName] = new HashSet<string>();
                }

                this.referencedCells[variableName].Add(cellName);
            }
        }

        /// <summary>
        /// Accepts a formula for a cell in the Form Letter:Number. Ex: A26.
        /// </summary>
        /// /// <param name="formula">The string representation of our cell.</param>
        /// <returns> A touple of the (row, col) integers. </returns>
        private (int, int) GetCellFromFormula(string formula)
        {
            formula = formula.ToUpper();

            // Process function (Accepts input in form =rowcol. EX: =A26 )
            // Gets our row value from first character.
            char a = formula[0];

            // -65 because -64 ascii characters to get correct index and -1 for 0 point indexing.
            int row = a - 64;

            // Now parse our column value from substring 2 onward
            int col = int.Parse(formula.Substring(1));

            return (row, col);
        }

        /// <summary>
        /// Our Cell_PropertyChanged method that manages when a property change occurs.
        /// </summary>
        /// <param name="sender">The object that calls the Cell_PropertyChanged event.</param>
        /// <param name="e">TThe data contained in the Cell_PropertyChanged event call.</param>
        private void Cell_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Cell? c = sender as InstantiatedCell ?? throw new NullReferenceException("Cell is null");

            // Need to re-evaluate the cells value.
            if (e.PropertyName == "ReEvaluateCell")
            {
                this.AdjustCell(c.GetRowIndex(), c.GetColIndex(), c.GetValue());
            }
            else if (e.PropertyName != null && e.PropertyName.Contains("TextChanged"))
            {
                string var2change = e.PropertyName.Substring(12);
                for (int i = 0; i < this.cells.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < this.cells.GetLength(1) - 1; j++)
                    {
                        if (this.cells[i, j].GetValue().Contains(var2change))
                        {
                            if (this.cells[i, j].GetText() == "∞")
                            {
                                this.AdjustCell(j, i, "! Circular Reference");
                            }
                            else if (this.cells[i, j].GetName() != var2change)
                            {
                                this.AdjustCell(j, i, this.cells[i, j].GetValue());
                            }

                            this.CellPropertyChanged(this.cells[i, j], new PropertyChangedEventArgs("CellUpdated"));
                        }
                    }
                }

                (int, int) x = this.GetCellFromFormula(var2change);

                this.CellPropertyChanged(this.cells[x.Item1 - 1, x.Item2 - 1], new PropertyChangedEventArgs("CellUpdated"));
            }
            else if (e.PropertyName == "ColorChanged")
            {
                this.CellPropertyChanged(this.cells[c.GetColIndex(), c.GetRowIndex()], new PropertyChangedEventArgs("CellColorUpdated"));
            }
            else if (e.PropertyName == "Circular References")
            {
                this.CellPropertyChanged(this.cells[c.GetColIndex(), c.GetRowIndex()], new PropertyChangedEventArgs("CellUpdated"));
            }
        }

        /// <summary>
        /// If A1 = B1, check B1 to make sure its also in here. Want to run through this until we reach A1 again.
        /// If this doesn't loop, then we should be fine, however this could force us to jump into the loop and then we stack overflow. Need an array of previously parsed values
        /// And if we go back through said array, we fail and indicate that we've reached a circular reference.
        /// </summary>
        /// <param name="varName">The variable name we're calling / checking.</param>
        /// <param name="currentCell">The current cell we're editing.</param>
        /// <returns>A true or false value indicating if we have a circular reference.</returns>
        private bool CheckCircularReference(string varName, string currentCell)
        {
            if (currentCell == varName)
            {
            return true;
            }

            if (!this.referencedCells.ContainsKey(currentCell))
            {
            return false;
            }

            foreach (string var in this.referencedCells[currentCell])
            {
                if (this.CheckCircularReference(varName, var))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// If we detect a circular refrence in our code, then we'll set it as a circular refrence exception
        /// and then remove the exception so we can continue to check our code in the future.
        /// </summary>
        /// <param name="cellName">Our cell that has a refrence. </param>
        private void DeleteCircularReferences(string cellName)
        {
            List<string> dictionaryList = new List<string>();

            foreach (string key in this.referencedCells.Keys)
            {
                if (this.referencedCells[key].Contains(cellName))
                {
                    dictionaryList.Add(key);
                }
            }

            foreach (string item in dictionaryList)
            {
                HashSet<string> set = this.referencedCells[item];
                if (set.Contains(cellName))
                {
                    set.Remove(cellName);
                }
            }
        }
    }
}
