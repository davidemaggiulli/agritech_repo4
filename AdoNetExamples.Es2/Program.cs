using System;
using System.Collections.Generic;
using AdoNetExamples.EF.Es1.Data;
using AdoNetExamples.EF.Es1.Data.Models;

namespace AdoNetExamples.Es2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("** Pizzeria Client App **");

            IPizzeriaBusinessLogic bl = new EFPizzeriaBusinessLogic();

            var pizze = bl.GetPizzas();
            PrintPizza(pizze);

            Console.Write("Scegli una pizza:\t");
            int pizzaCode = int.Parse(Console.ReadLine());

            IEnumerable<Ingredient> ingredients = bl.GetPizzaIngredients(pizzaCode, out string errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                Console.WriteLine(errMsg);
            }
            else
            {
                Console.WriteLine("{0,5}\t{1,-30}{2,8}", "Codice", "Nome", "Costo");
                foreach (var i in ingredients)
                    Console.WriteLine("{0,5}\t{1,-30}{2,8}", i.Code, i.Name, i.Cost);
            }

            Console.Write("Scegli una pizza di cui aumentare del 10% il costo:\t");
            pizzaCode = int.Parse(Console.ReadLine());

            bool updateResult = bl.UpdatePizzaPrice(pizzaCode, out errMsg);
            if (updateResult)
            {
                Console.WriteLine("Pizza aggiornata correttamente");
            }
            else
            {
                Console.WriteLine("Errore durante aggiornamento.");
                if (!string.IsNullOrEmpty(errMsg))
                    Console.WriteLine(errMsg);
            }

            Pizza pizza = bl.GetByPizzaByCode(pizzaCode);
            PrintPizza(pizza);


            Console.Write($"Indicare ingrediente:\t");
            string ingredient = Console.ReadLine();

            IList<Pizza> pizzaByIng = bl.SearchPizzaByIngredientName(ingredient);
            PrintPizza(pizzaByIng);
            Console.ReadLine();
        }

        private static void PrintPizza(IEnumerable<Pizza> pizzas)
        {
            Console.WriteLine("{0,5}\t{1,-30}{2,8}", "Codice", "Nome", "Prezzo");
            foreach (var p in pizzas)
            {
                Console.WriteLine("{0,5}\t{1,-30}{2,8}", p.Code, p.Name, p.Price.ToString("C2"));
            }
        }

        private static void PrintPizza(Pizza pizza)
        {
            if (pizza != null)
            {
                Console.WriteLine($"Pizza: {pizza.Name}\tCosto:{pizza.Price}");
            }
        }
    }
}
