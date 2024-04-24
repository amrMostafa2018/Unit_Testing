using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    public class LoanRepaymentCalculatorShould
    {
        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
        //[TestCase(200_000, 6.5, 30, 1264.14)]
        //[TestCase(200_000, 10, 30, 1755.14)]
        public void LoanRepaymentCalculateRepayment(decimal principal,
                                                    decimal interestRate,
                                                    int termInYears,
                                                    decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                 new LoanAmount("USA", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));

        }

        [Test]
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        public decimal LoanRepaymentCalculateRepayment_SimplifiedTestCase(
                                                   decimal principal,
                                                   decimal interestRate,
                                                   int termInYears)
        {
            var sut = new LoanRepaymentCalculator();
            return sut.CalculateMonthlyRepayment(
                                  new LoanAmount("USA", principal),
                                  interestRate,
                                  new LoanTerm(termInYears));

            //Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));

        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), "TestCases")]
        //[TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        //[TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        public decimal LoanRepaymentCalculateRepayment_CenteralizedWithReturn(
                                               decimal principal,
                                               decimal interestRate,
                                               int termInYears)
        {
            var sut = new LoanRepaymentCalculator();
            return sut.CalculateMonthlyRepayment(
                                  new LoanAmount("USA", principal),
                                  interestRate,
                                  new LoanTerm(termInYears));

            //Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));

        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentCsvData), "GetTestCases",new object[] { "Data.csv" })]
        //[TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        //[TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        public void LoanRepaymentCalculateRepayment_Csv(
                                                    decimal principal,
                                                    decimal interestRate,
                                                    int termInYears,
                                                    decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                 new LoanAmount("USA", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }


        //make Combination test cases
        [Test]
        public void LoanRepaymentCalculateRepayment_Combinatorial(
                                            [Values(100_000, 200_000, 500_000)]decimal principal,
                                            [Values(6.5, 10, 20)]decimal interestRate,
                                            [Values(10, 20, 30)]int termInYears)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                 new LoanAmount("USA", principal), interestRate, new LoanTerm(termInYears));

        }

        [Test]
        [Sequential]
        public void LoanRepaymentCalculateRepayment_Sequential(
                                       [Values(200_000, 200_000, 500_000)] decimal principal,
                                       [Values(6.5, 10, 10)] decimal interestRate,
                                       [Values(30, 30, 30)] int termInYears,
                                       [Values(1264.14, 1755.14, 4387.86)] decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                 new LoanAmount("USA", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        public void LoanRepaymentCalculateRepayment_Range(
                                           [Range(1,5)] decimal principal,
                                           [Values(6.5, 10, 20)] decimal interestRate,
                                           [Values(10, 20, 30)] int termInYears)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                 new LoanAmount("USA", principal), interestRate, new LoanTerm(termInYears));

        }

        [Test]
        public void LoanRepaymentCalculateRepayment_10Percent()
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(
                                 new LoanAmount("USA", 200_000), 10m, new LoanTerm(30));

            Assert.That(monthlyPayment, Is.EqualTo(1755.14));

        }

    }
}
