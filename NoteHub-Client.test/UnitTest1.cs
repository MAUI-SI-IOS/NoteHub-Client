using NoteHub_Client.test.Testcontainers;

namespace NoteHub_Client.test
{
    public class UnitTest1 : IClassFixture<NoteHubServerFixture>
    {
        private readonly NoteHubServerFixture _apiFixture;

        public UnitTest1(NoteHubServerFixture apiFixture/*Injected by XUnit*/) 
        {
            _apiFixture = apiFixture; 
        }



        [Fact]
        public void Test1()
        {
            Assert.True(_apiFixture.ApiBaseUrl is string);

        }
    }
}
