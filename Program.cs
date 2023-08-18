using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string usersApiUrl = "https://jsonplaceholder.typicode.com/users"; 
        string postsApiUrl = "https://jsonplaceholder.typicode.com/posts"; 
        string commentsApiUrl = "https://jsonplaceholder.typicode.com/comments"; 

        ApiService apiService = new ApiService();
        List<User> users = await apiService.GetUsersWithPostsAsync(usersApiUrl, postsApiUrl);

        if (users != null)
        {
            Console.WriteLine("Available Users:");
            foreach (User user in users)
            {
                Console.WriteLine($"User ID: {user.Id}, Username: {user.Username}");
            }

            Console.Write("Enter the User ID to view details: ");
            if (int.TryParse(Console.ReadLine(), out int selectedUserId))
            {
                User selectedUser = users.Find(user => user.Id == selectedUserId);
                if (selectedUser != null)
                {
                    Console.WriteLine($"Selected User ID: {selectedUser.Id}, Username: {selectedUser.Username}");
                    Console.WriteLine("Posts:");
                    foreach (Post post in selectedUser.Posts)
                    {
                        Console.WriteLine($"- Post ID: {post.Id}, Title: {post.Title}");
                    }

                    Console.Write("Enter the Post ID to view comments: ");
                    if (int.TryParse(Console.ReadLine(), out int selectedPostId))
                    {
                        Post selectedPost = selectedUser.Posts.Find(post => post.Id == selectedPostId);
                        if (selectedPost != null)
                        {
                            Console.WriteLine($"Selected Post ID: {selectedPost.Id}, Title: {selectedPost.Title}");
                            List<Comment> comments = await apiService.GetCommentsForPostAsync(commentsApiUrl, selectedPost.Id);
                            if (comments != null)
                            {
                                Console.WriteLine("Comments:");
                                foreach (Comment comment in comments)
                                {
                                    Console.WriteLine($"- Name: {comment.Name}, Email: {comment.Email}, Body: {comment.Body}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Post not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid Post ID.");
                    }
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid User ID.");
            }
        }
    }
}

