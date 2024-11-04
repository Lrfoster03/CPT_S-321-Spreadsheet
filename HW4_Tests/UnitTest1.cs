namespace Spreadsheet_Logan_Foster
{
    using NUnit.Framework;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


    public class UnitTest1
    {
        SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(26, 50);

       [SetUp]
        public void Setup()
        {
        }
       /*

        [Test]
        public void TestCell1()
        {
            // Input String to A1 cell
            sheet.GetCell(0, 0).SetText("Hey it works!", "Hey it works!");

            // Test value from Cell with value
            // Compare
            Assert.That(sheet.GetCell(0, 0).GetText(), Is.EqualTo("Hey it works!"));
        }

        [Test]
        public void TestCell2()
        {
            // Input String to random cell

            sheet.GetCell(12, 20).SetText("Hey I'm working!", "Hey I'm working!");

            // Test value from Cell with value
            // Compare
            Assert.That(sheet.GetCell(12, 20).GetText(), Is.EqualTo("Hey I'm working!"));
        }

        [Test]
        public void TestCell3()
        {
            var rand = new Random();
            int col = rand.Next(0, 26);
            int row = rand.Next(0, 50);
            sheet.GetCell(col, row).SetText("Randomly Selected!", "Randomly Selected!");
            // Input String to random cell
            //Should be cell B2
            // Test value from Cell with value
            // Compare
            Assert.That(sheet.GetCell(col, row).GetText(), Is.EqualTo("Randomly Selected!"));
        }*/
    }
}