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
    public class StockUnitServiceTest : BaseTest
    {
        private StockUnitService _service;
        private StockUnit _stockUnit;
        private int _stockUnitId = 1;

        public StockUnitServiceTest() : base()
        {
            _stockUnit = CreateObject.GetStockUnit(this._stockUnitId);
            _service = this._autoMock.Create<StockUnitService>();
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockStockUnitRepository.Setup(x => x.Add(_stockUnit)).ReturnsAsync(_stockUnit);

            var actual = await _service.Add(_stockUnit);

            AssertHelper.AssertAddUpdate<StockUnit>(actual);
            this._mockStockUnitRepository.Verify(x => x.Add(_stockUnit), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockStockUnitRepository.Setup(x => x.Get(_stockUnitId, true)).ReturnsAsync(_stockUnit);
            this._mockStockUnitRepository.Setup(x => x.Update(_stockUnit)).ReturnsAsync(_stockUnit);

            var actual = await _service.Delete(_stockUnitId);

            this._mockStockUnitRepository.Verify(x => x.Get(_stockUnitId, true), Times.Exactly(1));
            this._mockStockUnitRepository.Verify(x => x.Update(_stockUnit), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_stockUnit.WasRemoved);
            Assert.NotNull(_stockUnit.RemovedId);
            Assert.NotNull(_stockUnit.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var categoryId = 1000;

            this._mockStockUnitRepository.Setup(x => x.Get(categoryId, true)).Returns(Task.FromResult((StockUnit?)null));

            var actual = await _service.Delete(categoryId);

            this._mockStockUnitRepository.Verify(x => x.Get(categoryId, true), Times.Exactly(1));
            this._mockStockUnitRepository.Verify(x => x.Update(_stockUnit), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_stockUnit.WasRemoved);
            Assert.Null(_stockUnit.RemovedId);
            Assert.Null(_stockUnit.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockStockUnitRepository.Setup(x => x.Get(_stockUnitId, false)).ReturnsAsync(_stockUnit);

            var actual = await _service.Get(_stockUnitId);

            this._mockStockUnitRepository.Verify(x => x.Get(_stockUnitId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _stockUnit);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<StockUnit>() { _stockUnit };

            this._mockStockUnitRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockStockUnitRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockStockUnitRepository.Setup(x => x.Get(_stockUnitId, true)).ReturnsAsync(_stockUnit);
            this._mockStockUnitRepository.Setup(x => x.Update(_stockUnit)).ReturnsAsync(_stockUnit);

            var actual = await _service.Update(_stockUnit);

            AssertHelper.AssertAddUpdate<StockUnit>(actual);
            this._mockStockUnitRepository.Verify(x => x.Get(_stockUnitId, true), Times.Exactly(1));
            this._mockStockUnitRepository.Verify(x => x.Update(_stockUnit), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var stockUnitId = 1000;

            this._mockStockUnitRepository.Setup(x => x.Get(stockUnitId, true)).Returns(Task.FromResult((StockUnit?)null));

            var actual = await _service.Update(new StockUnit(stockUnitId, String.Empty, String.Empty, String.Empty));

            this._mockStockUnitRepository.Verify(x => x.Get(stockUnitId, true), Times.Exactly(1));
            this._mockStockUnitRepository.Verify(x => x.Update(_stockUnit), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}