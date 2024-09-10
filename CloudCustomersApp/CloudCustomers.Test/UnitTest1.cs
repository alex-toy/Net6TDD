namespace CloudCustomers.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Theory]
        [InlineData("foo", 1)]
        [InlineData("bar", 2)]
        public void Test2(string param, int param2)
        {

        }
    }
}