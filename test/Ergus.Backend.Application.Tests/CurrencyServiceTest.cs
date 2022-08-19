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
    public class CurrencyServiceTest : BaseTest
    {
        private CurrencyService _service;
        private Currency _currency;
        private int _currencyId = 1;

        public CurrencyServiceTest() : base()
        {
            _currency = CreateObject.GetCurrency(this._currencyId);
            _service = this._autoMock.Create<CurrencyService>();
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockCurrencyRepository.Setup(x => x.Add(_currency)).ReturnsAsync(_currency);

            var actual = await _service.Add(_currency);

            AssertHelper.AssertAddUpdate<Currency>(actual);
            this._mockCurrencyRepository.Verify(x => x.Add(_currency), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockCurrencyRepository.Setup(x => x.Get(_currencyId, true)).ReturnsAsync(_currency);
            this._mockCurrencyRepository.Setup(x => x.Update(_currency)).ReturnsAsync(_currency);

            var actual = await _service.Delete(_currencyId);

            this._mockCurrencyRepository.Verify(x => x.Get(_currencyId, true), Times.Exactly(1));
            this._mockCurrencyRepository.Verify(x => x.Update(_currency), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_currency.WasRemoved);
            Assert.NotNull(_currency.RemovedId);
            Assert.NotNull(_currency.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var categoryId = 1000;

            this._mockCurrencyRepository.Setup(x => x.Get(categoryId, true)).Returns(Task.FromResult((Currency?)null));

            var actual = await _service.Delete(categoryId);

            this._mockCurrencyRepository.Verify(x => x.Get(categoryId, true), Times.Exactly(1));
            this._mockCurrencyRepository.Verify(x => x.Update(_currency), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_currency.WasRemoved);
            Assert.Null(_currency.RemovedId);
            Assert.Null(_currency.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockCurrencyRepository.Setup(x => x.Get(_currencyId, false)).ReturnsAsync(_currency);

            var actual = await _service.Get(_currencyId);

            this._mockCurrencyRepository.Verify(x => x.Get(_currencyId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _currency);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<Currency>() { _currency };

            this._mockCurrencyRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockCurrencyRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockCurrencyRepository.Setup(x => x.Get(_currencyId, true)).ReturnsAsync(_currency);
            this._mockCurrencyRepository.Setup(x => x.Update(_currency)).ReturnsAsync(_currency);

            var actual = await _service.Update(_currency);

            AssertHelper.AssertAddUpdate<Currency>(actual);
            this._mockCurrencyRepository.Verify(x => x.Get(_currencyId, true), Times.Exactly(1));
            this._mockCurrencyRepository.Verify(x => x.Update(_currency), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var currencyId = 1000;

            this._mockCurrencyRepository.Setup(x => x.Get(currencyId, true)).Returns(Task.FromResult((Currency?)null));

            var actual = await _service.Update(new Currency(currencyId, String.Empty, String.Empty, String.Empty, String.Empty));

            this._mockCurrencyRepository.Verify(x => x.Get(currencyId, true), Times.Exactly(1));
            this._mockCurrencyRepository.Verify(x => x.Update(_currency), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}