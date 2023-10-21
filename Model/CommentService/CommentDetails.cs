using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    /// <summary>
    /// VO Class which contains the details of comment
    /// </summary>
    [Serializable()]
    public class CommentDetails
    {
        public string authorLogin { get; set; }

        public DateTime insertDate { get; set; }

        public string commentText { get; set; }

        public List<string> tags { get; set; }

        public CommentDetails(string authorLogin, DateTime insertDate, string commentText)
        {
            this.authorLogin = authorLogin;
            this.insertDate = insertDate;
            this.commentText = commentText;
        }

        public CommentDetails(string authorLogin, DateTime insertDate, string commentText, List<string> tags)
        {
            this.authorLogin = authorLogin;
            this.insertDate = insertDate;
            this.commentText = commentText;
            this.tags = tags;
        }
    }
}