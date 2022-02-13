using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace JasonPereira84.Helpers.Web.Tests
{
    namespace Extensions
    {
        using JasonPereira84.Helpers.Extensions;

        using Microsoft.AspNetCore.Mvc;
        using Microsoft.AspNetCore.Mvc.ViewFeatures;

        [TestClass]
        public class Test_TViewDataDictionary
        {
            [TestMethod]
            public void reallyTryGetTitle()
            {
                {
                    var @object = "Test";
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.IsTrue(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: @object,
                        actual: title);
                }

                {
                    var @object = "Test";
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "title", @object }
                    };

                    Assert.IsTrue(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: @object,
                        actual: title);
                }

                {
                    var dictionary = new Dictionary<String, Object>();

                    Assert.IsFalse(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: "NULL",
                        actual: title);
                }

                {
                    var dictionary = default(IDictionary<String, Object>);

                    Assert.IsFalse(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: "NULL",
                        actual: title);
                }

                {
                    var @object = default(Object);
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.IsFalse(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: "NULL",
                        actual: title);
                }

                {
                    var @object = 1;
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.IsFalse(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: "NULL",
                        actual: title);
                }

                {
                    var @object = new SomeClass();
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.IsFalse(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: "NULL",
                        actual: title);
                }

                {
                    var @object = new[] { 1 };
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.IsFalse(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: "NULL",
                        actual: title);
                }

                {
                    var @object = default(String);
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.IsFalse(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: "NULL",
                        actual: title);
                }

                {
                    var @object = "";
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.IsFalse(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: "EMPTY",
                        actual: title);
                }

                {
                    var @object = " ";
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.IsFalse(Web.reallyTryGetTitle(dictionary, out String title));
                    Assert.AreEqual(
                        expected: "WHITESPACE",
                        actual: title);
                }

            }

            [TestMethod]
            public void titleFrom()
            {
                {
                    var prefix = "Prefix";
                    var @object = "Test";
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.AreEqual(
                        expected: $"{prefix}{@object}",
                        actual: Web.titleFrom(dictionary, prefix, "{0}{1}", "{0}"));
                }

                {
                    var dictionary = new Dictionary<String, Object>();

                    Assert.AreEqual(
                        expected: "NULL",
                        actual: Web.titleFrom(dictionary, "Prefix", "{1}", "{1}"));
                }

                {
                    var @object = default(String);
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.AreEqual(
                        expected: "NULL",
                        actual: Web.titleFrom(dictionary, "Prefix", "{1}", "{1}"));
                }

                {
                    var @object = "";
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.AreEqual(
                        expected: "EMPTY",
                        actual: Web.titleFrom(dictionary, "Prefix", "{1}", "{1}"));
                }

                {
                    var @object = " ";
                    var dictionary = new Dictionary<String, Object>
                    {
                        { "Title", @object }
                    };

                    Assert.AreEqual(
                        expected: "WHITESPACE",
                        actual: Web.titleFrom(dictionary, "Prefix", "{1}", "{1}"));
                }

                {
                    {
                        {
                            var prefix = "Prefix";
                            var @object = "Test";
                            var dictionary = new Dictionary<String, Object>
                        {
                            { "Title", @object }
                        };

                            Assert.AreEqual(
                                expected: $"{prefix} - {@object}",
                                actual: Web.titleFrom(dictionary, prefix, default(String), "{0}"));
                        }

                        {
                            var prefix = "Prefix";
                            var @object = "Test";
                            var dictionary = new Dictionary<String, Object>
                        {
                            { "Title", @object }
                        };

                            Assert.AreEqual(
                                expected: "",
                                actual: Web.titleFrom(dictionary, prefix, "", "{0}"));
                        }

                        {
                            var prefix = "Prefix";
                            var @object = "Test";
                            var dictionary = new Dictionary<String, Object>
                        {
                            { "Title", @object }
                        };

                            Assert.AreEqual(
                                expected: " ",
                                actual: Web.titleFrom(dictionary, prefix, " ", "{0}"));
                        }

                        {
                            var prefix = "Prefix";
                            var @object = "Test";
                            var dictionary = new Dictionary<String, Object>
                        {
                            { "Title", @object }
                        };

                            Assert.AreEqual(
                                expected: "x",
                                actual: Web.titleFrom(dictionary, prefix, "x", "{0}"));
                        }

                    }

                    {
                        {
                            var prefix = "Prefix";
                            var dictionary = new Dictionary<String, Object>();

                            Assert.AreEqual(
                                expected: prefix,
                                actual: Web.titleFrom(dictionary, prefix, "{0}", default(String)));
                        }

                        {
                            var prefix = "Prefix";
                            var dictionary = new Dictionary<String, Object>();

                            Assert.AreEqual(
                                expected: "",
                                actual: Web.titleFrom(dictionary, prefix, "{0}", ""));
                        }

                        {
                            var prefix = "Prefix";
                            var dictionary = new Dictionary<String, Object>();

                            Assert.AreEqual(
                                expected: " ",
                                actual: Web.titleFrom(dictionary, prefix, "{0}", " "));
                        }

                        {
                            var prefix = "Prefix";
                            var dictionary = new Dictionary<String, Object>();

                            Assert.AreEqual(
                                expected: "x",
                                actual: Web.titleFrom(dictionary, prefix, "{0}", "x"));
                        }

                    }

                }

            }

        }
    }
}
