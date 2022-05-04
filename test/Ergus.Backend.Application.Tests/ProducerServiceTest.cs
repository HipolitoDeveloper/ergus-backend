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
    public class ProducerServiceTest : BaseTest
    {
        private ProducerService _service;
        private Producer _producer;
        private int _producerId = 1;
        private int _addressId = 2;

        public ProducerServiceTest() : base()
        {
            var address = CreateObject.GetAddress(null);

            _producer = CreateObject.GetProducer(this._producerId, address);
            _service = this._autoMock.Create<ProducerService>();

            MockGetAddress(this._addressId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockProducerRepository.Setup(x => x.Add(_producer)).ReturnsAsync(_producer);

            var actual = await _service.Add(_producer);

            AssertHelper.AssertAddUpdate<Producer>(actual);
            this._mockProducerRepository.Verify(x => x.Add(_producer), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockProducerRepository.Setup(x => x.Get(_producerId, true)).ReturnsAsync(_producer);
            this._mockProducerRepository.Setup(x => x.Update(_producer)).ReturnsAsync(_producer);

            var actual = await _service.Delete(_producerId);

            this._mockProducerRepository.Verify(x => x.Get(_producerId, true), Times.Exactly(1));
            this._mockProducerRepository.Verify(x => x.Update(_producer), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.False(_producer.Active);
            Assert.True(_producer.WasRemoved);
            Assert.NotNull(_producer.RemovedId);
            Assert.NotNull(_producer.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var producerId = 1000;

            this._mockProducerRepository.Setup(x => x.Get(producerId, true)).Returns(Task.FromResult((Producer?)null));

            var actual = await _service.Delete(producerId);

            this._mockProducerRepository.Verify(x => x.Get(producerId, true), Times.Exactly(1));
            this._mockProducerRepository.Verify(x => x.Update(_producer), Times.Exactly(0));
            Assert.Null(actual);
            Assert.True(_producer.Active);
            Assert.False(_producer.WasRemoved);
            Assert.Null(_producer.RemovedId);
            Assert.Null(_producer.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockProducerRepository.Setup(x => x.Get(_producerId, false)).ReturnsAsync(_producer);

            var actual = await _service.Get(_producerId);

            this._mockProducerRepository.Verify(x => x.Get(_producerId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _producer);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<Producer>() { _producer };

            this._mockProducerRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockProducerRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockProducerRepository.Setup(x => x.Get(_producerId, true)).ReturnsAsync(_producer);
            this._mockProducerRepository.Setup(x => x.Update(_producer)).ReturnsAsync(_producer);

            var actual = await _service.Update(_producer);

            AssertHelper.AssertAddUpdate<Producer>(actual);
            this._mockProducerRepository.Verify(x => x.Get(_producerId, true), Times.Exactly(1));
            this._mockProducerRepository.Verify(x => x.Update(_producer), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var producerId = 1000;

            this._mockProducerRepository.Setup(x => x.Get(producerId, true)).Returns(Task.FromResult((Producer?)null));

            var actual = await _service.Update(new Producer(producerId, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, Infrastructure.Helpers.TipoPessoa.Isento, true, null));

            this._mockProducerRepository.Verify(x => x.Get(producerId, true), Times.Exactly(1));
            this._mockProducerRepository.Verify(x => x.Update(_producer), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}