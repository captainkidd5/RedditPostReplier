using System;
using System.Threading;
using Reddit.Controllers;

namespace RedditBot
{
    //Get your auth key here. https://not-an-aardvark.github.io/reddit-oauth-helper/
    public class Program
    {
        public static string BotUsername = "MyRedditUsername";
        static string[] accountInfo = new string[] { "MyAppId","MyRefreshToken","MyAccessToken" };
        static string[] keywords = new string[] { "words", "to", "search", "for", "etc.." };
        static void Main(string[] args)
        {
            WorkFlow workFlow = new WorkFlow(accountInfo);
            Console.WriteLine("\n \n Press Escape at any time to exit. \n");

            while (true)
            {
                Thread.Sleep(500);
                Console.WriteLine("Enter the Url of the post");
                string urlPost = string.Empty;
                

                while (string.IsNullOrEmpty(urlPost))
                {
                    urlPost = Console.ReadLine();
                    if (string.IsNullOrEmpty(urlPost))
                    {
                        Console.WriteLine("url may not be empty");
                        Console.WriteLine("Enter the Url of the post");

                    }
                }

                    Console.WriteLine("Enter your comment");

                string comment = string.Empty;
                while (string.IsNullOrEmpty(comment))
                {
                    comment = Console.ReadLine();

                    if (string.IsNullOrEmpty(comment))
                    {
                        Console.WriteLine("Comment may not be empty");
                        Console.WriteLine("Enter your comment");

                    }
                }
                Post post = workFlow.ParsePostFromUrl(urlPost);
                if(post == null)
                {
                    Console.WriteLine("Url is incorrect, or your bot is unauthorized to post here. \n" +
                        " Check your reddit messages if you believe the link is correct, otherwise please try again \n" +
                        "with a new link.");
                    Console.WriteLine("------------------------");
                    Thread.Sleep(500);

                    continue;
                }
                string response = workFlow.MassReply(post, keywords, comment);
                
                Console.WriteLine(response);
                Thread.Sleep(500);
                Console.WriteLine("------------------------");

            }

        }
    }
}
