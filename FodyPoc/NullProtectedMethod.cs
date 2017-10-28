using System;
using FluentAssertions;
using NullGuard;
using NUnit.Framework;

namespace FodyPoc
{
    [NullGuard(ValidationFlags.All)]
    public class NullProtectedMethod
    {
        public void Call(string notNullString)
        {
        }
    }

    public class NullProtectedMethodTests
    {
        [Test]
        public void null_protected_method_can_be_called_with_actual_value()
        {
            CallingNullProtectedMethod("yo!").ShouldNotThrow();
        }

        [Test]
        public void call_cannot_be_called_With_null()
        {
            CallingNullProtectedMethod(null).ShouldThrow<ArgumentNullException>();
        }

        private static Action CallingNullProtectedMethod(string argument)
        {
            return () => new NullProtectedMethod().Call(argument);
        }
    }
}
