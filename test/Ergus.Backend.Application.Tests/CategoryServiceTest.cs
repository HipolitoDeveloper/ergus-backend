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

            this._mockCategoryRepository.Setup(x => x.GetAll(0, 0, true)).ReturnsAsync(expected);

            var actual = await _service.GetAll(0, 0, true);

            this._mockCategoryRepository.Verify(x => x.GetAll(0, 0, true), Times.Exactly(1));

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < actual.Count; i++)
                AssertHelper.AssertEqual(expected[i], actual[i]);
        }

        [Fact]
        public async Task ShouldGetCategoryTreeSuccessfully()
        {
            var categoryList = new List<CategoryTree>()
            {
                new CategoryTree(1, "Categoria 2", null),
                new CategoryTree(2, "Categoria 1", null),
                new CategoryTree(3, "Categoria 1.1", 2),
                new CategoryTree(4, "Categoria 1.2", 2),
                new CategoryTree(5, "Categoria 1.1.1", 3),
                new CategoryTree(6, "Categoria 2.1", 1),
                new CategoryTree(7, "Categoria 2.2", 1),
                new CategoryTree(8, "Categoria 4", null),
                new CategoryTree(9, "Categoria 3.2", 13),
                new CategoryTree(10, "Categoria 3.1", 13),
                new CategoryTree(11, "Categoria 3.1.1", 10),
                new CategoryTree(12, "Categoria 1.1.1.1", 5),
                new CategoryTree(13, "Categoria 3", null),
            };
            categoryList.Shuffle();

            #region [ Montando lista esperada ]

            var expectedCategoria1 = new CategoryTree(2, "Categoria 1", null);
            var expectedCategoria11 = new CategoryTree(3, "Categoria 1.1", 2);
            var expectedCategoria111 = new CategoryTree(5, "Categoria 1.1.1", 3);
            var expectedCategoria1111 = new CategoryTree(12, "Categoria 1.1.1.1", 5);
            var expectedCategoria12 = new CategoryTree(4, "Categoria 1.2", 2);

            expectedCategoria111.Children.Add(expectedCategoria1111);
            expectedCategoria11.Children.Add(expectedCategoria111);
            expectedCategoria1.Children.Add(expectedCategoria11);
            expectedCategoria1.Children.Add(expectedCategoria12);


            var expectedCategoria2 = new CategoryTree(1, "Categoria 2", null);
            var expectedCategoria21 = new CategoryTree(6, "Categoria 2.1", 1);
            var expectedCategoria22 = new CategoryTree(7, "Categoria 2.2", 1);

            expectedCategoria2.Children.Add(expectedCategoria21);
            expectedCategoria2.Children.Add(expectedCategoria22);


            var expectedCategoria3 = new CategoryTree(13, "Categoria 3", null);
            var expectedCategoria31 = new CategoryTree(10, "Categoria 3.1", 13);
            var expectedCategoria311 = new CategoryTree(11, "Categoria 3.1.1", 10);
            var expectedCategoria32 = new CategoryTree(9, "Categoria 3.2", 13);

            expectedCategoria31.Children.Add(expectedCategoria311);
            expectedCategoria3.Children.Add(expectedCategoria31);
            expectedCategoria3.Children.Add(expectedCategoria32);

            var expected = new List<CategoryTree>()
            {
                expectedCategoria1,
                expectedCategoria2,
                expectedCategoria3,
                new CategoryTree(8, "Categoria 4", null)
            };

            #endregion [ FIM - Montando lista esperada ]

            var actual = await this._service.GetAllTree(categoryList);

            Assert.Equal(expected.Count, actual.Count);

            for(var i = 0; i <= expected.Count - 1; i++)
            {
                var itemActual = actual[i];
                var itemExpected = expected[i];

                ValidateTreenode(itemActual, itemExpected);
            }
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

        private void ValidateTreenode(CategoryTree actual, CategoryTree expected)
        {
            Assert.Equal(actual.Id, expected.Id);
            Assert.Equal(actual.Name, expected.Name);
            Assert.Equal(actual.Children.Count, expected.Children.Count);

            for (var i = 0; i <= expected.Children.Count - 1; i++)
            {
                var itemActual = actual.Children[i];
                var itemExpected = expected.Children[i];

                ValidateTreenode(itemActual, itemExpected);
            }
        }
    }
}