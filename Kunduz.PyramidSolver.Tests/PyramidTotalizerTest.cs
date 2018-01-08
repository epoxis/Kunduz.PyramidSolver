using System;
using System.Linq;
using Moq;
using Xunit;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoFixture.AutoMoq;
using FluentAssertions;

namespace Kunduz.PyramidSolverTests
{
    using PyramidSolver;
    public class PyramidTotalizerTest
    {
        [Theory, AutoData]
        public static void Totalize_ShouldTotalizeSections(PyramidTotalizer sut)
        {
            var topValue = 3;
            var array1 = new[] { 7, 4 };
            var array2 = new[] { 2, 4, 6 };
            var array3 = new[] { 8, 5, 9, 3 };

            var bottomSection = new PyramidSection(topValue)
                .CreateNext(array1)
                .CreateNext(array2)
                .CreateNext(array3);
            var value = sut.Totalize(bottomSection);

            value.Should().Be(23);
        }
        [Theory, AutoMoqData]
        public static void Totalize_PyramidShapeIsWrong_ThrowsException(
            PyramidTotalizer sut,
            Mock<IPyramidSection> section1,
            Mock<IPyramidSection> section2,
            Mock<IPyramidSection> section3)
        {
            section1.SetupGet(s => s.Step).Returns(5);
            section1.Setup(s => s.Previous).Returns(section2.Object);

            section2.SetupGet(s => s.Step).Returns(7);
            section2.Setup(s => s.Previous).Returns(section3.Object);

            section3.SetupGet(s => s.Step).Returns(1);
            section3.Setup(s => s.Previous).Returns(() => null);

            Assert.Throws<ArgumentException>(() => sut.Totalize(section1.Object));
        }
        [Theory, AutoMoqData]
        public static void Totalize_PyramidDoesntHaveTopLayer_ThrowsException(
            PyramidTotalizer sut,
            Mock<IPyramidSection> section1,
            Mock<IPyramidSection> section2,
            Mock<IPyramidSection> section3,
            IFixture fixture)
        {
            section1.SetupGet(s => s.Values).Returns(fixture.CreateMany<int>(4).ToArray());
            section1.SetupGet(s => s.Step).Returns(4);
            section1.Setup(s => s.Previous).Returns(section2.Object);

            section2.SetupGet(s => s.Values).Returns(fixture.CreateMany<int>(3).ToArray());
            section2.SetupGet(s => s.Step).Returns(3);
            section2.Setup(s => s.Previous).Returns(section3.Object);

            section3.SetupGet(s => s.Values).Returns(fixture.CreateMany<int>(2).ToArray());
            section3.SetupGet(s => s.Step).Returns(2);
            section3.Setup(s => s.Previous).Returns(() => null);

            Assert.Throws<InvalidOperationException>(() => sut.Totalize(section1.Object));
        }

        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization()))
            {

            }
        }
    }
}
