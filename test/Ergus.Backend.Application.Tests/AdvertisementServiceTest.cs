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
    public class AdvertisementServiceTest : BaseTest
    {
        private AdvertisementService _service;
        private Advertisement _advertisement;
        private int _advertisementId = 1;
        private int _integrationId = 2;
        private int _productId = 1;

        public AdvertisementServiceTest() : base()
        {
            _advertisement = CreateObject.GetAdvertisement(this._advertisementId, this._integrationId, this._productId);
            _service = this._autoMock.Create<AdvertisementService>();

            MockGetIntegration(this._integrationId);
            MockGetProduct(this._productId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockAdvertisementRepository.Setup(x => x.Add(_advertisement)).ReturnsAsync(_advertisement);

            var actual = await _service.Add(_advertisement);

            AssertHelper.AssertAddUpdate<Advertisement>(actual);
            this._mockAdvertisementRepository.Verify(x => x.Add(_advertisement), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockAdvertisementRepository.Setup(x => x.Get(_advertisementId, true)).ReturnsAsync(_advertisement);
            this._mockAdvertisementRepository.Setup(x => x.Update(_advertisement)).ReturnsAsync(_advertisement);

            var actual = await _service.Delete(_advertisementId);

            this._mockAdvertisementRepository.Verify(x => x.Get(_advertisementId, true), Times.Exactly(1));
            this._mockAdvertisementRepository.Verify(x => x.Update(_advertisement), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_advertisement.WasRemoved);
            Assert.NotNull(_advertisement.RemovedId);
            Assert.NotNull(_advertisement.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var advertisementId = 1000;

            this._mockAdvertisementRepository.Setup(x => x.Get(advertisementId, true)).Returns(Task.FromResult((Advertisement?)null));

            var actual = await _service.Delete(advertisementId);

            this._mockAdvertisementRepository.Verify(x => x.Get(advertisementId, true), Times.Exactly(1));
            this._mockAdvertisementRepository.Verify(x => x.Update(_advertisement), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_advertisement.WasRemoved);
            Assert.Null(_advertisement.RemovedId);
            Assert.Null(_advertisement.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockAdvertisementRepository.Setup(x => x.Get(_advertisementId, false)).ReturnsAsync(_advertisement);

            var actual = await _service.Get(_advertisementId);

            this._mockAdvertisementRepository.Verify(x => x.Get(_advertisementId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _advertisement);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<Advertisement>() { _advertisement };

            this._mockAdvertisementRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockAdvertisementRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockAdvertisementRepository.Setup(x => x.Get(_advertisementId, true)).ReturnsAsync(_advertisement);
            this._mockAdvertisementRepository.Setup(x => x.Update(_advertisement)).ReturnsAsync(_advertisement);

            var actual = await _service.Update(_advertisement);

            AssertHelper.AssertAddUpdate<Advertisement>(actual);
            this._mockAdvertisementRepository.Verify(x => x.Get(_advertisementId, true), Times.Exactly(1));
            this._mockAdvertisementRepository.Verify(x => x.Update(_advertisement), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var advertisementId = 1000;

            this._mockAdvertisementRepository.Setup(x => x.Get(advertisementId, true)).Returns(Task.FromResult((Advertisement?)null));

            var actual = await _service.Update(new Advertisement(advertisementId, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, Infrastructure.Helpers.TipoAnuncio.None, Infrastructure.Helpers.TipoStatusAnuncio.Inativo, null, null));

            this._mockAdvertisementRepository.Verify(x => x.Get(advertisementId, true), Times.Exactly(1));
            this._mockAdvertisementRepository.Verify(x => x.Update(_advertisement), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}