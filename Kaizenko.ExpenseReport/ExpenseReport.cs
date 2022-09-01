using System;
using System.Collections.Generic;

namespace Kaizenko.ExpenseReport
{

    public enum ExpenseType
    {
        DINNER, BREAKFAST, CAR_RENTAL
    }

    public class Expense
    {
        public ExpenseType type;
        public int amount;
    }

    public class ExpenseReport
    {
        public void PrintReport(List<Expense> expenses)
        {
            int total = 0;
            int mealExpenses = 0;

            Console.WriteLine("Expenses " + GetDate());

            foreach (Expense expense in expenses)
            {
                mealExpenses = CalculateMealExpenses(mealExpenses, expense);
                string expenseName = GetExpenseName(expense);
                string mealOverExpensesMarker = GetMealOverExpenseMarker(expense);

                Console.WriteLine(expenseName + "\t" + expense.amount + "\t" + mealOverExpensesMarker);

                total += expense.amount;
            }

            Console.WriteLine("Meal expenses: " + mealExpenses);
            Console.WriteLine("Total expenses: " + total);
        }

        private static string GetMealOverExpenseMarker(Expense expense)
        {
            return expense.type == ExpenseType.DINNER && expense.amount > 5000 || expense.type == ExpenseType.BREAKFAST && expense.amount > 1000
                                    ? "X"
                                    : " ";
        }

        private static string GetExpenseName(Expense expense)
        {
            string expenseName = "";
            switch (expense.type)
            {
                case ExpenseType.DINNER:
                    expenseName = "Dinner";
                    break;
                case ExpenseType.BREAKFAST:
                    expenseName = "Breakfast";
                    break;
                case ExpenseType.CAR_RENTAL:
                    expenseName = "Car Rental";
                    break;
            }

            return expenseName;
        }

        private static int CalculateMealExpenses(int mealExpenses, Expense expense)
        {
            if (expense.type == ExpenseType.DINNER || expense.type == ExpenseType.BREAKFAST)
            {
                mealExpenses += expense.amount;
            }

            return mealExpenses;
        }

        protected virtual DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}
