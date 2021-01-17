using NUnit.Framework;

namespace Core.UnitTests
{
    public class ResultClassTests
    {
        public class Method : ResultClassTests
        {
            [TestFixture]
            public class MatchWithReturn : Method
            {
                [Test]
                public void Should_GiveBackExpectedReturnInt_When_ResultIsSuccessful()
                {
                    var successResult = Result<int, string>.Succ(5)
                        .Match<int>(
                            (successResult) => successResult * 10,
                            (errorResult) => 0
                        );

                    Assert.That(successResult, Is.EqualTo(50));
                }

                [Test]
                public void Should_GiveBackExpectedString_When_ResultErrors()
                {
                    var errorResult = Result<int, string>.Err("This correctly indicates failure")
                        .Match<string>(
                            (successResult) => "This shouldn't have succeeded",
                            (errorResult) => errorResult
                        );

                    Assert.That(errorResult, Is.EqualTo("This correctly indicates failure"));
                }
            }
        }

        [TestFixture]
        public class ScenarioTests : ResultClassTests
        {

        }

        [TestFixture]
        public class MatchWithReturnMethodTests : ResultClassTests
        {

        }

        [TestFixture]
        public class MatchWithoutReturnMethodTests : ResultClassTests
        {

        }
    }
}