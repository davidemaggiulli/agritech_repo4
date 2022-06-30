using AdoNetExamples.EF.Es1.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdoNetExamples.EF.Es1.Data
{
    public class EFPizzeriaBusinessLogic : IPizzeriaBusinessLogic
    {
        public Pizza GetByPizzaByCode(int pizzaCode)
        {
            using var ctx = new PizzeriaDbContext();
            var pizza = ctx.Pizzas.Find(pizzaCode);
            return pizza;
        }

        public IEnumerable<Ingredient> GetPizzaIngredients(int pizzaCode, out string err)
        {
            err = null;
            try
            {
                using var ctx = new PizzeriaDbContext();
                var query = ctx.Compositions
                    .Where(c => c.CodePizza == pizzaCode)
                    .Select(x => x.Ingredient);
                return query.ToList();
            }catch(Exception ex)
            {
                err = ex.Message;
            }
            return new List<Ingredient>();
        }

        public IEnumerable<Pizza> GetPizzas()
        {
            using var ctx = new PizzeriaDbContext();
            var query = ctx.Pizzas;
            return query.ToList();
        }

        public IList<Pizza> SearchPizzaByIngredientName(string ingredient)
        {
            using var ctx = new PizzeriaDbContext();
            //var query = ctx.Pizzas.Where(p => p.Name.Contains(ingredient));
            var query = ctx.Compositions
                .Where(c => c.Ingredient.Name.Contains(ingredient))
                .Select(c => c.Pizza);
                
            return query.ToList();
        }

        public bool UpdatePizzaPrice(int pizzaCode, out string errMsg)
        {
            errMsg = null;
            try
            {
                using var ctx = new PizzeriaDbContext();
                var pizza = ctx.Pizzas.Find(pizzaCode);
                if(pizza == null)
                {
                    errMsg = "Pizza non trovata";
                } else
                {
                    pizza.Price *= 1.1m;
                    ctx.SaveChanges();
                    return true;
                }
            }catch(Exception ex)
            {
                errMsg =ex.Message;
            }
            return false;
        }
    }
}
