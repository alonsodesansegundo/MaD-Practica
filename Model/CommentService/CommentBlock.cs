using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentBlock
    {
        public List<CommentDetails> Comments { get; private set; }

        public bool ExistMoreItems { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CommentBlock" /> class.</summary>
        /// <param name="Comments">The comments.</param>
        /// <param name="ExistMoreItems">if set to <c>true</c> [exist more items].</param>
        public CommentBlock(List<CommentDetails> Comments, bool ExistMoreItems)
        {
            this.Comments = Comments;
            this.ExistMoreItems = ExistMoreItems;
        }
    }
}