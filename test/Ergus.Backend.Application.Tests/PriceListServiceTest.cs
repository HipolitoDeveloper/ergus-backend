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
    public class PriceListServiceTest : BaseTest
    {
        private PriceListService _service;
        private PriceList _priceList;
        private int _priceListId = 1;
        private int _parentId = 2;

        public PriceListServiceTest() : base()
        {
            _priceList = CreateObject.GetPriceList(this._priceListId, this._parentId);
            _service = this._autoMock.Create<PriceListService>();

            MockGetPriceList(this._parentId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockPriceListRepository.Setup(x => x.Add(_priceList)).ReturnsAsync(_priceList);

            var actual = await _service.Add(_priceList);

            Assert.NotNull(actual);
            Assert.True(actual!.EhValido());
            Assert.Empty(actual.Erros);
            this._mockPriceListRepository.Verify(x => x.Add(_priceList), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockPriceListRepository.Setup(x => x.Get(_priceListId, true)).ReturnsAsync(_priceList);
            this._mockPriceListRepository.Setup(x => x.Update(_priceList)).ReturnsAsync(_priceList);

            var actual = await _service.Delete(_priceListId);

            this._mockPriceListRepository.Verify(x => x.Get(_priceListId, true), Times.Exactly(1));
            this._mockPriceListRepository.Verify(x => x.Update(_priceList), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_priceList.WasRemoved);
            Assert.NotNull(_priceList.RemovedId);
            Assert.NotNull(_priceList.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var priceListId = 1000;

            this._mockPriceListRepository.Setup(x => x.Get(priceListId, true)).Returns(Task.FromResult((PriceList?)null));

            var actual = await _service.Delete(priceListId);

            this._mockPriceListRepository.Verify(x => x.Get(priceListId, true), Times.Exactly(1));
            this._mockPriceListRepository.Verify(x => x.Update(_priceList), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_priceList.WasRemoved);
            Assert.Null(_priceList.RemovedId);
            Assert.Null(_priceList.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockPriceListRepository.Setup(x => x.Get(_priceListId, false)).ReturnsAsync(_priceList);

            var actual = await _service.Get(_priceListId);

            this._mockPriceListRepository.Verify(x => x.Get(_priceListId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _priceList);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<PriceList>() { _priceList };

            this._mockPriceListRepository.Setup(x => x.GetAll()).ReturnsAsync(expected);

            var actual = await _service.GetAll();

            this._mockPriceListRepository.Verify(x => x.GetAll(), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockPriceListRepository.Setup(x => x.Get(_priceListId, true)).ReturnsAsync(_priceList);
            this._mockPriceListRepository.Setup(x => x.Update(_priceList)).ReturnsAsync(_priceList);

            var actual = await _service.Update(_priceList);

            Assert.NotNull(actual);
            Assert.True(actual!.EhValido());
            Assert.Empty(actual.Erros);
            this._mockPriceListRepository.Verify(x => x.Get(_priceListId, true), Times.Exactly(1));
            this._mockPriceListRepository.Verify(x => x.Update(_priceList), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var priceListId = 1000;

            this._mockPriceListRepository.Setup(x => x.Get(priceListId, true)).Returns(Task.FromResult((PriceList?)null));

            var actual = await _service.Update(new PriceList(priceListId, String.Empty, String.Empty, DateTime.MinValue, DateTime.MaxValue, String.Empty, 1, Infrastructure.Helpers.TipoListaPreco.Dinamico, Infrastructure.Helpers.TipoAjuste.ValorFixo, Infrastructure.Helpers.TipoOperacao.Adicao, 1, null));

            this._mockPriceListRepository.Verify(x => x.Get(priceListId, true), Times.Exactly(1));
            this._mockPriceListRepository.Verify(x => x.Update(_priceList), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}