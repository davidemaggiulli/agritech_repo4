using AdoNetExamples.Es1.Data.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace AdoNetExamples.Es1.Data
{
    public class PizzeriaBusinessLogic : IPizzeriaBusinessLogic
    {
        private const string connectionString = "Server=localhost;database=pizzeria;username=root;password=123456abc!;port=3306";
        public IEnumerable<Pizza> GetPizzas()
        {
            using (DbConnection connection = new MySqlConnection(connectionString))
            {
                IList<Pizza> pizzas = new List<Pizza>();
                try
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        string sql = $"SELECT Code,Name,Price FROM pizza";
                        DbCommand cmd = new MySqlCommand(sql, connection as MySqlConnection);

                        DbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Pizza p = new Pizza
                            {
                                Id = reader.GetInt32(0),
                                Name = (string)reader[1],
                                Price = Convert.ToSingle(reader["Price"])
                            };
                            pizzas.Add(p);
                        }

                    }
                }
                catch (MySqlException ex)
                {


                }
                catch (Exception ex2)
                {


                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                        connection.Close();
                    //connection.Dispose();
                }
                return pizzas;
            }
        }

        public IEnumerable<Ingredient> GetPizzaIngredients(int pizzaCode, out string errMsg) {
            errMsg = null;
            IList<Ingredient> ingredients = new List<Ingredient>();

            

            
            using DbConnection conn = new MySqlConnection(connectionString);
            string sql = $"SELECT COUNT(*) FROM Pizza WHERE Code = {pizzaCode}";

            
            using var cmd = new MySqlCommand(sql, conn as MySqlConnection);
            try
            {

                conn.Open();
                long count = (long)cmd.ExecuteScalar();
                if(count == 0)
                {
                    errMsg = $"Pizza '{pizzaCode}' non esiste";
                } else
                {
                    sql = $@"SELECT I.* 
                        FROM Composition C 
                        JOIN Ingredient I ON C.CodeIngredient = I.Code
                        WHERE C.CodePizza = {pizzaCode};";

                    using DbCommand cmd1 = new MySqlCommand(sql, conn as MySqlConnection);
                    var reader = cmd1.ExecuteReader();

                    while (reader.Read())
                    {
                        Ingredient i = new Ingredient
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Cost = reader.GetDecimal(2)
                        };

                        ingredients.Add(i);
                    }
                }
            }
            catch(Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                if(conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return ingredients;
        }

        public bool UpdatePizzaPrice(int pizzaCode, out string errMsg)
        {
            errMsg = null;
            using var conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();
                string sql = $"UPDATE Pizza Set Price = 1.1 * Price WHERE Code = {pizzaCode}";
                using var cmd = new MySqlCommand(sql, conn);

                int result = cmd.ExecuteNonQuery();
                if(result == 1)
                {
                    return true;
                } 
                else if(result == 0)
                {
                    errMsg = $"Impossibile aggiornare la pizza '{pizzaCode}'";
                }

            }catch(Exception e)
            {
                errMsg = e.Message;
            }
            finally
            {
                conn.Close();
            }

            return false;
        }

        public Pizza GetByPizzaByCode(int pizzaCode)
        {
            using var conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                var sql = $"SELECT * FROM Pizza WHERE Code = {pizzaCode}";
                using var cmd = new MySqlCommand(sql, conn);

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Pizza
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = Convert.ToSingle(reader["Price"])
                    };
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return null;
        }

        public IList<Pizza> SearchPizzaByIngredientName(string ingredient)
        {
            using var conn = new MySqlConnection(connectionString);
            IList<Pizza> pizzas = new List<Pizza>();
            try
            {
                conn.Open();

                var sql = @"SELECT P.*
                            FROM Composition C
                            JOIN Pizza P ON p.Code = C.CodePizza
                            JOIN Ingredient I ON C.CodeIngredient = I.Code
                            WHERE I.Name LIKE @ing";


                using var cmd = new MySqlCommand(sql, conn);
                DbParameter parameter = new MySqlParameter("@ing", MySqlDbType.VarChar, 30);
                parameter.Value = $"%{ingredient}%";
                cmd.Parameters.Add(parameter);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pizzas.Add(new Pizza
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = Convert.ToSingle(reader["Price"])
                    });
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return pizzas;
        }
    }
}
