using System;
using Ninject;
using Ninject.MockingKernel.Moq;
using SomeClassLib;
using Xunit;
using Xunit.Abstractions;

namespace SomeLibTester
{
    public class UnitTest1
    {
        private readonly MoqMockingKernel _kernel;
        private static int cnt = 1;
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
            _kernel = new MoqMockingKernel();
            _kernel.Reset();
        }

        [Fact]
        public void ConcatTest()
        {
            _kernel.GetMock<IThingA>()
                .Setup(thing => thing.GetSomeString())
                .Returns("Goodbye");

            var classUnderTest = _kernel.Get<SomeClass>();
            var expectedResult = "Hello" + "Goodbye";
            var actualResult = classUnderTest.ConcatTopThing("Hello");
            output.WriteLine($"ActualResult: {actualResult}");
            Assert.Equal(expectedResult, actualResult);

        }

        [Fact]
        public void ComputeTest()
        {
            // you only need to mock the things you plan to use
            // note: _kernel.GetMock<T> returns a singleton instance of Mock<T>
            _kernel.GetMock<IThingB>()
                .SetupGet(thingB => thingB.SomeInt)
                .Returns(4);

            var classUnderTest = _kernel.Get<SomeClass>();

            int expectedResult = 4 + 10;
            int actualResult = classUnderTest.ComputeSomething(10);

            output.WriteLine($"ActualResult: {actualResult}");

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void SuperConcatTest()
        {
            _kernel.GetMock<IThingA>()
                .Setup(thing => thing.GetSomeString())
                .Returns("Goodbye");

            _kernel.GetMock<IThingB>()
                .Setup(thingB => thingB.SampleMethod())
                .Returns("GoAway");

            var classUnderTest = _kernel.Get<SomeClass>();
            var expectedResult = "Hello" + "Goodbye" + "GoAway";

            var actualResult = classUnderTest.SuperConcat("Hello");
            output.WriteLine($"ActualResult: {actualResult}");
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
