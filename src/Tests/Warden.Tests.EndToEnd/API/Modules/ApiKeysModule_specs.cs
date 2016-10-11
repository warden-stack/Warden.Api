using System;
using System.Collections.Generic;
using Machine.Specifications;
using Warden.DTO.ApiKeys;

namespace Warden.Tests.EndToEnd.API.Modules
{
    public class ApiKeysModule_specs : ModuleBase_specs
    {
        protected static void Initialize()
        {
        }

        protected static IEnumerable<ApiKeyDto> GetApiKeys()
            => RequestAuthenticatedAsync(c => c.GetCollectionAsync<ApiKeyDto>("api-keys"))
                .GetAwaiter()
                .GetResult();
    }

    [Subject("API keys collection")]
    public class when_fetching_api_keys : ApiKeysModule_specs
    {
        static IEnumerable<ApiKeyDto> ApiKeys;

        Establish context = () => Initialize();

        Because of = () => ApiKeys = GetApiKeys();

        It should_return_non_empty_collection = () =>
        {
            ApiKeys.ShouldNotBeEmpty();
            foreach (var apiKey in ApiKeys)
            {
                apiKey.Id.ShouldNotEqual(Guid.Empty);
                apiKey.UserId.ShouldNotBeEmpty();
                apiKey.Key.ShouldNotBeEmpty();
            }
        };
    }
}