// <copyright file="Form1.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace Spreadsheet_Logan_Foster
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using SpreadsheetEngine;
    using SpreadsheetEngine.Changes;

    /// <summary>
    /// Our pre-generated form1 class inhereting from Windows form.
    /// </summary>
    public partial class Form1 : Form
    {
        private SpreadsheetEngine.Spreadsheet sheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.sheet = new SpreadsheetEngine.Spreadsheet(50, 26);

            this.InitializeComponent();
            this.InitializeDataGrid();
        }

        /// <summary>
        /// Initializes our dataGridView1.
        /// </summary>
        public void InitializeDataGrid()
        {
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Rows.Clear();
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            foreach (char c in alpha)
            {
                this.dataGridView1.Columns.Add(c.ToString(), c.ToString());
            }

            this.dataGridView1.Rows.Add(50);
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
            }

            this.sheet.CellPropertyChanged += this.Spreadsheet_PropertyChanged;
            this.undoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Our spreadsheet property change function that handels when text changes.
        /// </summary>
        private void Spreadsheet_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Cell? c = sender as SpreadsheetEngine.InstantiatedCell;
            if (c != null)
            {
                int col = c.GetRowIndex();
                int row = c.GetColIndex();
                if (e.PropertyName != null && e.PropertyName.Contains("CellUpdated"))
                {
                    this.dataGridView1[row, col].Value = c.GetText();
                }
                else if (e.PropertyName != null && e.PropertyName.Contains("CellColorUpdated"))
                {
                    int color = (int)c.GetColor();

                    this.dataGridView1[row, col].Style.BackColor = Color.FromArgb((int)-color);
                }
            }
        }

        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            this.dataGridView1[col, row].Value = this.sheet.GetCell(row, col).GetValue();
        }

        /// <summary>
        /// Our CellEndEdit function that handles when cells text changes.
        /// </summary>
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            // Check if the value of a cell is null
            string input = this.dataGridView1[col, row].Value.ToString() ?? string.Empty;

            ArrayList changes = new ArrayList
            {
                // Get our prior text value before changing it.
                new TextChanges(row, col, this.sheet.GetCell(row, col).GetText()),
            };

            this.sheet.AdjustCell(row, col, input);
            this.dataGridView1[col, row].Value = this.sheet.GetCell(row, col).GetText();

            // Adds our arrayList of changes to UndoStack.
            this.sheet.ClearRedo();
            this.sheet.AddUndo(changes);
            this.UpdateStacks();
        }

        /// <summary>
        /// Button click function when demo button is clicked.
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            // this.sheet.Demo();
        }

        private void ChangeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog myDialog = new ColorDialog();

            // Keeps the user from selecting a custom color.
            myDialog.AllowFullOpen = false;

            // Allows the user to get help. (The default is false.)
            myDialog.ShowHelp = true;

            myDialog.AllowFullOpen = true;

            // Update the text box color if the user clicks OK.
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                ArrayList changes = new ArrayList();
                foreach (DataGridViewCell cell in this.dataGridView1.SelectedCells)
                {
                    changes.Add(new ColorChanges(cell.RowIndex, cell.ColumnIndex, this.sheet.GetCell(cell.RowIndex, cell.ColumnIndex).GetColor()));

                    int color = myDialog.Color.ToArgb();

                    uint colorint = (uint)Math.Abs(myDialog.Color.ToArgb());

                    this.sheet.AdjustCellColor(cell.RowIndex, cell.ColumnIndex, colorint);
                }

                // Adds our arrayList of changes to UndoStack.
                this.sheet.ClearRedo();
                this.sheet.AddUndo(changes);
                this.UpdateStacks();
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sheet.Undo();
            this.UpdateStacks();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sheet.Redo();
            this.UpdateStacks();
        }

        /// <summary>
        /// Checks the undo / redo stack to see if they are empty or not.
        /// </summary>
        private void UpdateStacks()
        {
            if (this.sheet.UndoStackEmpty())
            {
                this.undoToolStripMenuItem.Enabled = false;
                this.undoToolStripMenuItem.Text = "Undo";
            }
            else
            {
                this.undoToolStripMenuItem.Enabled = true;
                this.undoToolStripMenuItem.Text = "Undo " + this.sheet.KindOfChange("undo");
            }

            if (this.sheet.RedoStackEmpty())
            {
                this.redoToolStripMenuItem.Enabled = false;
                this.redoToolStripMenuItem.Text = "Redo ";
            }
            else
            {
                this.redoToolStripMenuItem.Enabled = true;
                this.redoToolStripMenuItem.Text = "Redo " + this.sheet.KindOfChange("redo");
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z && !e.Shift)
            {
                if (this.undoToolStripMenuItem.Enabled)
                {
                    this.sheet.Undo();
                    this.UpdateStacks();
                }
            }
            else if ((e.Control && e.KeyCode == Keys.Y) || (e.Control && e.Shift && e.KeyCode == Keys.Z))
            {
                if (this.redoToolStripMenuItem.Enabled)
                {
                    this.sheet.Redo();
                    this.UpdateStacks();
                }
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                this.SaveFile();
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                this.LoadFile();
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveFile();
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadFile();
        }

        private void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML File|*.xml";
            sfd.Title = "Save";
            sfd.ShowDialog();

            Task.Run(() =>
            {
                if (sfd.FileName != string.Empty)
                {
                    using FileStream file = (FileStream)sfd.OpenFile();
                    this.sheet.Save(file);
                    file.Close();
                }
            });
        }

        private async void LoadFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML File|*.xml";
            ofd.Title = "Open";
            ofd.ShowDialog();

            await Task.Run(() =>
            {
                if (ofd.FileName != string.Empty)
                {
                    using FileStream file = (FileStream)ofd.OpenFile();
                    this.sheet.Load(file);
                    file.Close();
                }
            });
            this.UpdateStacks();
        }
    }
}