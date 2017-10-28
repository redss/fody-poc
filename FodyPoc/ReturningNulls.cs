using System;
using FluentAssertions;
using NullGuard;
using NUnit.Framework;

namespace FodyPoc
{
    [NullGuard(ValidationFlags.All)]
    public class ReturningNulls
    {
        public string GetNull() => null;

        [return: AllowNull]
        public string AllowedGetNull() => null;
    }

    public class ReturningNullsTests
    {
        [Test]
        public void cannot_get_null_from_null_protected_method()
        {
            Action gettingNull = () => new ReturningNulls().GetNull();

            gettingNull.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void can_get_null_when_it_is_explicitly_allowed()
        {
            Action gettingNull = () => new ReturningNulls().AllowedGetNull();

            gettingNull.ShouldNotThrow();
        }
    }
}