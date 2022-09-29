namespace Sceny.TestHelpers.Tests
{
    public class FluentAssertionsTests
    {
        [Fact]
        public void FluentAssertions_was_referenced_by_Sceny_TestHelpers_project()
        {
            "string".Should().Be("string"); // only check if the packages xUnit, and FluentAssertions are available through referencing Sceny.TestHelpers.
        }
    }
}
