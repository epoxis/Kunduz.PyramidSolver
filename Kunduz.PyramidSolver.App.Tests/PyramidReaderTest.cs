using System;
using Moq;
using Xunit;
using AutoFixture;
using AutoFixture.Idioms;
using AutoFixture.AutoMoq;
using FluentAssertions;
using AutoFixture.Xunit2;
using System.IO;

namespace Kunduz.PyramidSolver.App.Tests
{
    public class PyramidReaderTest
    {
        [Theory, AutoMoqData]
        public void GeneratePyramid_InvalidInput_ThrowsException([Frozen]Mock<IFileHelper> fileHelper, string file, string path, PyramidReader sut)
        {
            fileHelper.Setup(fr => fr.FileExists(It.IsAny<string>())).Returns(true);
            fileHelper.Setup(fr => fr.ReadAllText(It.IsAny<string>())).Returns(file);

            Assert.Throws<ArgumentException>(() => sut.GeneratePyramidSections(path));
        }
        [Theory, AutoMoqData]
        public void GeneratePyramid_FileDoesntExist_ThrowsFileNotFoundException([Frozen]Mock<IFileHelper> fileHelper, string path, PyramidReader sut)
        {
            fileHelper.Setup(fr => fr.FileExists(path)).Returns(false);

            Assert.Throws<FileNotFoundException>(() => sut.GeneratePyramidSections(path));
        }
        [Theory, AutoMoqData]
        public void GeneratePyramid_ValidInput_GeneratesPyramid([Frozen]Mock<IFileHelper> fileHelper, string path, PyramidReader sut)
        {
            var pyramidText = $"12@14 55@13 40 11".Replace("@", Environment.NewLine);
            fileHelper.Setup(fr => fr.FileExists(It.IsAny<string>())).Returns(true);
            fileHelper.Setup(fr => fr.ReadAllText(It.IsAny<string>())).Returns(pyramidText);

            var section = sut.GeneratePyramidSections(path);

            section.Should().NotBeNull();
            section.Previous.Should().NotBeNull();
        }
        [Theory, AutoMoqData]
        public void Constructor_GuardClausesArePresent(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(PyramidReader).GetConstructors());
        }
        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization()))
            {

            }
        }
    }
}
