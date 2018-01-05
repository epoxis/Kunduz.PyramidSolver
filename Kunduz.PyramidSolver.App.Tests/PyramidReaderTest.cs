using System;
using Xunit;
using AutoFixture;
using FluentAssertions;
using AutoFixture.AutoMoq;
using Moq;

namespace Kunduz.PyramidSolver.App.Tests
{
    public class PyramidReaderTest
    {
        private string _pyramidTxt = $"12@14 55@13 40 11".Replace("@", Environment.NewLine);
        private readonly Mock<IFileHelper> _fileHelper;
        private readonly IFixture _fixture = new Fixture();
        public PyramidReaderTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fileHelper = _fixture.Freeze<Mock<IFileHelper>>();
        }
        [Fact]
        public void GeneratePyramid_InvalidInput_ThrowsException()
        {
            _fileHelper.Setup(fr => fr.ReadAllText(It.IsAny<string>())).Returns(_fixture.Create<string>());
            var sut = _fixture.Create<PyramidReader>();

            Assert.Throws<ArgumentException>(() => sut.GeneratePyramidSections());
        }
        [Fact]
        public void GeneratePyramid_ValidInput_GeneratesPyramid()
        {
            _fileHelper.Setup(fr => fr.ReadAllText(It.IsAny<string>())).Returns(_pyramidTxt);
            var sut = _fixture.Create<PyramidReader>();
            var section = sut.GeneratePyramidSections();
            section.Should().NotBeNull();
            section.Previous.Should().NotBeNull();
        }
    }
}
