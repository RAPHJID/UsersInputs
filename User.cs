// User.cs
using System;
using System.Collections.Generic;

class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public List<Post> Posts { get; set; } 
}

class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
