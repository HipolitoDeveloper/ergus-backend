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
    public class UserServiceTest : BaseTest
    {
        private UserService _service;
        private User _user;
        private int _userId = 1;

        public UserServiceTest() : base()
        {
            _user = CreateObject.GetUser(this._userId);
            _service = this._autoMock.Create<UserService>();
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockUserRepository.Setup(x => x.Get(_userId, false)).ReturnsAsync(_user);

            var actual = await _service.Get(_userId);

            this._mockUserRepository.Verify(x => x.Get(_userId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _user);
        }

        [Fact]
        public async Task ShouldGetByLoginSuccessfully()
        {
            var login = "Login";
            this._mockUserRepository.Setup(x => x.GetByLogin(login)).ReturnsAsync(_user);

            var actual = await _service.GetByLogin(login);

            this._mockUserRepository.Verify(x => x.GetByLogin(login), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _user);
        }

        [Fact]
        public async Task ShouldHandleUserTokensSuccessfully()
        {
            var encryptedAccessToken = "encryptedAccessToken";
            var userTokens = new List<UserToken>();
            this._mockUserRepository.Setup(x => x.GetUserTokensByUserId(_userId)).ReturnsAsync(userTokens);
            this._mockUserRepository.Setup(x => x.RemoveOldUserTokens(userTokens)).Returns(Task.CompletedTask);
            this._mockUserRepository.Setup(x => x.AddUserToken(It.IsAny<UserToken>())).Returns(Task.CompletedTask);

            var actual = await _service.HandleUserTokens(_userId, encryptedAccessToken);

            this._mockUserRepository.Verify(x => x.GetUserTokensByUserId(_userId), Times.Exactly(1));
            this._mockUserRepository.Verify(x => x.RemoveOldUserTokens(userTokens), Times.Exactly(1));
            this._mockUserRepository.Verify(x => x.AddUserToken(It.IsAny<UserToken>()), Times.Exactly(1));

            Assert.True(actual);
        }
    }
}