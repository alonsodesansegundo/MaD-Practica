using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    /// <summary>
    /// A Custom VO which keeps the results for a login action.
    /// </summary>
    [Serializable()]
    public class LoginResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResult"/> class.
        /// </summary>
        /// <param name="userId">The user profile id.</param>
        /// <param name="firstName">Users's first name.</param>
        /// <param name="lastName">Users's last name.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        /// <param name="postalAddress">The postal address.</param>
        public LoginResult(long userId, String firstName, String lastName,
            String encryptedPassword, String language, String country, String postalAddress)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EncryptedPassword = encryptedPassword;
            this.Language = language;
            this.Country = country;
            this.PostalAddress = postalAddress;
        }

        #region Properties Region

        /// <summary>
        /// Gets the user profile id.
        /// </summary>
        /// <value>The user profile id.</value>
        public long UserId { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>The <c>firstName</c></value>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <value>The <c>lastName</c></value>
        public string LastName { get; private set; }


        /// <summary>
        /// Gets the postal address.
        /// </summary>
        /// <value>The <c>postalAddress</c></value>
        public string PostalAddress { get; private set; }

        /// <summary>
        /// Gets the encrypted password.
        /// </summary>
        /// <value>The <c>encryptedPassword.</c></value>
        public string EncryptedPassword { get; private set; }

        /// <summary>
        /// Gets the language code.
        /// </summary>
        /// <value>The language code.</value>
        public string Language { get; private set; }

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string Country { get; private set; }

        #endregion Properties Region
        public override bool Equals(object obj)
        {
            LoginResult target = (LoginResult)obj;

            return (this.UserId == target.UserId)
                   && (this.FirstName == target.FirstName)
                   && (this.LastName == target.LastName)
                   && (this.EncryptedPassword == target.EncryptedPassword)
                   && (this.Language == target.Language)
                   && (this.Country == target.Country);
        }

        public override int GetHashCode()
        {
            return this.UserId.GetHashCode();
        }


        public override String ToString()
        {
            String strLoginResult;

            strLoginResult =
                "[ userProfileId = " + UserId + " | " +
                "firstName = " + FirstName + " | " +
                "lastName = " + LastName + " | " +
                "encryptedPassword = " + EncryptedPassword + " | " +
                "language = " + Language + " | " +
                "country = " + Country + " ]";

            return strLoginResult;
        }

    }
}