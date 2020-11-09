using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using Xunit;

namespace Covid.Integration.Test
{
    public class CategoriesControllerTest : IClassFixture<ApiFactory<FakeStartup>>
    {
        private readonly ApiFactory<FakeStartup> factory;

        public CategoriesControllerTest(ApiFactory<FakeStartup> factory)
        {
            this.factory = factory;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(10000)]
        public async System.Threading.Tasks.Task GetTopTestAsync(int top)
        {
            using (var client = factory.CreateClient())
            {
                var response = await client.GetAsync($"/api/categories/{top}");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
