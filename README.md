# RedditPostReplier
A reddit bot for mass replying a given message to a post.
This both searches for comments containing any of your keywords, and 
replies your message to them. Max 3 replies per request.


***Usage*** - 

In Program.cs fill out your bot information:

Reddit Username - 

public static string BotUsername = "MyRedditUsername";


-----------------------
**Bot Account Info** - 

static string[] accountInfo = new string[] { "MyAppId","MyRefreshToken","MyAccessToken" };


-----------------------
**App Id** is the "web app" string and is located directly under your bot name

For **Refresh token** and **Access token** go to this url: https://not-an-aardvark.github.io/reddit-oauth-helper/
and follow the directions to generate your refresh and access tokens.



-----------------------
**Keywords** to search for and reply to - static string[] keywords = new string[] { "words", "to", "search", "for", "etc.." };

The bot will search the entire post (up to 1500 comments/child comments) and reply your custom message to any comments containing your keywords.
Right now this is limited to 3 to avoid bans, but may be increased in the future. 


Feel free to contribute or clean up my lazy code.
