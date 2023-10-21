using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    /// <summary>
    /// VO Class which contains the user details
    /// </summary>
    [Serializable()]
    public class UserDetails
    {
        #region Properties Region
        public string Login { get; private set; }

        public string Password { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PostalAddress { get; private set; }

        public string Country { get; private set; }

        public string Language { get; private set; }

        public bool Admin { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDetails"/>
        /// class.
        /// </summary> 
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="email">The user's email.</param>
        /// <param name="postalAddress">The user's postal address.</param>
        /// <param name="country">The country.</param>
        /// <param name="language">The language.</param>
        /// <param name="admin">Is admin.</param>
        public UserDetails(String firstName, String lastName, String email, String postalAddress,
            String country, String language, Boolean admin)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PostalAddress = postalAddress;
            this.Country = country;
            this.Language = language;
            this.Admin = admin;
        }

        public UserDetails(String firstName, String lastName, String email, String postalAddress,
            String country, String language)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PostalAddress = postalAddress;
            this.Country = country;
            this.Language = language;
        }

        public override bool Equals(object obj)
        {
            UserDetails target = (UserDetails)obj;

            return (this.FirstName == target.FirstName)
                  && (this.LastName == target.LastName)
                  && (this.Email == target.Email)
                  && (this.PostalAddress == target.PostalAddress)
                  && (this.Country == target.Country)
                  && (this.Language == target.Language)
                  && (this.Admin == target.Admin);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return this.Login.GetHashCode();
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
            String strUserProfileDetails;

            strUserProfileDetails =
                "[firstName = " + FirstName + " | " +
                "lastName = " + LastName + " | " +
                "postalAddress = " + PostalAddress + " | " +
                "email = " + Email + " | " +
                "country = " + Country + " | " +
                "language = " + Language + " | " +
                "admin = " + Admin + " ]";

            return strUserProfileDetails;
        }

    }
}