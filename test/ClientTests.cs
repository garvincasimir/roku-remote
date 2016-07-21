using Xunit;
using System.Text.RegularExpressions;
using RokuDeviceLib;

namespace rokuclient.tests
{
    public class ClientTests
    {
        [Fact]
        public void TestUrlPattern()
        {
            var location = "Location: http://192.168.1.134:8060/";

            var isMath = Regex.IsMatch(location,RokuClient.LOCATION_PATTERN,RegexOptions.IgnoreCase);

            Assert.True(isMath);

        }
    }
}