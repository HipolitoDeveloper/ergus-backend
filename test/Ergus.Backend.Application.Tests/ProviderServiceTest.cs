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
    public class ProviderServiceTest : BaseTest
    {
        private ProviderService _service;
        private Provider _provider;
        private int _providerId = 1;
        private int _addressId = 2;

        public ProviderServiceTest() : base()
        {
            var address = CreateObject.GetAddress(null);

            _provider = CreateObject.GetProvider(this._providerId, address);
            _service = this._autoMock.Create<ProviderService>();

            MockGetAddress(this._addressId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockProviderRepository.Setup(x => x.Add(_provider)).ReturnsAsync(_provider);

            var actual = await _service.Add(_provider);

            AssertHelper.AssertAddUpdate<Provider>(actual);
            this._mockProviderRepository.Verify(x => x.Add(_provider), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockProviderRepository.Setup(x => x.Get(_providerId, true)).ReturnsAsync(_provider);
            this._mockProviderRepository.Setup(x => x.Update(_provider)).ReturnsAsync(_provider);

            var actual = await _service.Delete(_providerId);

            this._mockProviderRepository.Verify(x => x.Get(_providerId, true), Times.Exactly(1));
            this._mockProviderRepository.Verify(x => x.Update(_provider), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.False(_provider.Active);
            Assert.True(_provider.WasRemoved);
            Assert.NotNull(_provider.RemovedId);
            Assert.NotNull(_provider.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var providerId = 1000;

            this._mockProviderRepository.Setup(x => x.Get(providerId, true)).Returns(Task.FromResult((Provider?)null));

            var actual = await _service.Delete(providerId);

            this._mockProviderRepository.Verify(x => x.Get(providerId, true), Times.Exactly(1));
            this._mockProviderRepository.Verify(x => x.Update(_provider), Times.Exactly(0));
            Assert.Null(actual);
            Assert.True(_provider.Active);
            Assert.False(_provider.WasRemoved);
            Assert.Null(_provider.RemovedId);
            Assert.Null(_provider.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockProviderRepository.Setup(x => x.Get(_providerId, false)).ReturnsAsync(_provider);

            var actual = await _service.Get(_providerId);

            this._mockProviderRepository.Verify(x => x.Get(_providerId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _provider);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<Provider>() { _provider };

            this._mockProviderRepository.Setup(x => x.GetAll()).ReturnsAsync(expected);

            var actual = await _service.GetAll();

            this._mockProviderRepository.Verify(x => x.GetAll(), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockProviderRepository.Setup(x => x.Get(_providerId, true)).ReturnsAsync(_provider);
            this._mockProviderRepository.Setup(x => x.Update(_provider)).ReturnsAsync(_provider);

            var actual = await _service.Update(_provider);

            AssertHelper.AssertAddUpdate<Provider>(actual);
            this._mockProviderRepository.Verify(x => x.Get(_providerId, true), Times.Exactly(1));
            this._mockProviderRepository.Verify(x => x.Update(_provider), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var providerId = 1000;

            this._mockProviderRepository.Setup(x => x.Get(providerId, true)).Returns(Task.FromResult((Provider?)null));

            var actual = await _service.Update(new Provider(providerId, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, Infrastructure.Helpers.TipoPessoa.Isento, true, null));

            this._mockProviderRepository.Verify(x => x.Get(providerId, true), Times.Exactly(1));
            this._mockProviderRepository.Verify(x => x.Update(_provider), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}