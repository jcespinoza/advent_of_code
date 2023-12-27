
using Advent23.Days.Day12;
using Xunit;

namespace Advent23.OtherTests
{
    public class Day12MoreTests
    {
        [Fact]
        public void ComparesConditionRecords()
        {
            ConditionRecord c1 = new() { 
                Sizes = [1,2,3],
                Text = "123456"
            };
            ConditionRecord c2 = new() { 
                Sizes = [1,2,3],
                Text = c1.Text.Substring(0, c1.Text.Length-1)+"6"
            };
            
            Assert.True(c1 == c2);
        }
    }
}
