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
    public class HorizontalVariationServiceTest : BaseTest
    {
        private HorizontalVariationService _service;
        private HorizontalVariation _horizontalVariation;
        private int _horizontalVariationId = 1;
        private int _gridId = 2;

        public HorizontalVariationServiceTest() : base()
        {
            _horizontalVariation = CreateObject.GetHorizontalVariation(this._horizontalVariationId, this._gridId);
            _service = this._autoMock.Create<HorizontalVariationService>();

            MockGetGrid(this._gridId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockHorizontalVariationRepository.Setup(x => x.Add(_horizontalVariation)).ReturnsAsync(_horizontalVariation);

            var actual = await _service.Add(_horizontalVariation);

            AssertHelper.AssertAddUpdate<HorizontalVariation>(actual);
            this._mockHorizontalVariationRepository.Verify(x => x.Add(_horizontalVariation), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockHorizontalVariationRepository.Setup(x => x.Get(_horizontalVariationId, true)).ReturnsAsync(_horizontalVariation);
            this._mockHorizontalVariationRepository.Setup(x => x.Update(_horizontalVariation)).ReturnsAsync(_horizontalVariation);

            var actual = await _service.Delete(_horizontalVariationId);

            this._mockHorizontalVariationRepository.Verify(x => x.Get(_horizontalVariationId, true), Times.Exactly(1));
            this._mockHorizontalVariationRepository.Verify(x => x.Update(_horizontalVariation), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_horizontalVariation.WasRemoved);
            Assert.NotNull(_horizontalVariation.RemovedId);
            Assert.NotNull(_horizontalVariation.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var categoryId = 1000;

            this._mockHorizontalVariationRepository.Setup(x => x.Get(categoryId, true)).Returns(Task.FromResult((HorizontalVariation?)null));

            var actual = await _service.Delete(categoryId);

            this._mockHorizontalVariationRepository.Verify(x => x.Get(categoryId, true), Times.Exactly(1));
            this._mockHorizontalVariationRepository.Verify(x => x.Update(_horizontalVariation), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_horizontalVariation.WasRemoved);
            Assert.Null(_horizontalVariation.RemovedId);
            Assert.Null(_horizontalVariation.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockHorizontalVariationRepository.Setup(x => x.Get(_horizontalVariationId, false)).ReturnsAsync(_horizontalVariation);

            var actual = await _service.Get(_horizontalVariationId);

            this._mockHorizontalVariationRepository.Verify(x => x.Get(_horizontalVariationId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _horizontalVariation);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<HorizontalVariation>() { _horizontalVariation };

            this._mockHorizontalVariationRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockHorizontalVariationRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockHorizontalVariationRepository.Setup(x => x.Get(_horizontalVariationId, true)).ReturnsAsync(_horizontalVariation);
            this._mockHorizontalVariationRepository.Setup(x => x.Update(_horizontalVariation)).ReturnsAsync(_horizontalVariation);

            var actual = await _service.Update(_horizontalVariation);

            AssertHelper.AssertAddUpdate<HorizontalVariation>(actual);
            this._mockHorizontalVariationRepository.Verify(x => x.Get(_horizontalVariationId, true), Times.Exactly(1));
            this._mockHorizontalVariationRepository.Verify(x => x.Update(_horizontalVariation), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var horizontalVariationId = 1000;

            this._mockHorizontalVariationRepository.Setup(x => x.Get(horizontalVariationId, true)).Returns(Task.FromResult((HorizontalVariation?)null));

            var actual = await _service.Update(new HorizontalVariation(horizontalVariationId, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, 1, null));

            this._mockHorizontalVariationRepository.Verify(x => x.Get(horizontalVariationId, true), Times.Exactly(1));
            this._mockHorizontalVariationRepository.Verify(x => x.Update(_horizontalVariation), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}