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
    public class CategoryServiceTest : BaseTest
    {
        private CategoryService _service;
        private Category _category;
        private int _categoryId = 1;
        private int _parentId = 2;

        public CategoryServiceTest() : base()
        {
            var categoryText = CreateObject.GetCategoryText(null);
            _category = CreateObject.GetCategory(this._categoryId, this._parentId, categoryText);
            _service = this._autoMock.Create<CategoryService>();

            MockGetCategory(this._parentId);
        }

        [Fact]
        public async Task ShouldAddSuccessfully()
        {
            this._mockCategoryRepository.Setup(x => x.Add(_category)).ReturnsAsync(_category);

            var actual = await _service.Add(_category);

            AssertHelper.AssertAddUpdate<Category>(actual);
            this._mockCategoryRepository.Verify(x => x.Add(_category), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            this._mockCategoryRepository.Setup(x => x.Get(_categoryId, true)).ReturnsAsync(_category);
            this._mockCategoryRepository.Setup(x => x.Update(_category)).ReturnsAsync(_category);

            var actual = await _service.Delete(_categoryId);

            this._mockCategoryRepository.Verify(x => x.Get(_categoryId, true), Times.Exactly(1));
            this._mockCategoryRepository.Verify(x => x.Update(_category), Times.Exactly(1));
            Assert.NotNull(actual);
            Assert.False(_category.Active);
            Assert.True(_category.WasRemoved);
            Assert.NotNull(_category.RemovedId);
            Assert.NotNull(_category.RemovedDate);
        }

        [Fact]
        public async Task ShouldDeleteUnsuccessfully_WhenIdNotExists()
        {
            var categoryId = 1000;

            this._mockCategoryRepository.Setup(x => x.Get(categoryId, true)).Returns(Task.FromResult((Category?)null));

            var actual = await _service.Delete(categoryId);

            this._mockCategoryRepository.Verify(x => x.Get(categoryId, true), Times.Exactly(1));
            this._mockCategoryRepository.Verify(x => x.Update(_category), Times.Exactly(0));
            Assert.Null(actual);
            Assert.True(_category.Active);
            Assert.False(_category.WasRemoved);
            Assert.Null(_category.RemovedId);
            Assert.Null(_category.RemovedDate);
        }

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            this._mockCategoryRepository.Setup(x => x.Get(_categoryId, false)).ReturnsAsync(_category);

            var actual = await _service.Get(_categoryId);

            this._mockCategoryRepository.Verify(x => x.Get(_categoryId, false), Times.Exactly(1));

            Assert.NotNull(actual);
            AssertHelper.AssertEqual(actual!, _category);
        }

        [Fact]
        public async Task ShouldGetAllSuccessfully()
        {
            var expected = new List<Category>() { _category };

            this._mockCategoryRepository.Setup(x => x.GetAll()).ReturnsAsync(expected);

            var actual = await _service.GetAll();

            this._mockCategoryRepository.Verify(x => x.GetAll(), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            this._mockCategoryRepository.Setup(x => x.Get(_categoryId, true)).ReturnsAsync(_category);
            this._mockCategoryRepository.Setup(x => x.Update(_category)).ReturnsAsync(_category);

            var actual = await _service.Update(_category);

            AssertHelper.AssertAddUpdate<Category>(actual);
            this._mockCategoryRepository.Verify(x => x.Get(_categoryId, true), Times.Exactly(1));
            this._mockCategoryRepository.Verify(x => x.Update(_category), Times.Exactly(1));
        }

        [Fact]
        public async Task ShouldUpdateUnsuccessfully_WhenObjectNotExists()
        {
            var categoryId = 1000;

            this._mockCategoryRepository.Setup(x => x.Get(categoryId, true)).Returns(Task.FromResult((Category?)null));

            var actual = await _service.Update(new Category(categoryId, String.Empty, String.Empty, String.Empty, true, null, null));

            this._mockCategoryRepository.Verify(x => x.Get(categoryId, true), Times.Exactly(1));
            this._mockCategoryRepository.Verify(x => x.Update(_category), Times.Exactly(0));
            Assert.Null(actual);
        }
    }
}