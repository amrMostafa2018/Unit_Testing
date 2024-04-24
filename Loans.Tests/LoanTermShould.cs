using Loans.Domain.Applications;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Loans.Tests
{
    [TestFixture]
    public class LoanTermShould
    {
        [Test]
        public void ReturnTermInMonth()
        {
            //Arrange
            var sut = new LoanTerm(1);
            //Act
            var numberOfMonths = sut.ToMonths();
            //Assert
            Assert.That(numberOfMonths, Is.EqualTo(12) , "Months should be 12 * number of years");
        }

        [Test]
        public void storeYears()
        {
            var sut = new LoanTerm(1);
            //Same 
            //Assert.That(sut.Years, new EqualConstraint(1));
            Assert.That(sut.Years, Is.EqualTo(1));
        }

        [Test]
        public void RespectValueEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);
            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);
            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void ReferenceEqualityExample()
        {
            var a = new LoanTerm(1);
            var b = a;
            var c = new LoanTerm(1);
            //SameAs ==> meaning same Reference
            Assert.That(a, Is.SameAs(b));

            var x = new List<string> { "a", "b" };
            var y = x;

            Assert.That(y, Is.SameAs(x));
        }

        [Test]
        public void Double()
        {
            double a = 1.0 / 3.0;
          
            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
        }

        [Test]
        public void NotAllowZero()
        {
            Assert.That(() => new LoanTerm(0),
                Throws.TypeOf<ArgumentOutOfRangeException>()
                      .With
                      .Matches<ArgumentOutOfRangeException>(ex => ex.ParamName == "years"));
        }

    }
}
