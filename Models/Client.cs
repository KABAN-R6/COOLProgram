namespace AvaloniaApplication7.Models;

public class Client
{
    public int Id { get; set; }
    public string name { get; set; }
    public string login { get; set; }
    public string Password { get; set; }

    public Client(int id, string name, string login, string password)
    {
        Id = id;
        this.name = name;
        this.login = login;
        Password = password;
    }
}