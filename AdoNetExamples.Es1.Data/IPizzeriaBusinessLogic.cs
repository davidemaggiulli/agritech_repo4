using AdoNetExamples.Es1.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdoNetExamples.Es1.Data
{
    public interface IPizzeriaBusinessLogic
    {
        IEnumerable<Pizza> GetPizzas();
        IEnumerable<Ingredient> GetPizzaIngredients(int pizzaCode, out string err);
        bool UpdatePizzaPrice(int pizzaCode, out string errMsg);
        Pizza GetByPizzaByCode(int pizzaCode);
        IList<Pizza> SearchPizzaByIngredientName(string ingredient);
    }
}
