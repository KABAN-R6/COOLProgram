using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication7.Models;
using AvaloniaApplication8;
using MySqlConnector;

namespace AvaloniaApplication2;

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
            Database = "test",
            UserID = "root",
            Password = "Givig-6812"

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
                command.CommandText = "SELECT *FROM clients";
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