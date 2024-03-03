namespace MinWindowSubstringTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new string[] { "01ab23", "ab" }, "ab")]
        [InlineData(new string[] { "a**ba*c*a*", "abac" }, "ba*c*a")]
        [InlineData(new string[] { "ahffaksfajeeubsne", "jefaa" }, "aksfaje")]
        [InlineData(new string[] { "aaffhkksemckelloe", "fhea" }, "affhkkse")]
        public void TestSolutionMinWindowSubString(string[] strArr, string expected)
        {
            var result = Solution.MinWindowSubstring(strArr);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new string[] { "01ab23", "ab" }, "ab")]
        [InlineData(new string[] { "a**ba*c*a*", "abac" }, "ba*c*a")]
        [InlineData(new string[] { "ahffaksfajeeubsne", "jefaa" }, "aksfaje")]
        [InlineData(new string[] { "aaffhkksemckelloe", "fhea" }, "affhkkse")]
        public void TestMinWindowSubString(string[] strArr, string expected)
        {
            var result = MainClass.MinWindowSubstring(strArr);
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(new string[] { "**ab**", "ab" }, "a->1[2] b->1[3] ")]
        [InlineData(new string[] { "a**ba*c*a*", "abac" }, "a->2[0,4,8] b->1[3] c->1[6] ")]
        [InlineData(new string[] { "ahffaksfajeeubsne", "jefaa" }, "j->1[9] e->1[10,11,16] f->1[2,3,7] a->2[0,4,8] ")]
        [InlineData(new string[] { "aaffhkksemckelloe", "fhea" }, "f->1[2,3] h->1[4] e->1[8,12,16] a->1[0,1] ")]
        public void TestCreateIndexers(string[] strArr, string expected)
        {
            var resultIndexers = MainClass.CreateIndexers(strArr[0].ToList(), strArr[1].ToList());
            string output = string.Empty;
            foreach (var idx in resultIndexers)
            {
                var c = idx.Key;
                var count = idx.Value.Item1;
                var indexes = idx.Value.Item2;
                var strIndexes = string.Join(',', indexes);
                output += $"{c}->{count}[{strIndexes}] ";
            }

            Assert.Equal(expected, output);
        }
    }
}