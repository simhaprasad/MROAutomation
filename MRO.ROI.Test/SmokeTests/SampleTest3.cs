using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Test.SmokeTests
{

    [TestClass]
    public class SampleTest3
    {

        [TestMethod]
        public void samplemethod3()
        {
            TestBase baseClass = new TestBase();
            baseClass.Init("ROIAdmin");
            baseClass.Dispose();

        }
    }
}

