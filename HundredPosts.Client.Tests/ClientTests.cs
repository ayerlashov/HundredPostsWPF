using HundredPosts.Client.Interface;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace HundredPosts.Client.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class ClientTests : IDisposable
    {
        private IHundredPostsClient Client;

        [SetUp]
        public void Setup() => Client = new HundredPostsClient(new ClientConfig());

        [Test]
        public async Task PostCountTest()
        {
            var res = await Client.GetPosts();

            Assert.AreEqual(100, res.Count);
            Assert.Pass();
        }

        [TearDown]
        public void TearDown() => Client.Dispose();

        public void Dispose() => Client?.Dispose();
    }
}