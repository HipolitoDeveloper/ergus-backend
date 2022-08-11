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
    public class UnitOfMeasureServiceTest : BaseTest
    {
        private UnitOfMeasureService _service;
        private UnitOfMeasure _unitOfMeasure;
        private int _unitOfMeasureId = 1;

        public UnitOfMeasureServiceTest() : base()
        {
            _unitOfMeasure = CreateObject.GetUnitOfMeasure(this._unitOfMeasureId);
            _service = this._autoMock.Create<UnitOfMeasureService>();
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockUnitOfMeasureRepository.Setup(x => x.Add(_unitOfMeasure)).ReturnsAsync(_unitOfMeasure);

            var actual = await _service.Add(_unitOfMeasure);

            AssertHelper.AssertAddUpdate<UnitOfMeasure>(actual);
            this._mockUnitOfMeasureRepository.Verify(x => x.Add(_unitOfMeasure), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockUnitOfMeasureRepository.Setup(x => x.Get(_unitOfMeasureId, true)).ReturnsAsync(_unitOfMeasure);
            this._mockUnitOfMeasureRepository.Setup(x => x.Update(_unitOfMeasure)).ReturnsAsync(_unitOfMeasure);

            var actual = await _service.Delete(_unitOfMeasureId);

            this._mockUnitOfMeasureRepository.Verify(x => x.Get(_unitOfMeasureId, true), Times.Exactly(1));
            this._mockUnitOfMeasureRepository.Verify(x => x.Update(_unitOfMeasure), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_unitOfMeasure.WasRemoved);
            Assert.NotNull(_unitOfMeasure.RemovedId);
            Assert.NotNull(_unitOfMeasure.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var categoryId = 1000;

            this._mockUnitOfMeasureRepository.Setup(x => x.Get(categoryId, true)).Returns(Task.FromResult((UnitOfMeasure?)null));

            var actual = await _service.Delete(categoryId);

            this._mockUnitOfMeasureRepository.Verify(x => x.Get(categoryId, true), Times.Exactly(1));
            this._mockUnitOfMeasureRepository.Verify(x => x.Update(_unitOfMeasure), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_unitOfMeasure.WasRemoved);
            Assert.Null(_unitOfMeasure.RemovedId);
            Assert.Null(_unitOfMeasure.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockUnitOfMeasureRepository.Setup(x => x.Get(_unitOfMeasureId, false)).ReturnsAsync(_unitOfMeasure);

            var actual = await _service.Get(_unitOfMeasureId);

            this._mockUnitOfMeasureRepository.Verify(x => x.Get(_unitOfMeasureId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _unitOfMeasure);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<UnitOfMeasure>() { _unitOfMeasure };

            this._mockUnitOfMeasureRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockUnitOfMeasureRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockUnitOfMeasureRepository.Setup(x => x.Get(_unitOfMeasureId, true)).ReturnsAsync(_unitOfMeasure);
            this._mockUnitOfMeasureRepository.Setup(x => x.Update(_unitOfMeasure)).ReturnsAsync(_unitOfMeasure);

            var actual = await _service.Update(_unitOfMeasure);

            AssertHelper.AssertAddUpdate<UnitOfMeasure>(actual);
            this._mockUnitOfMeasureRepository.Verify(x => x.Get(_unitOfMeasureId, true), Times.Exactly(1));
            this._mockUnitOfMeasureRepository.Verify(x => x.Update(_unitOfMeasure), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var unitOfMeasureId = 1000;

            this._mockUnitOfMeasureRepository.Setup(x => x.Get(unitOfMeasureId, true)).Returns(Task.FromResult((UnitOfMeasure?)null));

            var actual = await _service.Update(new UnitOfMeasure(unitOfMeasureId, String.Empty, String.Empty, String.Empty, String.Empty));

            this._mockUnitOfMeasureRepository.Verify(x => x.Get(unitOfMeasureId, true), Times.Exactly(1));
            this._mockUnitOfMeasureRepository.Verify(x => x.Update(_unitOfMeasure), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}