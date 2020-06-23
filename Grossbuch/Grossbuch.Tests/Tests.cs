using NUnit.Framework;
using Xamarin.UITest;

namespace Grossbuch.Tests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class ForGetBalanceAsync
    {
        IApp app;
        readonly Platform platform;

        public ForGetBalanceAsync(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void Test1()
        {
            //AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            //app.Screenshot("Welcome screen.");

            Assert.IsTrue(true);
        }

        [Test]
        public void Test2()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void Test3()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void Test4()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void Test5()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void Test6()
        {
            Assert.IsTrue(true);
        }
    }
}
