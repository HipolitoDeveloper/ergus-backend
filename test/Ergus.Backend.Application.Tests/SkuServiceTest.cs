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
    public class SkuServiceTest : BaseTest
    {
        private SkuService _service;
        private Sku _sku;
        private int _skuId = 1;
        private int _productId = 2;

        public SkuServiceTest() : base()
        {
            _sku = CreateObject.GetSku(this._skuId, this._productId);
            _service = this._autoMock.Create<SkuService>();

            MockGetProduct(this._productId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockSkuRepository.Setup(x => x.Add(_sku)).ReturnsAsync(_sku);

            var actual = await _service.Add(_sku);

            AssertHelper.AssertAddUpdate<Sku>(actual);
            this._mockSkuRepository.Verify(x => x.Add(_sku), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockSkuRepository.Setup(x => x.Get(_skuId, true)).ReturnsAsync(_sku);
            this._mockSkuRepository.Setup(x => x.Update(_sku)).ReturnsAsync(_sku);

            var actual = await _service.Delete(_skuId);

            this._mockSkuRepository.Verify(x => x.Get(_skuId, true), Times.Exactly(1));
            this._mockSkuRepository.Verify(x => x.Update(_sku), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_sku.WasRemoved);
            Assert.NotNull(_sku.RemovedId);
            Assert.NotNull(_sku.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var skuId = 1000;

            this._mockSkuRepository.Setup(x => x.Get(skuId, true)).Returns(Task.FromResult((Sku?)null));

            var actual = await _service.Delete(skuId);

            this._mockSkuRepository.Verify(x => x.Get(skuId, true), Times.Exactly(1));
            this._mockSkuRepository.Verify(x => x.Update(_sku), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_sku.WasRemoved);
            Assert.Null(_sku.RemovedId);
            Assert.Null(_sku.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockSkuRepository.Setup(x => x.Get(_skuId, false)).ReturnsAsync(_sku);

            var actual = await _service.Get(_skuId);

            this._mockSkuRepository.Verify(x => x.Get(_skuId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _sku);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<Sku>() { _sku };

            this._mockSkuRepository.Setup(x => x.GetAll()).ReturnsAsync(expected);

            var actual = await _service.GetAll();

            this._mockSkuRepository.Verify(x => x.GetAll(), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockSkuRepository.Setup(x => x.Get(_skuId, true)).ReturnsAsync(_sku);
            this._mockSkuRepository.Setup(x => x.Update(_sku)).ReturnsAsync(_sku);

            var actual = await _service.Update(_sku);

            AssertHelper.AssertAddUpdate<Sku>(actual);
            this._mockSkuRepository.Verify(x => x.Get(_skuId, true), Times.Exactly(1));
            this._mockSkuRepository.Verify(x => x.Update(_sku), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var skuId = 1000;

            this._mockSkuRepository.Setup(x => x.Get(skuId, true)).Returns(Task.FromResult((Sku?)null));

            var actual = await _service.Update(new Sku(skuId, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, 1, 2, 3, 4, 5, null));

            this._mockSkuRepository.Verify(x => x.Get(skuId, true), Times.Exactly(1));
            this._mockSkuRepository.Verify(x => x.Update(_sku), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}