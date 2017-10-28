using System;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace FodyPoc
{
    public class RequiredProperitesDto
    {
        public string NotNull { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public string CanBeNull { get; set; }
    }

    public class RequiredProperitesDtoTests
    {
        [Test]
        public void can_deserialize_from_json_if_required_property_is_not_null()
        {
            var json = @"{
                ""NotNull"": ""asd"",
                ""CanBeNull"": null
            }";

            DeserializingDto(json).ShouldNotThrow();
        }

        [Test]
        public void cannot_deserialize_from_json_if_required_property_is_null()
        {
            var json = @"{
                ""NotNull"": null,
                ""CanBeNull"": null
            }";

            DeserializingDto(json).ShouldThrow<Exception>();
        }

        private Action DeserializingDto(string json)
        {
            return () => DeserializeDto(json);
        }

        private RequiredProperitesDto DeserializeDto(string json)
        {
            return JsonConvert.DeserializeObject<RequiredProperitesDto>(json, new JsonSerializerSettings
            {
                ContractResolver = new RequireObjectPropertiesContractResolver()
            });
        }
    }

    public class RequireObjectPropertiesContractResolver : DefaultContractResolver
    {
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);
            contract.ItemRequired = Required.Always;
            return contract;
        }
    }
}