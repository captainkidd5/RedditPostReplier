using Reddit;
using Reddit.Controllers;
using System;
using System.Collections.Generic;


namespace RedditBot
{
    /// <summary>
    /// https://github.com/sirkris/Reddit.NET#running-the-tests
    /// </summary>
    public class WorkFlow
    {
        private RedditClient Reddit;
        private int NumComments = 0;
        private HashSet<string> CommentIds;
        public WorkFlow(string[] args)
        {
            string appId = args[0];
            string refreshToken = args[1];
            string accessToken = (args.Length > 2 ? args[2] : null);

            Reddit = new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);
            CommentIds = new HashSet<string>();
        }

        /// <summary>
        /// Gets post object from url
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Returns null if url is incorrect, or user is unauthorized</returns>
        public Post ParsePostFromUrl(string url)
        {
            try
            {
                string subreddit = url.Split("r/")[1];
                subreddit = subreddit.Split("/")[0];

                string postId = url.Split("comments/")[1];
                postId = postId.Split("/")[0];
                postId = "t3_" + postId;

                return Reddit.Subreddit(subreddit).Post(postId).About();
            }
            catch
            {
                return null;
            }                
        }

        public string MassReply(Post post, string[] keywords, string replyMsg)
        {
            return "Replied to " + IterateCommentsAndReply(post.Comments.GetNew(limit: 1500), keywords, replyMsg) + " Comments.";
        }

        private void ShowComment(Comment comment, int depth = 0)
        {
            if (comment == null || string.IsNullOrWhiteSpace(comment.Author))
            {
                return;
            }

            NumComments++;
            if (!CommentIds.Contains(comment.Id))
            {
                CommentIds.Add(comment.Id);
            }

            if (depth.Equals(0))
            {
                Console.WriteLine("---------------------");
            }
            else
            {
                for (int i = 1; i <= depth; i++)
                {
                    Console.Write("> ");
                }
            }

            Console.WriteLine("[" + comment.Author + "] " + comment.Body);
        }



        private int IterateCommentsAndReply(IList<Comment> comments, string[] keywords, string replyMsg, int maxReplies = 3,int depth = 0)
        {
            int numTimesReplied = 0;
            foreach (Comment comment in comments)
            {
                if (numTimesReplied > maxReplies)
                    return numTimesReplied;
                if(comment.Author.ToLower() != Program.BotUsername && DoesCommentContain(comment, keywords))
                {
                    comment.Reply(replyMsg);
                    Console.WriteLine("Replied to comment: " + comment.Body);
                    numTimesReplied++;
                }
                //ShowComment(comment, depth);
                IterateCommentsAndReply(comment.Replies,keywords, replyMsg, (depth + 1));
                IterateCommentsAndReply(GetMoreChildren(comment), keywords, replyMsg, depth);
            }
            return numTimesReplied;
        }

        private bool DoesCommentContain(Comment comment, string[] keywords)
        {
            foreach(string str in keywords)
            {
                if (comment.Body.ToLower().IndexOf(str) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        private IList<Comment> GetMoreChildren(Comment comment)
        {
            List<Comment> res = new List<Comment>();
            if (comment.More == null)
            {
                return res;
            }

            foreach (Reddit.Things.More more in comment.More)
            {
                foreach (string id in more.Children)
                {
                    if (!CommentIds.Contains(id))
                    {
                    }
                }
            }

            return res;
        }
    }
}
