using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EssentialTools.Models;

#region UnitTesting
//Table 6-2. Static Assert Methods
//Method Description
//AreEqual<T>(T, T)
//AreEqual<T>(T, T, string)
//Asserts that two objects of type T have the same value.

//AreNotEqual<T>(T, T)
//AreNotEqual<T>(T, T, string)
//Asserts that two objects of type T do not have the same
//value.

//AreSame<T>(T, T)
//AreSame<T>(T, T, string)
//Asserts that two variables refer to the same object.

//AreNotSame<T>(T, T)
//AreNotSame<T>(T, T, string)
//Asserts that two variables refer to different objects.

//Fail()
//Fail(string)
//Fails an assertion—no conditions are checked.

//Inconclusive()
//Inconclusive(string)
//Indicates that the result of the unit test cannot be
//definitively established.

//IsTrue(bool)
//IsTrue(bool, string)
//Asserts that a bool value is true—most often used to
//evaluate an expression that returns a bool result.

//IsFalse(bool)
//IsFalse(bool, string)
//Asserts that a bool value is false.

//IsNull(object)
//IsNull(object, string)
//Asserts that a variable is not assigned an object
//reference.

//IsNotNull(object)
//IsNotNull(object, string)
//Asserts that a variable is assigned an object reference.

//IsInstanceOfType(object, Type)
//IsInstanceOfType(object, Type, string)
//Asserts that an object is of the specified type or is derived
//from the specified type.

//IsNotInstanceOfType(object, Type)
//IsNotInstanceOfType(object, Type, string)
//Asserts that an object is not of the specified type.



//Each of the static methods in the Assert class allows you to check some aspect of your unit test. An
//exception is thrown if an assertion fails, and this means that the entire unit test fails. Each unit test is
//treated separately, so other tests will continue to be performed.

//Each of these methods is overloaded with a version that takes a string parameter. The string is
//included as the message element of the exception if the assertion fails. The AreEqual and AreNotEqual
//methods have a number of overloads that cater to comparing specific types. For example, there is a
//version that allows strings to be compared without taking case into account.
#endregion

#region Moq
//Using Moq

//The paid-for versions of Visual Studio 2012 include support for creating mock objects through a
//feature called fakes, but we prefer to use a library called Moq, which is simple, easy-to-use and can be
//used with all Visual Studio editions, including the free ones.

#endregion




namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IDiscountHelper getTestObject()
        {
            return new MinimumDiscountHelper();
        }

        [TestMethod]
        public void Discount_Above_100()
        {
            // arrange
            IDiscountHelper target = getTestObject();
            decimal total = 200;
            // act
            var discountedTotal = target.ApplyDiscount(total);
            // assert
            Assert.AreEqual(total * 0.9M, discountedTotal);
        }

        [TestMethod]
        public void Discount_Between_10_And_100() 
        {
            //arrange
            IDiscountHelper target = getTestObject();
            // act
            decimal TenDollarDiscount = target.ApplyDiscount(10);
            decimal HundredDollarDiscount = target.ApplyDiscount(100);
            decimal FiftyDollarDiscount = target.ApplyDiscount(50);
            // assert
            Assert.AreEqual(5, TenDollarDiscount, "$10 discount is wrong");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 discount is wrong");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 discount is wrong");
        }

        [TestMethod]
        public void Discount_Less_Than_10()
        {
            //arrange
            IDiscountHelper target = getTestObject();
            // act
            decimal discount5 = target.ApplyDiscount(5);
            decimal discount0 = target.ApplyDiscount(0);
            // assert
            Assert.AreEqual(5, discount5);
            Assert.AreEqual(0, discount0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Discount_Negative_Total()
        {
            //arrange
            IDiscountHelper target = getTestObject();
            // act
            target.ApplyDiscount(-1);
        }

    }
}
