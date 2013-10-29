using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Configuration;
using EssentialTools.Models;


//The MVC Framework uses a dependency resolver to create instances of the classes it needs to service requests. 
//By creating a custom resolver, we ensure that Ninject is used whenever an object is going to be created.

//The MVC Framework will call the GetService or GetServices methods when it needs an instance of a
//class to service an incoming request.

//We have to tell the MVC Framework that we want to use our own dependency resolver, which we do by
//modifying the Global.asax.cs file.


////**************************************** ÖNEMLİ
//What we have created is an example of constructor injection, which is one form of dependency
//injection. Here is what happened when you ran the example app and Internet Explorer made the request
//for the root URL of the app:
////**************************************** 

//1. The MVC Framework received the request and figured out that the request is
//intended for the Home controller (we will explain how the MVC framework figures
//this stuff out in Chapter 15).

//2. The MVC Framework asked our custom dependency resolver class to create a
//new instance of the HomeController class, specifying the class using the Type
//parameter of the GetService method.

//3. Our dependency resolver asked Ninject to create a new HomeController class by
//passing on the Type object to the TryGet method.

//4. Ninject inspected the HomeController constructor and found that it requires an
//IValueCalculator implementation, which it knows that it has a binding for.

//5. Ninject creates an instance of the LinqValueCalculator class and uses it to
//create a new instance of the HomeController class.

//6. Ninject passes the newly-created HomeController instance to the custom
//dependency resolver, which returns it to the MVC Framework. The MVC
//Framework uses the controller instance to service the request.


//One benefit of the approach we have taken here is that any controller can declare that it requires an
//IValueCalculator in its constructor and Ninject will be used, through our custom dependency resolver,
//creates an instance of the implementation we specified in the AddBindings method.

//The best part is that we only have to modify our dependency resolver class when we want to replace
//the LinqValueCalculator with another implementation, because this is the only place where we have to
//specify the implementation that is used to satisfy requests for the IValueCalculator interface.


//Creating Chains of Dependency

//yeni discount class'ı ekledim
//artık linqvaluecalculator constructor'ı idiscount parametresi alıyor.
//aşağıda addbindings'e yeni satırı ekledim.


//Specifying Property and Constructor Parameter Values

//discount.cs'e bir alan eklendi ve aşağıda kernel bind'ı değiştirdim



//Using Conditional Binding


//Table 6-1. Ninject Conditional Binding Methods
//Method Effect
//When(predicate) Binding is used when the predicate—a lambda expression—evaluates to
//true.
//WhenClassHas<T>() Binding is used when the class being injected is annotated with the attribute
//whose type is specified by T.
//WhenInjectedInto<T>() Binding is used when the class being injected into is of type T.





namespace EssentialTools.Infrastructure 
{
    public class NinjectDependencyResolver : IDependencyResolver 
    {
        private IKernel kernel;

        public NinjectDependencyResolver() 
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType) 
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();

            
            //kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>();

            //kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize", 50M);

            //kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithConstructorArgument("discountParam", 50M);

            kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculator>();
        }
    }
}