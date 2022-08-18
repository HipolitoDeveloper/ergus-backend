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
    public class PaymentFormServiceTest : BaseTest
    {
        private PaymentFormService _service;
        private PaymentForm _paymentForm;
        private int _paymentFormId = 1;
        private int _providerId = 2;

        public PaymentFormServiceTest() : base()
        {
            _paymentForm = CreateObject.GetPaymentForm(this._paymentFormId, this._providerId);
            _service = this._autoMock.Create<PaymentFormService>();

            MockGetProvider(this._providerId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockPaymentFormRepository.Setup(x => x.Add(_paymentForm)).ReturnsAsync(_paymentForm);

            var actual = await _service.Add(_paymentForm);

            AssertHelper.AssertAddUpdate<PaymentForm>(actual);
            this._mockPaymentFormRepository.Verify(x => x.Add(_paymentForm), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockPaymentFormRepository.Setup(x => x.Get(_paymentFormId, true)).ReturnsAsync(_paymentForm);
            this._mockPaymentFormRepository.Setup(x => x.Update(_paymentForm)).ReturnsAsync(_paymentForm);

            var actual = await _service.Delete(_paymentFormId);

            this._mockPaymentFormRepository.Verify(x => x.Get(_paymentFormId, true), Times.Exactly(1));
            this._mockPaymentFormRepository.Verify(x => x.Update(_paymentForm), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_paymentForm.WasRemoved);
            Assert.NotNull(_paymentForm.RemovedId);
            Assert.NotNull(_paymentForm.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var categoryId = 1000;

            this._mockPaymentFormRepository.Setup(x => x.Get(categoryId, true)).Returns(Task.FromResult((PaymentForm?)null));

            var actual = await _service.Delete(categoryId);

            this._mockPaymentFormRepository.Verify(x => x.Get(categoryId, true), Times.Exactly(1));
            this._mockPaymentFormRepository.Verify(x => x.Update(_paymentForm), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_paymentForm.WasRemoved);
            Assert.Null(_paymentForm.RemovedId);
            Assert.Null(_paymentForm.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockPaymentFormRepository.Setup(x => x.Get(_paymentFormId, false)).ReturnsAsync(_paymentForm);

            var actual = await _service.Get(_paymentFormId);

            this._mockPaymentFormRepository.Verify(x => x.Get(_paymentFormId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _paymentForm);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<PaymentForm>() { _paymentForm };

            this._mockPaymentFormRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockPaymentFormRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockPaymentFormRepository.Setup(x => x.Get(_paymentFormId, true)).ReturnsAsync(_paymentForm);
            this._mockPaymentFormRepository.Setup(x => x.Update(_paymentForm)).ReturnsAsync(_paymentForm);

            var actual = await _service.Update(_paymentForm);

            AssertHelper.AssertAddUpdate<PaymentForm>(actual);
            this._mockPaymentFormRepository.Verify(x => x.Get(_paymentFormId, true), Times.Exactly(1));
            this._mockPaymentFormRepository.Verify(x => x.Update(_paymentForm), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var paymentFormId = 1000;

            this._mockPaymentFormRepository.Setup(x => x.Get(paymentFormId, true)).Returns(Task.FromResult((PaymentForm?)null));

            var actual = await _service.Update(new PaymentForm(paymentFormId, String.Empty, String.Empty, String.Empty, true, null));

            this._mockPaymentFormRepository.Verify(x => x.Get(paymentFormId, true), Times.Exactly(1));
            this._mockPaymentFormRepository.Verify(x => x.Update(_paymentForm), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}