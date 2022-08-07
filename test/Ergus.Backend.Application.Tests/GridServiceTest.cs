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
    public class GridServiceTest : BaseTest
    {
        private GridService _service;
        private Grid _grid;
        private int _gridId = 1;

        public GridServiceTest() : base()
        {
            _grid = CreateObject.GetGrid(this._gridId);
            _service = this._autoMock.Create<GridService>();
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockGridRepository.Setup(x => x.Add(_grid)).ReturnsAsync(_grid);

            var actual = await _service.Add(_grid);

            AssertHelper.AssertAddUpdate<Grid>(actual);
            this._mockGridRepository.Verify(x => x.Add(_grid), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockGridRepository.Setup(x => x.Get(_gridId, true)).ReturnsAsync(_grid);
            this._mockGridRepository.Setup(x => x.Update(_grid)).ReturnsAsync(_grid);

            var actual = await _service.Delete(_gridId);

            this._mockGridRepository.Verify(x => x.Get(_gridId, true), Times.Exactly(1));
            this._mockGridRepository.Verify(x => x.Update(_grid), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.True(_grid.WasRemoved);
            Assert.NotNull(_grid.RemovedId);
            Assert.NotNull(_grid.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var categoryId = 1000;

            this._mockGridRepository.Setup(x => x.Get(categoryId, true)).Returns(Task.FromResult((Grid?)null));

            var actual = await _service.Delete(categoryId);

            this._mockGridRepository.Verify(x => x.Get(categoryId, true), Times.Exactly(1));
            this._mockGridRepository.Verify(x => x.Update(_grid), Times.Exactly(0));
            Assert.Null(actual);
            Assert.False(_grid.WasRemoved);
            Assert.Null(_grid.RemovedId);
            Assert.Null(_grid.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockGridRepository.Setup(x => x.Get(_gridId, false)).ReturnsAsync(_grid);

            var actual = await _service.Get(_gridId);

            this._mockGridRepository.Verify(x => x.Get(_gridId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _grid);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<Grid>() { _grid };

            this._mockGridRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockGridRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockGridRepository.Setup(x => x.Get(_gridId, true)).ReturnsAsync(_grid);
            this._mockGridRepository.Setup(x => x.Update(_grid)).ReturnsAsync(_grid);

            var actual = await _service.Update(_grid);

            AssertHelper.AssertAddUpdate<Grid>(actual);
            this._mockGridRepository.Verify(x => x.Get(_gridId, true), Times.Exactly(1));
            this._mockGridRepository.Verify(x => x.Update(_grid), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var gridId = 1000;

            this._mockGridRepository.Setup(x => x.Get(gridId, true)).Returns(Task.FromResult((Grid?)null));

            var actual = await _service.Update(new Grid(gridId, String.Empty, String.Empty, String.Empty));

            this._mockGridRepository.Verify(x => x.Get(gridId, true), Times.Exactly(1));
            this._mockGridRepository.Verify(x => x.Update(_grid), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}