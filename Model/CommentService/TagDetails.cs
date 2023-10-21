using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    /// <summary>
    /// VO Class which contains the tag details
    /// </summary>
    public class TagDetails
    {
        #region Properties Region

        public string name { get; set; }
        public int uses { get; set; }
        public int fontSize { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TagDetails"/> class.
        /// </summary>
        /// <param name="name">Name of the tag.</param>
        /// <param name="uses">The uses.</param>
        public TagDetails()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagDetails"/> class.
        /// </summary>
        /// <param name="name">Name of the tag.</param>
        /// <param name="uses">The uses.</param>
        public TagDetails(string name, int uses)
        {
            this.name = name;
            this.uses = uses;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagDetails"/> class.
        /// </summary>
        /// <param name="name">Name of the tag.</param>
        /// <param name="uses">The uses.</param>
        public TagDetails(string name, int uses, int fontSize)
        {
            this.name = name;
            this.uses = uses;
            this.fontSize = fontSize;
        }

        public override bool Equals(object obj)
        {
            TagDetails target = (TagDetails)obj;

            return (this.name == target.name)
                  && (this.uses == target.uses);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the Name does not change.        
        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strTagDetails;

            strTagDetails =
                "[ tagName = " + name + " | " +
                "uses = " + uses + " ]";

            return strTagDetails;
        }
    }
}