using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gasstation;
using Gasstation.Implementation;

namespace Unit_Tests
{
    [TestClass]
    public class GasstationTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            int x = 5;
            Assert.AreEqual(5, 5);
      
        }

        public void TestZapfhahnSelection()
        {
            // deprecated 
            // Zapfsaeule zapfsaeule = new Zapfsaeule();
        }
    }
}
