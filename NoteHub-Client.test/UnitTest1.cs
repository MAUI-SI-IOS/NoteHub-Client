using NoteHub_Client.test.Testcontainers;

namespace NoteHub_Client.test
{
    public class UnitTest1 : IClassFixture<NoteHubServerFixture>
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}
