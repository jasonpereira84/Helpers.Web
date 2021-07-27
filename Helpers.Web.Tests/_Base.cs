using System;

namespace JasonPereira84.Helpers.UnitTests
{
    internal class Struct
    {
        public Int32 Value { get; set; }

        public static Struct From(Int32 value)
            => new Struct { Value = value };
    }

    internal class Class
    {
        public Int32 Value { get; set; }

        public static Class From(Int32 value)
            => new Class { Value = value };

    }

    internal class SomeException : Exception
    {
        public SomeException() : base() { }
        public SomeException(string message) : base(message) { }
        public SomeException(string message, Exception innerException) : base(message, innerException) { }
    }

    internal class ExceptionWhileTesting : Exception
    {
        public ExceptionWhileTesting(string message) : base(message) { }
    }
}
