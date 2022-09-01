using ApprovalTests.Reporters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
                expenseReport.PrintReport(new ExpenseCollection(new System.Collections.Generic.List<Expense>()));
                ApprovalTests.Approvals.Verify(sw);
            }
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void PrintReport_Legacy()
        {
            using (StringWriter sw = new StringWriter())
            {

                Console.SetOut(sw);
                ExpenseReport expenseReport = new MyExpenseReport();
                expenseReport.PrintReport(getList());
                ApprovalTests.Approvals.Verify(sw);
            }
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void PrintReport_WhenLunchUnderLimit_ExpenseNoLimitMarker()
        {
            using (StringWriter sw = new StringWriter())
            {

                Console.SetOut(sw);
                ExpenseReport expenseReport = new MyExpenseReport();
                expenseReport.PrintReport(new ExpenseCollection(new List<Expense>()
                {
                    CreateExpense(ExpenseType.LUNCH, 999)
                }));
                ApprovalTests.Approvals.Verify(sw);
            }
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void PrintReport_WhenLunchOverLimit_ExpenseLimitMarker()
        {
            using (StringWriter sw = new StringWriter())
            {

                Console.SetOut(sw);
                ExpenseReport expenseReport = new MyExpenseReport();
                expenseReport.PrintReport(new ExpenseCollection(new List<Expense>()
                {
                    CreateExpense(ExpenseType.LUNCH, 2001)
                }));
                ApprovalTests.Approvals.Verify(sw);
            }
        }

        private ExpenseCollection getList()
        {
            List<Expense> expenses = new List<Expense>();
            ExpenseType[] expenseTypes = { ExpenseType.DINNER, ExpenseType.BREAKFAST, ExpenseType.CAR_RENTAL };
            foreach (ExpenseType expenseType in expenseTypes)
                for (int amount = 500; amount <= 8000; amount += 500)
                    expenses.Add(CreateExpense(expenseType, amount));
            return new ExpenseCollection(expenses);
        }

        private Expense CreateExpense(ExpenseType type, int amount)
        {
            return new Expense()
            {
                type = type,
                amount = amount
            };
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
