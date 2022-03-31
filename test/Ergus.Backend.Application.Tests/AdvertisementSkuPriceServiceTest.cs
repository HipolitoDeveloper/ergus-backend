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
    public class AdvertisementSkuPriceServiceTest : BaseTest
    {
        private AdvertisementSkuPriceService _service;
        private AdvertisementSkuPrice _advertisementSkuPrice;
        private int _advertisementSkuPriceId = 1;
        private int _advertisementSkuId = 1;
        private int _priceListId = 2;

        public AdvertisementSkuPriceServiceTest() : base()
        {
            _advertisementSkuPrice = CreateObject.GetAdvertisementSkuPrice(this._advertisementSkuPriceId, this._priceListId, this._advertisementSkuId);
            _service = this._autoMock.Create<AdvertisementSkuPriceService>();

            MockGetPriceList(this._priceListId);
            MockGetAdvertisementSku(this._advertisementSkuId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockAdvertisementSkuPriceRepository.Setup(x => x.Add(_advertisementSkuPrice)).ReturnsAsync(_advertisementSkuPrice);

            var actual = await _service.Add(_advertisementSkuPrice);

            this._mockAdvertisementSkuPriceRepository.Verify(x => x.Add(_advertisementSkuPrice), Times.Exactly(1));
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockAdvertisementSkuPriceRepository.Setup(x => x.Get(_advertisementSkuPriceId, true)).ReturnsAsync(_advertisementSkuPrice);
            this._mockAdvertisementSkuPriceRepository.Setup(x => x.Update(_advertisementSkuPrice)).ReturnsAsync(_advertisementSkuPrice);

            var actual = await _service.Delete(_advertisementSkuPriceId);

            this._mockAdvertisementSkuPriceRepository.Verify(x => x.Get(_advertisementSkuPriceId, true), Times.Exactly(1));
            this._mockAdvertisementSkuPriceRepository.Verify(x => x.Update(_advertisementSkuPrice), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_advertisementSkuPrice.WasRemoved);
            Assert.NotNull(_advertisementSkuPrice.RemovedId);
            Assert.NotNull(_advertisementSkuPrice.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var advertisementSkuPriceId = 1000;

            this._mockAdvertisementSkuPriceRepository.Setup(x => x.Get(advertisementSkuPriceId, true)).Returns(Task.FromResult((AdvertisementSkuPrice?)null));

            var actual = await _service.Delete(advertisementSkuPriceId);

            this._mockAdvertisementSkuPriceRepository.Verify(x => x.Get(advertisementSkuPriceId, true), Times.Exactly(1));
            this._mockAdvertisementSkuPriceRepository.Verify(x => x.Update(_advertisementSkuPrice), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_advertisementSkuPrice.WasRemoved);
            Assert.Null(_advertisementSkuPrice.RemovedId);
            Assert.Null(_advertisementSkuPrice.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockAdvertisementSkuPriceRepository.Setup(x => x.Get(_advertisementSkuPriceId, false)).ReturnsAsync(_advertisementSkuPrice);

            var actual = await _service.Get(_advertisementSkuPriceId);

            this._mockAdvertisementSkuPriceRepository.Verify(x => x.Get(_advertisementSkuPriceId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _advertisementSkuPrice);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<AdvertisementSkuPrice>() { _advertisementSkuPrice };

            this._mockAdvertisementSkuPriceRepository.Setup(x => x.GetAll()).ReturnsAsync(expected);

            var actual = await _service.GetAll();

            this._mockAdvertisementSkuPriceRepository.Verify(x => x.GetAll(), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockAdvertisementSkuPriceRepository.Setup(x => x.Get(_advertisementSkuPriceId, true)).ReturnsAsync(_advertisementSkuPrice);
            this._mockAdvertisementSkuPriceRepository.Setup(x => x.Update(_advertisementSkuPrice)).ReturnsAsync(_advertisementSkuPrice);

            var actual = await _service.Update(_advertisementSkuPrice);

            this._mockAdvertisementSkuPriceRepository.Verify(x => x.Get(_advertisementSkuPriceId, true), Times.Exactly(1));
            this._mockAdvertisementSkuPriceRepository.Verify(x => x.Update(_advertisementSkuPrice), Times.Exactly(1));
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var advertisementSkuPriceId = 1000;

            this._mockAdvertisementSkuPriceRepository.Setup(x => x.Get(advertisementSkuPriceId, true)).Returns(Task.FromResult((AdvertisementSkuPrice?)null));

            var actual = await _service.Update(new AdvertisementSkuPrice(advertisementSkuPriceId, String.Empty, String.Empty, 0, 0, DateTime.MinValue, DateTime.MaxValue, null, null));

            this._mockAdvertisementSkuPriceRepository.Verify(x => x.Get(advertisementSkuPriceId, true), Times.Exactly(1));
            this._mockAdvertisementSkuPriceRepository.Verify(x => x.Update(_advertisementSkuPrice), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}