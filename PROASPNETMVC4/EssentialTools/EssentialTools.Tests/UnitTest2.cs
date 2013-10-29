using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;

using Moq;

//The problem we face is that the LinqValueCalculator class depends on an implementation of the
//IDiscountHelper interface to operate. In the example, we have used the MinimumDiscountHelper class,
//which presents two different issues.

//First, we have made our unit test complex and brittle. In order to create a unit test that works, we
//need to take into account the discount logic in the IDiscountHelper implementation to figure out the
//expected value from the ValueProducts method. The brittleness comes from the fact that our tests will fail
//if the discount logic in the implementation changes.

//Second, and most troubling, we have extended the scope of our unit test so that it implicitly includes
//the MinimumDiscountHelper class. When our unit test fails, we will not know if the problem is in the
//LinqValueCalculator or MinimumDiscountHelper class.



//We use the Setup method to add a method to our mock object.


//The benefit of using Moq in this way is that our unit test only checks the behavior of the
//LinqValueCalculator object and does not depend on any of the real implementations of the
//IDiscountHelper interface in the Models folder. This means that when our tests fail, we know that the
//problem is either in the LinqValueCalculator implementation or in the way we set up mock object—and
//solving a problem from either of these sources is simpler and easier than dealing with a chain of real
//objects and the interactions between them.

///**************************************
//mock.Setup()'ın içerisi herhangi bir discount yapılmamasını sağlıyor. yani
//idiscounthelper'ın implementasyonları kullanılmıyor. böylece birbirine bağlı
//sınıflarla çalışan fonksiyonlarda tek bir fonksiyonu diğerine bağlı olmadan
//kontrol etmek daha kolay oluyor.


//Creating a More Complex Mock Object
//we have used Moq to model the behavior of the MinimumDiscountHelper class.



namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] products = {
        new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
        new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
        new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
        new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };
        [TestMethod]
        public void Sum_Products_Correctly()
        {
            ////arrange
            //var discounter = new MinimumDiscountHelper();
            //var discounter = new FlexibleDiscountHelper();
            //var target = new LinqValueCalculator(discounter);
            //var goalTotal = products.Sum(e => e.Price);
            //// act
            //var result = target.ValueProducts(products);
            //// assert
            //Assert.AreEqual(goalTotal, result);


            // arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);
            // act
            var result = target.ValueProducts(products);
            // assert
            Assert.AreEqual(products.Sum(e => e.Price), result);


        }


        private Product[] createProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            // arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();

            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);

            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0))).Throws<System.ArgumentOutOfRangeException>();

            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100))).Returns<decimal>(total => (total * 0.9M));

            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100,Range.Inclusive))).Returns<decimal>(total => total - 5);

            var target = new LinqValueCalculator(mock.Object);
            // act
            decimal FiveDollarDiscount = target.ValueProducts(createProduct(5));
            decimal TenDollarDiscount = target.ValueProducts(createProduct(10));
            decimal FiftyDollarDiscount = target.ValueProducts(createProduct(50));
            decimal HundredDollarDiscount = target.ValueProducts(createProduct(100));
            decimal FiveHundredDollarDiscount = target.ValueProducts(createProduct(500));
            // assert
            Assert.AreEqual(5, FiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, TenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "$500 Fail");
            target.ValueProducts(createProduct(0));
        }
    
    }
}
