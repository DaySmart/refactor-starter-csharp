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
                ExpenseReport expenseReport = new MyExpenseReport();
                expenseReport.PrintReport(new System.Collections.Generic.List<Expense>());
                ApprovalTests.Approvals.Verify(sw);
            }
        }
    }

    public class MyExpenseReport : ExpenseReport
    {
        protected override DateTime GetDate()
        {
            return new DateTime(2018, 08, 23, 08, 12, 59);
        }
    }
}
