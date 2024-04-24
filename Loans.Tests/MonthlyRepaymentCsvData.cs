﻿using NUnit.Framework;
using System.Collections;

namespace Loans.Tests
{
    public class MonthlyRepaymentCsvData
    {
        public static IEnumerable GetTestCases(string csvFileName)
        {
            var csvLines = File.ReadAllLines(csvFileName);
            var testCases = new List<TestCaseData>();

            foreach (var line in csvLines)
            {
                string[] values = line.Replace(" ", "").Split(",");

                decimal principle = decimal.Parse(values[0]);
                decimal interestRate = decimal.Parse(values[1]);
                int termInYears = int.Parse(values[2]);
                decimal expectedRepayment = decimal.Parse(values[3]);

                testCases.Add(new TestCaseData(principle, interestRate, termInYears, expectedRepayment));
            }

            return testCases;
        }

    }
}
