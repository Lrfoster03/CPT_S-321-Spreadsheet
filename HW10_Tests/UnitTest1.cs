namespace HW10_Tests
{
    public class Tests
    {
        SpreadsheetEngine.Spreadsheet sheet = new SpreadsheetEngine.Spreadsheet(26, 50);

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        // Tests the Self Reference function
        public void TestSelfReference()
        {
            sheet.AdjustCell(0, 0, "=A1");

            // Test to see if that cell returns "! Self Reference"
            // Compare
            Assert.That(sheet.GetCell(0, 0).GetText(), Is.EqualTo("! Self Reference"));
        }

        [Test]
        // Tests the Bad Reference function
        public void TestBadReference()
        {
            sheet.AdjustCell(1, 1, "=6+Cell*27");

            // Test to see if that cell returns "! Bad Reference"
            // Compare
            Assert.That(sheet.GetCell(1, 1).GetText(), Is.EqualTo("! Bad Reference"));
        }

        [Test]
        // Tests the Circular Reference function
        public void TestCircularReference()
        {
            sheet.AdjustCell(0, 0, "=B2");
            sheet.AdjustCell(1, 1, "=A1");


            // Test to see if that cell returns "! Circular Reference"
            // Compare
            Assert.That(sheet.GetCell(0, 0).GetText(), Is.EqualTo("! Circular Reference"));
        }

    }
}
