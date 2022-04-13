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
    public class SkuPriceServiceTest : BaseTest
    {
        private SkuPriceService _service;
        private SkuPrice _skuPrice;
        private int _skuPriceId = 1;
        private int _priceListId = 2;
        private int _skuId = 2;

        public SkuPriceServiceTest() : base()
        {
            _skuPrice = CreateObject.GetSkuPrice(this._skuPriceId, this._priceListId, this._skuId);
            _service = this._autoMock.Create<SkuPriceService>();

            MockGetPriceList(this._priceListId);
            MockGetSku(this._skuId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockSkuPriceRepository.Setup(x => x.Add(_skuPrice)).ReturnsAsync(_skuPrice);

            var actual = await _service.Add(_skuPrice);

            AssertHelper.AssertAddUpdate<SkuPrice>(actual);
            this._mockSkuPriceRepository.Verify(x => x.Add(_skuPrice), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockSkuPriceRepository.Setup(x => x.Get(_skuPriceId, true)).ReturnsAsync(_skuPrice);
            this._mockSkuPriceRepository.Setup(x => x.Update(_skuPrice)).ReturnsAsync(_skuPrice);

            var actual = await _service.Delete(_skuPriceId);

            this._mockSkuPriceRepository.Verify(x => x.Get(_skuPriceId, true), Times.Exactly(1));
            this._mockSkuPriceRepository.Verify(x => x.Update(_skuPrice), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_skuPrice.WasRemoved);
            Assert.NotNull(_skuPrice.RemovedId);
            Assert.NotNull(_skuPrice.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var skuPriceId = 1000;

            this._mockSkuPriceRepository.Setup(x => x.Get(skuPriceId, true)).Returns(Task.FromResult((SkuPrice?)null));

            var actual = await _service.Delete(skuPriceId);

            this._mockSkuPriceRepository.Verify(x => x.Get(skuPriceId, true), Times.Exactly(1));
            this._mockSkuPriceRepository.Verify(x => x.Update(_skuPrice), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_skuPrice.WasRemoved);
            Assert.Null(_skuPrice.RemovedId);
            Assert.Null(_skuPrice.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockSkuPriceRepository.Setup(x => x.Get(_skuPriceId, false)).ReturnsAsync(_skuPrice);

            var actual = await _service.Get(_skuPriceId);

            this._mockSkuPriceRepository.Verify(x => x.Get(_skuPriceId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _skuPrice);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<SkuPrice>() { _skuPrice };

            this._mockSkuPriceRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockSkuPriceRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockSkuPriceRepository.Setup(x => x.Get(_skuPriceId, true)).ReturnsAsync(_skuPrice);
            this._mockSkuPriceRepository.Setup(x => x.Update(_skuPrice)).ReturnsAsync(_skuPrice);

            var actual = await _service.Update(_skuPrice);

            AssertHelper.AssertAddUpdate<SkuPrice>(actual);
            this._mockSkuPriceRepository.Verify(x => x.Get(_skuPriceId, true), Times.Exactly(1));
            this._mockSkuPriceRepository.Verify(x => x.Update(_skuPrice), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var skuPriceId = 1000;

            this._mockSkuPriceRepository.Setup(x => x.Get(skuPriceId, true)).Returns(Task.FromResult((SkuPrice?)null));

            var actual = await _service.Update(new SkuPrice(skuPriceId, String.Empty, String.Empty, 1, 2, DateTime.MinValue, DateTime.MaxValue, null, null));

            this._mockSkuPriceRepository.Verify(x => x.Get(skuPriceId, true), Times.Exactly(1));
            this._mockSkuPriceRepository.Verify(x => x.Update(_skuPrice), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}