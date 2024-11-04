namespace Spreadsheet_Logan_Foster
{
    using NUnit.Framework;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


    [TestFixture]
    public class UnitTest1
    {
        SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(26, 50);

        [SetUp]
        public void Setup()
        {
        }

/*        [Test]
        public void TestCell1()
        {
            try
            {
                // Input String to A1 cell
                sheet.AdjustCell(0, 0, "Hey it works!");

                // Test value from Cell with value
                // Compare
                Assert.That(sheet.GetCell(0, 0).GetText(), Is.EqualTo("Hey it works!"));
            }
            catch(Exception ex)
            {
                Assert.That(false);
            }
        }

        [Test]
        public void TestCell2()
        {
            // Input String to random cell
            sheet.AdjustCell(12, 20, "Hey I'm working!");

            // Test value from Cell with value
            // Compare
            Assert.That(sheet.GetCell(12, 20).GetText(), Is.EqualTo("Hey I'm working!"));
        }

        [Test]
        public void TestFunction()
        {
            // Input String to A1 cell
            sheet.AdjustCell(5, 5, "=A1");

            // Test value from Cell with value
            // Compare
            Assert.That(sheet.GetCell(0, 0).GetText(), Is.EqualTo("Hey it works!"));
        }

        [Test]
        public void TestCellEquation1()
        {
            // Input String to B1 Cell
            sheet.AdjustCell(1, 1, "=30+2");

            // Test value from Cell with value
            // Compare
            Assert.That(sheet.GetCell(1, 1).GetText(), Is.EqualTo("32"));
        }


        [Test]
        public void TestCellEquation2()
        {
            // Input String to our C2 cell
            // Multiply B1 cell * 2
            sheet.AdjustCell(2, 2, "=(B2*2)/4");


            // Test value from Cell with value
            // Compare
            Assert.That(sheet.GetCell(2, 2).GetText(), Is.EqualTo("16"));
        }*/

    }
}