using AdoNetExamples.Es1.Data;
using AdoNetExamples.Es1.Data.Models;
using System;
using System.Collections.Generic;

namespace AdoNetExamples.Es1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("** Pizzeria Client App **");

            IPizzeriaBusinessLogic bl = new PizzeriaBusinessLogic();

            var pizze = bl.GetPizzas();
            foreach(var p in pizze)
            {
                Console.WriteLine("{0,5}\t{1,-30}{2,8}", p.Id, p.Name, p.Price.ToString("C2"));
            }

            Console.Write("Scegli una pizza:\t");
            int pizzaCode = int.Parse(Console.ReadLine());
            
            IEnumerable<Ingredient> ingredients = bl.GetPizzaIngredients(pizzaCode, out string errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                Console.WriteLine(errMsg);
            } else
            {
                Console.WriteLine("{0,5}\t{1,-30}{2,8}", "Codice","Nome","Costo");
                foreach (var i in ingredients)
                    Console.WriteLine("{0,5}\t{1,-30}{2,8}", i.Id, i.Name, i.Cost);
            }

            Console.Write("Scegli una pizza di cui aumentare del 10% il costo:\t");
            pizzaCode = int.Parse(Console.ReadLine());

            bool updateResult = bl.UpdatePizzaPrice(pizzaCode, out errMsg);


            Console.ReadLine();
        }
    }
}
