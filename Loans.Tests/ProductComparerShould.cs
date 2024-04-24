using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    [Category("Product Comprasion")]
    public class ProductComparerShould
    {
        private List<LoanProduct> products;
        private ProductComparer sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            products = new List<LoanProduct>()
            {
                new LoanProduct(1,"a",1),
                new LoanProduct(2,"b",2),
                new LoanProduct(3,"c",3),
            };
            sut = new ProductComparer(new LoanAmount("USA", 200_000m), products);
        }

        [SetUp]
        public void SetUp() 
        {
            //products = new List<LoanProduct>()
            //{
            //    new LoanProduct(1,"a",1),
            //    new LoanProduct(2,"b",2),
            //    new LoanProduct(3,"c",3),
            //};
            // sut = new ProductComparer(new LoanAmount("USA", 200_000m), products);
        }

        [Test]
        public void ReturnCorrectNumberOfComparsions()
        {
            //Added in Setup
            //var products = new List<LoanProduct>()
            //{
            //    new LoanProduct(1,"a",1),
            //    new LoanProduct(2,"b",2),
            //    new LoanProduct(3,"c",3),
            //};

            //var sut = new ProductComparer(new LoanAmount("USA", 200_000m), products);

            List<MonthlyRepaymentComparison> comparisons =
                 sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicateComparsions()
        {
            //Added in Setup
            //var products = new List<LoanProduct>()
            //{
            //    new LoanProduct(1,"a",1),
            //    new LoanProduct(2,"b",2),
            //    new LoanProduct(3,"c",3),
            //    new LoanProduct(4,"c",4),
            //};

            //var sut = new ProductComparer(new LoanAmount("USA", 200_000m), products);

            List<MonthlyRepaymentComparison> comparisons =
                 sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparsionForFirstProduct()
        {
            //Added in Setup
            //var products = new List<LoanProduct>()
            //{
            //    new LoanProduct(1,"a",1m),
            //    new LoanProduct(2,"b",2),
            //    new LoanProduct(3,"c",3),
            //};

            //var sut = new ProductComparer(new LoanAmount("USA", 200_000m), products);

            List<MonthlyRepaymentComparison> comparisons =
                 sut.CompareMonthlyRepayments(new LoanTerm(30));

            //Need to also know the expected monthly repayment 
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28M);

            Assert.That(comparisons, Does.Contain(expectedProduct));
        }


        [Test]
        public void ReturnComparsionForFirstProduct_WithPartialKnownExpectedValues()
        {
            //Added in Setup
            //var products = new List<LoanProduct>()
            //{
            //    new LoanProduct(1,"a",1),
            //    new LoanProduct(2,"b",2),
            //    new LoanProduct(3,"c",3),
            //};

            //var sut = new ProductComparer(new LoanAmount("USA", 200_000m), products);

            List<MonthlyRepaymentComparison> comparisons =
                 sut.CompareMonthlyRepayments(new LoanTerm(30));

            //Same Code but using Matches

            //Assert.That(comparisons, Has.Exactly(1)
            //                        .Property("ProductName").EqualTo("a")
            //                        .And
            //                        .Property("InterestRate").EqualTo(1)
            //                        .And
            //                        .Property("MonthlyRepayment").GreaterThan(0));

            Assert.That(comparisons, Has.Exactly(1)
                                        .Matches<MonthlyRepaymentComparison>(item =>
                                          item.ProductName == "a" &&
                                          item.InterestRate == 1 &&
                                          item.MonthlyRepayment > 0));
        }
    }
}
