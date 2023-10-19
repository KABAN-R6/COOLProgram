using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication7.Models;
using MySqlConnector;

namespace AvaloniaApplication8;

public partial class AddTypeEquipment : Window
{
    private List<TypeEquipments> TypeEquipmentss { get; set; }
    private MySqlConnectionStringBuilder _connectionSb;
    public AddTypeEquipment()
    {
       
    
        InitializeComponent();
        TypeEquipmentss = new List<TypeEquipments>();
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
                command.CommandText = "SELECT *FROM TypeEquipments ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TypeEquipmentss.Add(new TypeEquipments()
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                        });
                    }
                    
                }
            }
            connection.Close();
        }

        GroupsDataGrid1.ItemsSource = TypeEquipmentss;
    }
}
