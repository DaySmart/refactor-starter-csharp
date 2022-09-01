using NUnit.Framework;
using System;
using System.IO;
using Kaizenko.ExpenseReport;

namespace Kaizenko.ExpenseReport.Tests
{
    public class ExpenseReportTests
    {
        [Test]
        public void WhenNoExpensesProvided_ExpectAnEmptyReport()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                ExpenseReport expenseReport = new ExpenseReport();
                expenseReport.PrintReport(new System.Collections.Generic.List<Expense>());
                string consoleOutput = sw.ToString();
                string[] lines = consoleOutput.Split('\n');

                Assert.That(lines.Length, Is.EqualTo(4));
                Assert.That(lines[0], Contains.Substring("Expenses"));
                Assert.That(lines[1], Is.EqualTo("Meal expenses: 0"));
                Assert.That(lines[2], Is.EqualTo("Total expenses: 0"));
            }
        }
    }
}
