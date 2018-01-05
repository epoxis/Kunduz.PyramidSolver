using System;
using System.Linq;
using Xunit;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoFixture.AutoMoq;
using FluentAssertions;

namespace Kunduz.PyramidSolverTests
{
    using PyramidSolver;
    public class PyramidSectionTest
    {
        private readonly IFixture _fixture;
        public PyramidSectionTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }
        [Theory, AutoData]
        public void Constructor_WithValue_ShouldCreatesArrayWithOneValue(int value)
        {
            var pyramidSection = new PyramidSection(value);

            pyramidSection.Step.Should().Be(1);
            pyramidSection.Values.Count.Should().Be(1);
            pyramidSection.Values.ElementAt(0).Should().Be(value);
        }
        [Theory, AutoData]
        public void CreateNext_ArrayWithWrongSize_ShouldThrowException(PyramidSection sut, Generator<int> generator)
        {
            var size = generator.First(number => number != sut.Step + 1);
            var values = _fixture.CreateMany<int>(size).ToArray();

            Assert.Throws<ArgumentException>(() => sut.CreateNext(values));
        }
        [Theory, AutoData]
        public void CreateNext_ArrayWithCorrectSize_ShouldCreateNextSection(PyramidSection sut)
        {
            var size = sut.Step + 1;
            var values = _fixture.CreateMany<int>(size).ToArray();

            var nextSection = sut.CreateNext(values);

            nextSection.Step.Should().Be(size);
            nextSection.Values.Count().Should().Be(size);
            nextSection.Previous.Should().Be(sut);
        }
      
    }
}
