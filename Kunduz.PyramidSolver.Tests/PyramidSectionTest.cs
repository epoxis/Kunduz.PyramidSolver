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
        [Theory, AutoData]
        public static void Constructor_WithValue_ShouldCreatesArrayWithOneValue(int value)
        {
            var pyramidSection = new PyramidSection(value);

            pyramidSection.Step.Should().Be(1);
            pyramidSection.Values.Count.Should().Be(1);
            pyramidSection.Values.ElementAt(0).Should().Be(value);
        }
        [Theory, AutoData]
        public static void CreateNext_ArrayWithWrongSize_ShouldThrowException(PyramidSection sut, Generator<int> generator, IFixture fixture)
        {
            var size = generator.First(number => number != sut.Step + 1);
            var values = fixture.CreateMany<int>(size).ToArray();

            Assert.Throws<ArgumentException>(() => sut.CreateNext(values));
        }
        [Theory, AutoData]
        public static void CreateNext_ArrayWithCorrectSize_ShouldCreateNextSection(PyramidSection sut, IFixture fixture)
        {
            var size = sut.Step + 1;
            var values = fixture.CreateMany<int>(size).ToArray();

            var nextSection = sut.CreateNext(values);

            nextSection.Step.Should().Be(size);
            nextSection.Values.Count().Should().Be(size);
            nextSection.Previous.Should().Be(sut);
        }

    }
}
