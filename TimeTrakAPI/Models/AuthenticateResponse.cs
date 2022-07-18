namespace TimeTrakAPI.Models;

using TimeTrakAPI.Entities;

public class AuthenticateResponse : Response
{
    public AuthenticateModel? Result { get; set; }
    //public int Id { get; set; }
    //public string? FirstName { get; set; }
    //public string? LastName { get; set; }
    //public string? Username { get; set; }
    //public string? Token { get; set; }

    //public AuthenticateResponse() { }

    //public AuthenticateResponse(User user, string token)
    //{
    //    Id = user.Id;
    //    FirstName = user.FirstName;
    //    LastName = user.LastName;
    //    Username = user.Username;
    //    Token = token;
    //}
}

public class AuthenticateModel
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Token { get; set; }

    public AuthenticateModel() { }

    public AuthenticateModel(User user, string token)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        Token = token;
    }
}