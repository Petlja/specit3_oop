using System;
using System.Collections.Generic;

class Server
{
    private Dictionary<string, string> password;
    private HashSet<string> loggedIn;
    private Dictionary<string, Tuple<bool, string>> content;
    public Server()
    {
        password = new Dictionary<string, string>();
        loggedIn = new HashSet<string>();
        content = new Dictionary<string, Tuple<bool, string>>();
    }
    public bool AddUser(string username, string pwd)
    {
        if (password.ContainsKey(username))
            return false;

        password[username] = pwd;
        return true;
    }
    public bool Login(string username, string pwd)
    {
        string realPwd;
        if (password.TryGetValue(username, out realPwd))
        {
            if (realPwd == pwd)
            {
                loggedIn.Add(username);
                return true;
            }
        }
        return false;
    }
    public void Logout(string username)
    {
        loggedIn.Remove(username);
    }
    public void Post(string path, string news, bool isPublic)
    {
        content[path] = new Tuple<bool, string>(isPublic, news);
    }
    public bool Get(string path, out string news)
    {
        Tuple<bool, string> ans;
        if (content.TryGetValue(path, out ans) && ans.Item1)
        {
            news = ans.Item2;
            return true;
        }
        news = "";
        return false;
    }
    public bool Get(string username, string path, out string news)
    {
        Tuple<bool, string> ans;
        if (content.TryGetValue(path, out ans) &&
            (loggedIn.Contains(username) || ans.Item1))
        {
            news = ans.Item2;
            return true;
        }
        news = "";
        return false;
    }
}
class Program
{
    static void TryRead(Server srv, string path, string user = null)
    {
        string tekst;
        bool uspeh;
        if (user != null)
            uspeh = srv.Get(user, path, out tekst);
        else
            uspeh = srv.Get(path, out tekst);

        if (uspeh)
            Console.WriteLine(tekst);
        else
            Console.WriteLine("Vest '{0}' nije dostupna.", path);
    }
    static void Main(string[] args)
    {
        Server srv = new Server();
        srv.Post("C1", "Tekst prve vesti", true);
        srv.Post("C2", "Tekst druge vesti", false);

        srv.AddUser("Pera", "pwd1");
        srv.Login("Pera", "pwd1");

        TryRead(srv, "C1");
        TryRead(srv, "C2");
        TryRead(srv, "C1", "Pera");
        TryRead(srv, "C2", "Pera");
        srv.Logout("Pera");
        TryRead(srv, "C1", "Pera");
        TryRead(srv, "C2", "Pera");
    }
}
