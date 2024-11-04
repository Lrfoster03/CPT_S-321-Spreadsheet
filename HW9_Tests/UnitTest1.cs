using SpreadsheetEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Threading.Channels;

namespace HW9_Tests
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


        // Tests the Saving functionality of the spreadsheet by saving files, clearing, and then reloading files
        public void TestSaveAndLoad1()
        {
            // Change cell A1 to be a different color
            sheet.AdjustCellColor(0, 0, 734657845);

            String path = "./texting.xml";

            FileStream Savefile = File.Create(path);

            this.sheet.Save(Savefile);

            Savefile.Close();

            FileStream Openfile = File.OpenRead(path);

            this.sheet.Load(Openfile);

            Assert.That(sheet.GetCell(0, 0).GetColor, Is.EqualTo(734657845));
        }

        [Test]
        public void TestSaveAndLoad2()
        {
            // Change cell A1 to be a different color
            sheet.AdjustCell(0, 0, "=(2-8)*6");

            String path = "./textingFuct.xml";

            FileStream Savefile = File.Create(path);

            this.sheet.Save(Savefile);

            Savefile.Close();

            FileStream Openfile = File.OpenRead(path);

            this.sheet.Load(Openfile);

            Assert.That(sheet.GetCell(0, 0).GetText(), Is.EqualTo("-36"));
        }*/

    }
}