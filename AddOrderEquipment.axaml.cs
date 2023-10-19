using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication7.Models;
using MySqlConnector;

namespace AvaloniaApplication8;

public partial class AddOrderEquipment : Window
{
    private List<orderEquipment> orderEquipmentt { get; set; }
    private MySqlConnectionStringBuilder _connectionSb;
    public AddOrderEquipment()
    {
      
    
        InitializeComponent();
        orderEquipmentt = new List<orderEquipment>();
        _connectionSb = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            Database = "test",
            UserID = "root",
            Password = "Givig-6812"

        };
        UpdateDataGrid();
    }

    private void UpdateDataGrid()
    {
        using (var connection = new MySqlConnection(_connectionSb.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT *FROM orderEquipment";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orderEquipmentt.Add(new orderEquipment()
                        {
                            Id= reader.GetInt32("Id"),
                            Client = reader.GetInt32("Client"),
                            Worker = reader.GetInt32("Worker"),
                            TypeEquip= reader.GetInt32("TypeEquip"),
                            Typefault = reader.GetInt32("Typefault"),
                            SerialNumber = reader.GetInt32("SerialNumber"),
                            DescriptionProblem = reader.GetString("DescriptionProblem"),
                            
                        });
                    }
                    
                }
            }
            connection.Close();
        }

        GroupsDataGrid2.ItemsSource = orderEquipmentt;
    }
}