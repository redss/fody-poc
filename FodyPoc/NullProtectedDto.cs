using System;
using FluentAssertions;
using Newtonsoft.Json;
using NullGuard;
using NUnit.Framework;

namespace FodyPoc
{
    [NullGuard(ValidationFlags.All)]
    public class NullProtectedDto
    {
        public string NotNull { get; set; }

        [AllowNull]
        public string NullAllowed { get; set; }

        public NullProtectedDto(string notNull)
        {
            NotNull = notNull;
        }
    }

    public class NullProtectedDtoTests
    {
        [Test]
        public void can_deserialize_json_to_a_null_protected_dto()
        {
            var dto = JsonConvert.DeserializeObject<NullProtectedDto>(
                @"{
                    ""NotNull"": ""lol"",
                    ""NullAllowed"": null
                }"
            );

            dto.NullAllowed.Should().BeNull();
        }

        [Test]
        public void deserializing_json_with_null_will_fail()
        {
            Action deserializing = () => JsonConvert.DeserializeObject<NullProtectedDto>(
                @"{
                    ""NotNull"": null,
                    ""NullAllowed"": null
                }"
            );

            deserializing.ShouldThrow<Exception>();
        }
    }
}