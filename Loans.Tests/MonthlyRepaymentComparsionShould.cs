using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loans.Tests
{
    public class MonthlyRepaymentComparsionShould
    {
        [Test]
        [Category("Product Comprasion")]
        public void RespectValueEquality()
        {
            var a = new MonthlyRepaymentComparison("a",1,1);
            var b = new MonthlyRepaymentComparison("a", 1, 1); ;
            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        [Category("XYZ")]
        public void RespectValueInEquality()
        {
            var a = new MonthlyRepaymentComparison("a", 1, 1);
            var b = new MonthlyRepaymentComparison("a", 2, 1); ;
            Assert.That(a, Is.Not.EqualTo(b));
        }
    }
}
