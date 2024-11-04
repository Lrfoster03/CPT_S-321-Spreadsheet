using SpreadsheetEngine;
using System.Collections;
using System.Threading.Channels;

namespace HW8_Tests
{
    public class Tests
    {
        SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(26, 50);

        [SetUp]
        public void Setup()
        {
        }
/*
        [Test]
        // Assign a new color to a cell and check its that color. 
        public void TestColor()
        {
            sheet.AdjustCellColor(0, 0, 734657845);

            // Test color from Cell with color
            // Compare
            Assert.That(sheet.GetCell(0, 0).GetColor, Is.EqualTo(734657845));
        }

        [Test]
        public void TestCellEquation()
        {

            ArrayList changes = new ArrayList() { (new SpreadsheetEngine.TextChanges(1, 1, this.sheet.GetCell(1, 1).GetText())) };

            // Input String to B2 Cell
            sheet.AdjustCell(1, 1, "=30+2");

            // Adds our arrayList of changes to UndoStack.
            this.sheet.AddUndo(changes);

            // Test value from Cell with value
            // Compare
            Assert.That(sheet.GetCell(1, 1).GetText(), Is.EqualTo("32"));
        }

        [Test]
        // Set a cell to a new value and then undo, ensuring that it was the value it was before (Most likely String.empty())
        public void TestUndo()
        {
            // Undo our previous tests results.
            sheet.Undo();

            // Test value from Cell with value
            // Compare
            Assert.That(sheet.GetCell(1, 1).GetText(), Is.EqualTo(string.Empty));
        }*/
    }
}