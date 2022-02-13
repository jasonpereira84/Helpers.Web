using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace JasonPereira84.Helpers.Web.Tests
{
    [TestClass]
    public class Test_ServiceContext
    {
        internal class ServiceContext<TData> : _ServiceContext<TData> 
        {
            public ServiceContext(String type, String serializer, TData data)
                : base(type, serializer, data)
            { }
        }

        [TestMethod]
        public void ctor()
        {
            {
                {
                    var type = "1";
                    var serializer = "1";
                    var data = 1;

                    var serviceContext = new ServiceContext<Int32>(type, serializer, data);
                    Assert.IsNotNull(serviceContext);
                    Assert.AreEqual(
                        expected: type,
                        actual: serviceContext.Type);
                    Assert.AreEqual(
                        expected: serializer,
                        actual: serviceContext.Serializer);
                    Assert.AreEqual(
                        expected: data,
                        actual: serviceContext.Data);
                }

                {
                    Assert.ThrowsException<ArgumentException>(
                        () => new ServiceContext<Int32>(default(String), "1", 1));
                }

                {
                    Assert.ThrowsException<ArgumentException>(
                        () => new ServiceContext<Int32>("1", default(String), 1));
                }

            }

            {
                {
                    var type = "1";
                    var serializer = "1";
                    var data = new SomeClass { Value = 1 };

                    var serviceContext = new ServiceContext<SomeClass>(type, serializer, data);
                    Assert.IsNotNull(serviceContext);
                    Assert.AreEqual(
                        expected: type,
                        actual: serviceContext.Type);
                    Assert.AreEqual(
                        expected: serializer,
                        actual: serviceContext.Serializer);
                    Assert.AreSame(
                        expected: data,
                        actual: serviceContext.Data);
                }

                {
                    Assert.ThrowsException<ArgumentException>(
                        () => new ServiceContext<SomeClass>(default(String), "1", new SomeClass { Value = 1 }));
                }

                {
                    Assert.ThrowsException<ArgumentException>(
                        () => new ServiceContext<SomeClass>("1", default(String), new SomeClass { Value = 1 }));
                }

            }

            {
                {
                    var type = "1";
                    var serializer = "1";
                    var data = new[] { 1 };

                    var serviceContext = new ServiceContext<IEnumerable<Int32>>(type, serializer, data);
                    Assert.IsNotNull(serviceContext);
                    Assert.AreEqual(
                        expected: type,
                        actual: serviceContext.Type);
                    Assert.AreEqual(
                        expected: serializer,
                        actual: serviceContext.Serializer);
                    Assert.AreSame(
                        expected: data,
                        actual: serviceContext.Data);
                }

                {
                    Assert.ThrowsException<ArgumentException>(
                        () => new ServiceContext<IEnumerable<Int32>>(default(String), "1", new[] { 1 }));
                }

                {
                    Assert.ThrowsException<ArgumentException>(
                        () => new ServiceContext<IEnumerable<Int32>>("1", default(String), new[] { 1 }));
                }

            }

            {
                {
                    var type = "1";
                    var serializer = "1";
                    var data = DayOfWeek.Monday;

                    var serviceContext = new ServiceContext<DayOfWeek>(type, serializer, data);
                    Assert.IsNotNull(serviceContext);
                    Assert.AreEqual(
                        expected: type,
                        actual: serviceContext.Type);
                    Assert.AreEqual(
                        expected: serializer,
                        actual: serviceContext.Serializer);
                    Assert.AreEqual(
                        expected: data,
                        actual: serviceContext.Data);
                }

                {
                    Assert.ThrowsException<ArgumentException>(
                        () => new ServiceContext<DayOfWeek>(default(String), "1", DayOfWeek.Monday));
                }

                {
                    Assert.ThrowsException<ArgumentException>(
                        () => new ServiceContext<DayOfWeek>("1", default(String), DayOfWeek.Monday));
                }

            }

        }

    }

}
