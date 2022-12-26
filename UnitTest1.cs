using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Курсовая_ТРПО_2022;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            sign_up sign = new sign_up();
            string l = "Denis";
            string p = "123456";
            bool exp = false;
            var res = sign.chekuser(l, p);
            Assert.AreEqual(exp, res);
        }
    }
}
