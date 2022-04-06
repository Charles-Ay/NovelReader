using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using NovelReader.TextLogger;
using Xunit.Sdk;

namespace NovelReaderTest
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void TestLog()
        {
            Logger.writeToLog("THIS IS A LOG FILE BEING USED FOR TESTING");
            Assert.IsTrue(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Logs.log")));
        }
    }
}
