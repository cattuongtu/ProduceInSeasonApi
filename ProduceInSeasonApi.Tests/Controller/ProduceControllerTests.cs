using Moq;
using Xunit;
using ProduceInSeasonApi.Models;
using ProduceInSeasonApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProduceInSeasonApi.Tests.Controller
{
    public class ProduceControllerTests
    {

        [Fact]
        public async Task GetProduce_ShouldReturnListOfDTOs()
        {
            // Arrange
            var dbContextOptions = new Mock<DbContextOptions<ProductContext>>();
            var dbContextMock = new Mock<ProductContext>(dbContextOptions);
            var controller = new ProduceController(dbContextMock.Object);

            var products = new List<Product>
            {
                // Create sample data for your entity type
            };

            var queryableProducts = products.AsQueryable();

            var dbSetMock = new Mock<DbSet<Product>>();
            dbSetMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(queryableProducts.Provider);
            dbSetMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(queryableProducts.Expression);
            dbSetMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(queryableProducts.ElementType);
            dbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => queryableProducts.GetEnumerator());

            dbContextMock.Setup(c => c.Products).Returns(dbSetMock.Object);

            // Act
            var result = await controller.GetProduce();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var productsResult = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(okResult.Value);

            Assert.NotNull(productsResult);
            Assert.Equal(products.Count, productsResult.Count());
            // Add more specific assertions based on your DTO conversion logic
        }
    }
}
