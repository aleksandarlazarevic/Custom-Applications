using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Helpers
{
    public class DbHelper
    {
        public static string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ToDoApp;";

        public static ToDoListItem GetListItem(int id)
        {
            ToDoListItem listItem = new ToDoListItem();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM TodoListItems WHERE Id=@id";
                SqlCommand command = new SqlCommand(commandString, sqlConnection);
                command.Parameters.AddWithValue("@id", id);
                sqlConnection.Open();
                using (SqlDataReader commandReader = command.ExecuteReader())
                {
                    while (commandReader.Read())
                    {
                        listItem.Id = (int)commandReader["Id"];
                        listItem.AddDate = (DateTime)commandReader["AddDate"];
                        listItem.Title = commandReader["Title"].ToString();
                        listItem.IsDone = (bool)commandReader["IsDone"];
                    }

                    sqlConnection.Close();
                }
            }

            return listItem;
        }

        internal static List<ToDoListItem> GetAllListItems()
        {
            List<ToDoListItem> listOfAllItems = new List<ToDoListItem>();
            ToDoListItem listItem = new ToDoListItem();
 
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM TodoListItems ORDER BY Id ASC";
                SqlCommand command = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                using (SqlDataReader commandReader = command.ExecuteReader())
                {
                    while (commandReader.Read())
                    {
                        listItem.Id = (int)commandReader["Id"];
                        listItem.AddDate = (DateTime)commandReader["AddDate"];
                        listItem.Title = commandReader["Title"].ToString();
                        listItem.IsDone = (bool)commandReader["IsDone"];
                        listOfAllItems.Add(listItem);
                    }

                    sqlConnection.Close();
                }
            }

            return listOfAllItems;
        }

        internal static void Insert(ToDoListItem editableItem)
        {
            int id = editableItem.Id;
            ToDoListItem lastItem = GetAllListItems().Last();
            if (lastItem != null)
            {
                id = lastItem.Id + 1;
            };

            DateTime date = editableItem.AddDate;
            string title = editableItem.Title;
            bool isDone = editableItem.IsDone;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string commandString = "INSERT INTO TodoListItems VALUES(@id, @date, @title, @isDone); ";
                SqlCommand command = new SqlCommand(commandString, sqlConnection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@isDone", isDone);
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        internal static void Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string commandString = "DELETE FROM TodoListItems WHERE Id=@id";
                SqlCommand command = new SqlCommand(commandString, sqlConnection);
                command.Parameters.AddWithValue("@id", id);
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        internal static void Update(ToDoListItem dbItem)
        {
            int id = dbItem.Id;
            DateTime date = dbItem.AddDate;
            string title = dbItem.Title;
            bool isDone = dbItem.IsDone;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string commandString = "UPDATE TodoListItems SET Id = ‘@id’, AddDate = ‘@date’, Title = '@title', IsDone = '@isDone' WHERE Id = @id; "; 
                SqlCommand command = new SqlCommand(commandString, sqlConnection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@isDone", isDone);
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}
