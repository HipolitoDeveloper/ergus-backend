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
    public class ProductServiceTest : BaseTest
    {
        private ProductService _service;
        private Product _product;
        private int _productId = 1;
        private int _categoryId = 2;
        private int _producerId = 1;
        private int _providerId = 3;

        public ProductServiceTest() : base()
        {
            _product = CreateObject.GetProduct(this._productId, this._producerId, this._categoryId, this._providerId);
            _service = this._autoMock.Create<ProductService>();

            MockGetCategory(this._categoryId);
            MockGetProducer(this._producerId);
            MockGetProvider(this._providerId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockProductRepository.Setup(x => x.Add(_product)).ReturnsAsync(_product);

            var actual = await _service.Add(_product);

            AssertHelper.AssertAddUpdate<Product>(actual);
            this._mockProductRepository.Verify(x => x.Add(_product), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockProductRepository.Setup(x => x.Get(_productId, true)).ReturnsAsync(_product);
            this._mockProductRepository.Setup(x => x.Update(_product)).ReturnsAsync(_product);

            var actual = await _service.Delete(_productId);

            this._mockProductRepository.Verify(x => x.Get(_productId, true), Times.Exactly(1));
            this._mockProductRepository.Verify(x => x.Update(_product), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.False(_product.Active);
            Assert.True(_product.WasRemoved);
            Assert.NotNull(_product.RemovedId);
            Assert.NotNull(_product.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var productId = 1000;

            this._mockProductRepository.Setup(x => x.Get(productId, true)).Returns(Task.FromResult((Product?)null));

            var actual = await _service.Delete(productId);

            this._mockProductRepository.Verify(x => x.Get(productId, true), Times.Exactly(1));
            this._mockProductRepository.Verify(x => x.Update(_product), Times.Exactly(0));
            Assert.Null(actual);
            Assert.True(_product.Active);
            Assert.False(_product.WasRemoved);
            Assert.Null(_product.RemovedId);
            Assert.Null(_product.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockProductRepository.Setup(x => x.Get(_productId, false)).ReturnsAsync(_product);

            var actual = await _service.Get(_productId);

            this._mockProductRepository.Verify(x => x.Get(_productId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _product);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<Product>() { _product };

            this._mockProductRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockProductRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockProductRepository.Setup(x => x.Get(_productId, true)).ReturnsAsync(_product);
            this._mockProductRepository.Setup(x => x.Update(_product)).ReturnsAsync(_product);

            var actual = await _service.Update(_product);

            AssertHelper.AssertAddUpdate<Product>(actual);
            this._mockProductRepository.Verify(x => x.Get(_productId, true), Times.Exactly(1));
            this._mockProductRepository.Verify(x => x.Update(_product), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var productId = 1000;

            this._mockProductRepository.Setup(x => x.Get(productId, true)).Returns(Task.FromResult((Product?)null));

            var actual = await _service.Update(new Product(productId, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, Infrastructure.Helpers.TipoAnuncio.None, true, null, null, null));

            this._mockProductRepository.Verify(x => x.Get(productId, true), Times.Exactly(1));
            this._mockProductRepository.Verify(x => x.Update(_product), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}