using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models.Domain;
using SportsStore.Tests.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests.Controllers {
    public class StoreControllerTest {
        private readonly DummyApplicationDbContext _dummyContext;
        private Mock<IProductRepository> _productRepository;
        private StoreController _storeController;

        public StoreControllerTest() {
            _dummyContext = new DummyApplicationDbContext();
            _productRepository = new Mock<IProductRepository>();
            _storeController = new StoreController(_productRepository.Object);
        }

        [Fact]
        public void IndexHttpGet_ReturnsListThatCanBeBoughtOnline() {
            _productRepository.Setup(m => m.GetByAvailability(new List<Availability> { Availability.ShopAndOnline, Availability.OnlineOnly }))
                .Returns(_dummyContext.ProductsOnline);

            var products = (_storeController.Index() as ViewResult)?.Model as IEnumerable<Product>;
            Assert.Equal(10, products.Count());

        }
    }
}
