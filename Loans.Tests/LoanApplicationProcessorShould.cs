using Loans.Domain.Applications;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loans.Tests
{
    [TestFixture]
    public class LoanApplicationProcessorShould
    {
        [Test]
        public void DeclineLowSalary()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                                                  product,
                                                  amount,
                                                  "Sarah",
                                                  25,
                                                  "133 Pluralsight Drive, Draper, Utah",
                                                  64_999);
            //add mock
            // using mock to make fake object to call method Process in LoanApplicationProcessor Class
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockICreditScorer = new Mock<ICreditScorer>();


            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                                                   mockICreditScorer.Object);

            // var sut = new LoanApplicationProcessor(null, null);

            //sut ==> system under test
            sut.Process(application);

            Assert.That(application.GetIsAccepted(), Is.False);
        }

        [Test]
        public void AcceptSalary()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                                                  product,
                                                  amount,
                                                  "Sarah",
                                                  25,
                                                  "133 Pluralsight Drive, Draper, Utah",
                                                  65_000);
            //add mock
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            //configuring mock method rturn values and assume + is True
            // add specific parameters

            //mockIdentityVerifier.Setup(b => b.Validate("Sarah", 25, "133 Pluralsight Drive, Draper, Utah"))
            //                                 .Returns(true);

            // we using It.IsAny<Guid>()  ==> in case when generate Guid and need to test 
            //mockIdentityVerifier.Setup(b => b.Validate("Sarah",
            //                                            It.IsAny<int>(), 
            //                                            "133 Pluralsight Drive, Draper, Utah"))
            //                                 .Returns(true);

            //using mock with method has out parameter
            bool isValidOutValue = true;
            mockIdentityVerifier.Setup(b => b.Validate("Sarah",
                                                        It.IsAny<int>(),
                                                        "133 Pluralsight Drive, Draper, Utah",
                                                        out isValidOutValue));




            //var mockICreditScorer = new Mock<ICreditScorer>();
            //configuring mock method rturn specific value
            //mockICreditScorer.Setup(b => b.Score).Returns(300);


            //proprty hierarchies
            //manual hierarchies
            //var mockScoreValue = new Mock<ScoreValue>();
            //mockScoreValue.Setup(x => x.Score).Returns(300);

            //var mockScoreResult = new Mock<ScoreResult>();
            //mockScoreResult.Setup(x => x.ScoreValue).Returns(mockScoreValue.Object);

            //mockICreditScorer.Setup(x => x.ScoreResult).Returns(mockScoreResult.Object);



            



            //auto hierarchies
            //you must add default Value Mock 
            var mockICreditScorer = new Mock<ICreditScorer>() { DefaultValue = DefaultValue.Mock};
            //mock All properties To Track Changes (must put before Setup) 
            mockICreditScorer.SetupAllProperties();

            mockICreditScorer.Setup(b => b.ScoreResult.ScoreValue.Score).Returns(300);

            //mock properties To Track Changes 
            //mockICreditScorer.SetupProperty(c => c.Count);
                //can add initial value
            //mockICreditScorer.SetupProperty(c => c.Count , 10);

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                                           mockICreditScorer.Object);



            sut.Process(application);

            Assert.That(application.GetIsAccepted(), Is.True);
            Assert.That(mockICreditScorer.Object.Count , Is.EqualTo(1));
           //Assert.That(mockICreditScorer.Object.Count , Is.EqualTo(11));

           
        }


        [Test]
        public void AcceptUsingPartialMock()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                                                  product,
                                                  amount,
                                                  "Sarah",
                                                  25,
                                                  "133 Pluralsight Drive, Draper, Utah",
                                                  65_000);

            var mockIdentityVerifier = new Mock<IdentityVerifierServiceGateway>();

            mockIdentityVerifier.Setup(x => x.CallService("Sarah",
                                                          25,
                                                          "133 Pluralsight Drive, Draper, Utah"))
                                .Returns(true);

            var expectedTime = new DateTime(2000, 1, 1);

            mockIdentityVerifier.Setup(x => x.GetCurrentTime())
                                .Returns(expectedTime);

            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);


            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                                                   mockCreditScorer.Object);

            sut.Process(application);
           // Assert.That(mockIdentityVerifier.Object.LastCheckTime, Is.EqualTo(expectedTime));
            Assert.That(mockIdentityVerifier.Object.LastCheckTime,
                    Is.EqualTo(DateTime.Now).Within(1).Seconds);
        }
       

        [Test]
        public void InitializeIdentityVerifier()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                                                  product,
                                                  amount,
                                                  "Sarah",
                                                  25,
                                                  "133 Pluralsight Drive, Draper, Utah",
                                                  65_000);
            //add mock
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();

            bool isValidOutValue = true;
            mockIdentityVerifier.Setup(b => b.Validate("Sarah",
                                                        It.IsAny<int>(),
                                                        "133 Pluralsight Drive, Draper, Utah",
                                                        out isValidOutValue));

            //auto hierarchies
            //you must add default Value Mock 
            var mockICreditScorer = new Mock<ICreditScorer>() { DefaultValue = DefaultValue.Mock };
            //mock All properties To Track Changes (must put before Setup) 
            mockICreditScorer.SetupAllProperties();

            mockICreditScorer.Setup(b => b.ScoreResult.ScoreValue.Score).Returns(300);

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                                           mockICreditScorer.Object);

            sut.Process(application);

            //add Verify to Sure that Initialize method call (it called in method ==>  sut.Process(application) )
            mockIdentityVerifier.Verify(x => x.Initialize());

            
        }


        [Test]
        public void CalculateScoreVerifier()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                                                  product,
                                                  amount,
                                                  "Sarah",
                                                  25,
                                                  "133 Pluralsight Drive, Draper, Utah",
                                                  65_000);
            //add mock
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();

            bool isValidOutValue = false;
            mockIdentityVerifier.Setup(b => b.Validate("Sarah",
                                                        It.IsAny<int>(),
                                                        "133 Pluralsight Drive, Draper, Utah",
                                                        out isValidOutValue));

            //auto hierarchies
            //you must add default Value Mock 
            var mockICreditScorer = new Mock<ICreditScorer>() { DefaultValue = DefaultValue.Mock };
            //mock All properties To Track Changes (must put before Setup) 
            mockICreditScorer.SetupAllProperties();

            mockICreditScorer.Setup(b => b.ScoreResult.ScoreValue.Score).Returns(300);

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                                                   mockICreditScorer.Object);

            sut.Process(application);

            //verifying Property get call
            //mockICreditScorer.VerifyGet(c => c.ScoreResult.ScoreValue.Score);

            //verifying Property set call
            mockICreditScorer.VerifySet(c => c.Count = 1);
            mockICreditScorer.VerifySet(c => c.Count = It.IsAny<int>());

            //add Verify to Sure that CalculateScore method call
            //(it called in method ==>  sut.Process(application))
            ///and you may specific number of times
            mockICreditScorer.Verify(x => x.CalculateScore(
                "Sarah", "133 Pluralsight Drive, Draper, Utah"),Times.Once);
        }
    }
}
