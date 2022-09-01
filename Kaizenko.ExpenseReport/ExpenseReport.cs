using System;
using System.Collections;
using System.Collections.Generic;

namespace Kaizenko.ExpenseReport
{

    public class ExpenseType
    {
        private static ExpenseType _dinner = new ExpenseType("Dinner", true, 5000);
        private static ExpenseType _breakfast = new ExpenseType("Breakfast", true, 1000);
        private static ExpenseType _car_rental = new ExpenseType("Car Rental", false, int.MaxValue);
        private static ExpenseType _lunch = new ExpenseType("Lunch", true, 2000);
        public readonly string Name;
        public readonly bool IsMeal;
        public readonly int Limit;

        public static ExpenseType DINNER { get => _dinner; }
        public static ExpenseType BREAKFAST { get => _breakfast; }
        public static ExpenseType CAR_RENTAL { get => _car_rental; }
        public static ExpenseType LUNCH { get => _lunch; }

        private ExpenseType(string name, bool isMeal, int limit)
        {
            Name = name;
            IsMeal = isMeal;
            Limit = limit;
        }
    }

    public class Expense
    {
        public ExpenseType type;
        public int amount;

        public string GetName()
        {
            return type.Name;
        }
        public bool IsMeal()
        {
            return type.IsMeal;
        }

        public bool IsOverLimit()
        {
            return amount > type.Limit;
        }
    }

    public class ExpenseCollection : IEnumerable<Expense>
    {
        private readonly List<Expense> _expenses;
        public ExpenseCollection(List<Expense> expenses)
        {
            _expenses = expenses;
        }

        public int CalculateMealTotal()
        {
            int mealExpenses = 0;
            foreach (Expense expense in _expenses)
            {
                if (expense.IsMeal())
                {
                    mealExpenses += expense.amount;
                }
            }

            return mealExpenses;
        }

        public int CalculateTotal()
        {
            int total = 0;
            foreach (Expense expense in _expenses)
            {
                total += expense.amount;
            }
            return total;
        }

        public IEnumerator<Expense> GetEnumerator()
        {
            return ((IEnumerable<Expense>)_expenses).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_expenses).GetEnumerator();
        }
    }

    public class ExpenseReport
    {
        public void PrintReport(ExpenseCollection expenses)
        {
            int mealExpenses = expenses.CalculateMealTotal();
            int total = expenses.CalculateTotal();

            PrintHeader();
            foreach (Expense expense in expenses)
            {
                PrintExpense(expense);
            }
            PrintTotals(total, mealExpenses);
        }

        private static void PrintTotals(int total, int mealExpenses)
        {
            Console.WriteLine("Meal expenses: " + mealExpenses);
            Console.WriteLine("Total expenses: " + total);
        }

        private static void PrintExpense(Expense expense)
        {
            string mealOverExpensesMarker = expense.IsOverLimit() ? "X" : " ";

            Console.WriteLine(expense.GetName() + "\t" + expense.amount + "\t" + mealOverExpensesMarker);
        }

        private void PrintHeader()
        {
            Console.WriteLine("Expenses " + GetDate());
        }

        protected virtual DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}
