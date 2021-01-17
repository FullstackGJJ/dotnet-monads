using NUnit.Framework;
using System;

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

            [TestFixture]
            public class MatchVoidReturn : Method
            {
                [Test]
                public void Should_ManipulateClosure_When_ResultIsSuccessful()
                {
                    var referenceInt = 10;
                    Result<int, string>.Succ(5)
                        .Match(
                            (successResult) => 
                            {
                                referenceInt += successResult;
                            },
                            (errorResult) => {}
                        );

                    var expectedInt = 15;
                    Assert.That(referenceInt, Is.EqualTo(expectedInt));
                }

                [Test]
                public void Should_ManipulateClosure_When_ResultErrors()
                {
                    var referenceString = "Hello Bob";
                    Result<int, string>.Err("Hi Dave")
                        .Match(
                            (successResult) => { },
                            (errorResult) => 
                            {
                                referenceString = $"{referenceString}, {errorResult}";
                            }
                        );

                    var expectedString = "Hello Bob, Hi Dave";
                    Assert.That(referenceString, Is.EqualTo(expectedString));
                }
            }

            [TestFixture]
            public class Status : Method
            {
                [Test]
                public void Should_ReturnProperStatusToAllowSwitches_When_Accessed()
                {
                    var successResult = Result<int, string>.Succ(5);
                    var errorResult = Result<int, string>.Err("Hello");

                    switch (successResult.Status)
                    {
                        case ResultStatus.Success:
                            Assert.Pass();
                            break;
                        case ResultStatus.Error:
                            Assert.Fail("Shouldn't have reached here on successResult status switch");
                            break;
                    }

                    switch (errorResult.Status)
                    {
                        case ResultStatus.Success:
                            Assert.Fail("Shouldn't have reached here on errorResult status switch");
                            break;
                        case ResultStatus.Error:
                            Assert.Pass();
                            break;
                    }
                }
            }

            [TestFixture]
            public class UnsafeResult : Method
            {
                [Test]
                public void Should_ReturnCorrectResultingObject_When_AccessedCarefully()
                {
                    var successResult = Result<int, string>.Succ(5);
                    var errorResult = Result<int, string>.Err("Error");


                    if (successResult.UnsafeResult is int s)
                    {
                        Assert.That(s, Is.EqualTo(5));
                    }
                    else
                    {
                        Assert.Fail("Shouldn't have reached here with successResult.UnsafeResult");
                    }
                    
                    if (errorResult.UnsafeResult is string message)
                    {
                        Assert.That(message, Is.EqualTo("Error"));
                    }
                    else
                    {
                        Assert.Fail("Shouldn't have reached here with errorResult.UnsafeResult");
                    }
                }
            }
        }

        [TestFixture]
        public class ScenarioTests : ResultClassTests
        {

        }
    }
}