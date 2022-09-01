using ApprovalTests.Reporters;
using NUnit.Framework;
using System;
using System.IO;

namespace Kaizenko.ExpenseReport.Tests
{
    public class ExpenseReportTests
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void PrintReport_WhenNoExpensesProvided_ExpectAnEmptyReport()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ExpenseReport expenseReport = new ExpenseReport();
                expenseReport.PrintReport(new System.Collections.Generic.List<Expense>());
                ApprovalTests.Approvals.Verify(sw);
            }
        }
    }
}
