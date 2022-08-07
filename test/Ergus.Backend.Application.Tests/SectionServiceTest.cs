using Ergus.Backend.Application.Services;
using Ergus.Backend.Application.Tests.Helpers;
using Ergus.Backend.Infrastructure.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Ergus.Backend.Application.Tests
{
    public class SectionServiceTest : BaseTest
    {
        private SectionService _service;
        private Section _section;
        private int _sectionId = 1;

        public SectionServiceTest() : base()
        {
            _section = CreateObject.GetSection(this._sectionId);
            _service = this._autoMock.Create<SectionService>();
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockSectionRepository.Setup(x => x.Add(_section)).ReturnsAsync(_section);

            var actual = await _service.Add(_section);

            AssertHelper.AssertAddUpdate<Section>(actual);
            this._mockSectionRepository.Verify(x => x.Add(_section), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockSectionRepository.Setup(x => x.Get(_sectionId, true)).ReturnsAsync(_section);
            this._mockSectionRepository.Setup(x => x.Update(_section)).ReturnsAsync(_section);

            var actual = await _service.Delete(_sectionId);

            this._mockSectionRepository.Verify(x => x.Get(_sectionId, true), Times.Exactly(1));
            this._mockSectionRepository.Verify(x => x.Update(_section), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_section.WasRemoved);
            Assert.NotNull(_section.RemovedId);
            Assert.NotNull(_section.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var categoryId = 1000;

            this._mockSectionRepository.Setup(x => x.Get(categoryId, true)).Returns(Task.FromResult((Section?)null));

            var actual = await _service.Delete(categoryId);

            this._mockSectionRepository.Verify(x => x.Get(categoryId, true), Times.Exactly(1));
            this._mockSectionRepository.Verify(x => x.Update(_section), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_section.WasRemoved);
            Assert.Null(_section.RemovedId);
            Assert.Null(_section.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockSectionRepository.Setup(x => x.Get(_sectionId, false)).ReturnsAsync(_section);

            var actual = await _service.Get(_sectionId);

            this._mockSectionRepository.Verify(x => x.Get(_sectionId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _section);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<Section>() { _section };

            this._mockSectionRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockSectionRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockSectionRepository.Setup(x => x.Get(_sectionId, true)).ReturnsAsync(_section);
            this._mockSectionRepository.Setup(x => x.Update(_section)).ReturnsAsync(_section);

            var actual = await _service.Update(_section);

            AssertHelper.AssertAddUpdate<Section>(actual);
            this._mockSectionRepository.Verify(x => x.Get(_sectionId, true), Times.Exactly(1));
            this._mockSectionRepository.Verify(x => x.Update(_section), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var sectionId = 1000;

            this._mockSectionRepository.Setup(x => x.Get(sectionId, true)).Returns(Task.FromResult((Section?)null));

            var actual = await _service.Update(new Section(sectionId, String.Empty, String.Empty, String.Empty));

            this._mockSectionRepository.Verify(x => x.Get(sectionId, true), Times.Exactly(1));
            this._mockSectionRepository.Verify(x => x.Update(_section), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}