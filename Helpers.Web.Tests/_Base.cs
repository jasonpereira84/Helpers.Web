using System;
using System.Collections.Generic;

namespace JasonPereira84.Helpers.Web.Tests
{
    internal class SomeClass : IEquatable<SomeClass>
    {
        public Int32 Value { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as SomeClass);
        }

        public bool Equals(SomeClass other)
        {
            if (other == null)
                return false;

            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            {
                hash.Add(Value);
            }
            return hash.ToHashCode();
        }

        public static bool operator ==(SomeClass left, SomeClass right)
        {
            return EqualityComparer<SomeClass>.Default.Equals(left, right);
        }

        public static bool operator !=(SomeClass left, SomeClass right)
        {
            return !(left == right);
        }

    }
}