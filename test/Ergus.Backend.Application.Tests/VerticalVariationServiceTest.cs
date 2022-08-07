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
    public class VerticalVariationServiceTest : BaseTest
    {
        private VerticalVariationService _service;
        private VerticalVariation _verticalVariation;
        private int _verticalVariationId = 1;
        private int _gridId = 2;

        public VerticalVariationServiceTest() : base()
        {
            _verticalVariation = CreateObject.GetVerticalVariation(this._verticalVariationId, this._gridId);
            _service = this._autoMock.Create<VerticalVariationService>();

            MockGetGrid(this._gridId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockVerticalVariationRepository.Setup(x => x.Add(_verticalVariation)).ReturnsAsync(_verticalVariation);

            var actual = await _service.Add(_verticalVariation);

            AssertHelper.AssertAddUpdate<VerticalVariation>(actual);
            this._mockVerticalVariationRepository.Verify(x => x.Add(_verticalVariation), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockVerticalVariationRepository.Setup(x => x.Get(_verticalVariationId, true)).ReturnsAsync(_verticalVariation);
            this._mockVerticalVariationRepository.Setup(x => x.Update(_verticalVariation)).ReturnsAsync(_verticalVariation);

            var actual = await _service.Delete(_verticalVariationId);

            this._mockVerticalVariationRepository.Verify(x => x.Get(_verticalVariationId, true), Times.Exactly(1));
            this._mockVerticalVariationRepository.Verify(x => x.Update(_verticalVariation), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_verticalVariation.WasRemoved);
            Assert.NotNull(_verticalVariation.RemovedId);
            Assert.NotNull(_verticalVariation.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var categoryId = 1000;

            this._mockVerticalVariationRepository.Setup(x => x.Get(categoryId, true)).Returns(Task.FromResult((VerticalVariation?)null));

            var actual = await _service.Delete(categoryId);

            this._mockVerticalVariationRepository.Verify(x => x.Get(categoryId, true), Times.Exactly(1));
            this._mockVerticalVariationRepository.Verify(x => x.Update(_verticalVariation), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_verticalVariation.WasRemoved);
            Assert.Null(_verticalVariation.RemovedId);
            Assert.Null(_verticalVariation.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockVerticalVariationRepository.Setup(x => x.Get(_verticalVariationId, false)).ReturnsAsync(_verticalVariation);

            var actual = await _service.Get(_verticalVariationId);

            this._mockVerticalVariationRepository.Verify(x => x.Get(_verticalVariationId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _verticalVariation);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<VerticalVariation>() { _verticalVariation };

            this._mockVerticalVariationRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockVerticalVariationRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockVerticalVariationRepository.Setup(x => x.Get(_verticalVariationId, true)).ReturnsAsync(_verticalVariation);
            this._mockVerticalVariationRepository.Setup(x => x.Update(_verticalVariation)).ReturnsAsync(_verticalVariation);

            var actual = await _service.Update(_verticalVariation);

            AssertHelper.AssertAddUpdate<VerticalVariation>(actual);
            this._mockVerticalVariationRepository.Verify(x => x.Get(_verticalVariationId, true), Times.Exactly(1));
            this._mockVerticalVariationRepository.Verify(x => x.Update(_verticalVariation), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var verticalVariationId = 1000;

            this._mockVerticalVariationRepository.Setup(x => x.Get(verticalVariationId, true)).Returns(Task.FromResult((VerticalVariation?)null));

            var actual = await _service.Update(new VerticalVariation(verticalVariationId, String.Empty, String.Empty, String.Empty, String.Empty, 1, null));

            this._mockVerticalVariationRepository.Verify(x => x.Get(verticalVariationId, true), Times.Exactly(1));
            this._mockVerticalVariationRepository.Verify(x => x.Update(_verticalVariation), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}