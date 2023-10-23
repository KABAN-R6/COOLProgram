using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication7.Models;
using MySqlConnector;

namespace AppOrders;

public partial class MainWindow : Window
{
    private List<Client> Clients { get; set; }
    private MySqlConnectionStringBuilder _connectionSb;
    public MainWindow()
    {
        
        InitializeComponent();
        Clients = new List<Client>();
        _connectionSb = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            Database = "pro2",
            UserID = "root",
            Password = "123456"

        };
        UpdateData();
    }
    
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {;
        
        var loginuser = LoginBOX.Text;
        var passuser = PasswordBOX.Text;

        foreach (var client in Clients)
        {
            if (loginuser.Equals(client.login) && passuser.Equals(client.Password))
            {
                new AddOrderEquipment().ShowDialog(this);
           
                break;
            }
        }
        

        
        
    }

    public void UpdateData()
    {
        using (var connection = new MySqlConnection(_connectionSb.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT *FROM Clients";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clients.Add(new Client(reader.GetInt16("Id"), reader.GetString("name"), reader.GetString("login"),
                        reader.GetString("Password")));
                }
            }

            connection.Close();
        }
    }
}
