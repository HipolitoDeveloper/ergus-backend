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
    public class ProductAttributeServiceTest : BaseTest
    {
        private ProductAttributeService _service;
        private ProductAttribute _productAttribute;
        private int _productAttributeId = 1;
        private int _metadataId = 2;
        private int _productId = 3;

        public ProductAttributeServiceTest() : base()
        {
            _productAttribute = CreateObject.GetProductAttribute(this._productAttributeId, this._metadataId, this._productId);
            _service = this._autoMock.Create<ProductAttributeService>();

            MockGetMetadata(this._metadataId);
            MockGetProduct(this._productId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockProductAttributeRepository.Setup(x => x.Add(_productAttribute)).ReturnsAsync(_productAttribute);

            var actual = await _service.Add(_productAttribute);

            Assert.NotNull(actual);
            Assert.True(actual!.EhValido());
            Assert.Empty(actual.Erros);
            this._mockProductAttributeRepository.Verify(x => x.Add(_productAttribute), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockProductAttributeRepository.Setup(x => x.Get(_productAttributeId, true)).ReturnsAsync(_productAttribute);
            this._mockProductAttributeRepository.Setup(x => x.Update(_productAttribute)).ReturnsAsync(_productAttribute);

            var actual = await _service.Delete(_productAttributeId);

            this._mockProductAttributeRepository.Verify(x => x.Get(_productAttributeId, true), Times.Exactly(1));
            this._mockProductAttributeRepository.Verify(x => x.Update(_productAttribute), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_productAttribute.WasRemoved);
            Assert.NotNull(_productAttribute.RemovedId);
            Assert.NotNull(_productAttribute.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var productAttributeId = 1000;

            this._mockProductAttributeRepository.Setup(x => x.Get(productAttributeId, true)).Returns(Task.FromResult((ProductAttribute?)null));

            var actual = await _service.Delete(productAttributeId);

            this._mockProductAttributeRepository.Verify(x => x.Get(productAttributeId, true), Times.Exactly(1));
            this._mockProductAttributeRepository.Verify(x => x.Update(_productAttribute), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_productAttribute.WasRemoved);
            Assert.Null(_productAttribute.RemovedId);
            Assert.Null(_productAttribute.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockProductAttributeRepository.Setup(x => x.Get(_productAttributeId, false)).ReturnsAsync(_productAttribute);

            var actual = await _service.Get(_productAttributeId);

            this._mockProductAttributeRepository.Verify(x => x.Get(_productAttributeId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _productAttribute);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<ProductAttribute>() { _productAttribute };

            this._mockProductAttributeRepository.Setup(x => x.GetAll()).ReturnsAsync(expected);

            var actual = await _service.GetAll();

            this._mockProductAttributeRepository.Verify(x => x.GetAll(), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockProductAttributeRepository.Setup(x => x.Get(_productAttributeId, true)).ReturnsAsync(_productAttribute);
            this._mockProductAttributeRepository.Setup(x => x.Update(_productAttribute)).ReturnsAsync(_productAttribute);

            var actual = await _service.Update(_productAttribute);

            Assert.NotNull(actual);
            Assert.True(actual!.EhValido());
            Assert.Empty(actual.Erros);
            this._mockProductAttributeRepository.Verify(x => x.Get(_productAttributeId, true), Times.Exactly(1));
            this._mockProductAttributeRepository.Verify(x => x.Update(_productAttribute), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var productAttributeId = 1000;

            this._mockProductAttributeRepository.Setup(x => x.Get(productAttributeId, true)).Returns(Task.FromResult((ProductAttribute?)null));

            var actual = await _service.Update(new ProductAttribute(productAttributeId, String.Empty, String.Empty, null, null));

            this._mockProductAttributeRepository.Verify(x => x.Get(productAttributeId, true), Times.Exactly(1));
            this._mockProductAttributeRepository.Verify(x => x.Update(_productAttribute), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}