using Es.Udc.DotNet.PracticaMaD.Model;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    public class UserSession
    {
        private long userId;
        private String firstName;
        private String postalAddress;
        private CreditCard defaultCreditCard;

        public long UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public CreditCard DefaultCreditCard
        {
            get { return defaultCreditCard; }
            set { defaultCreditCard = value; }
        }

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public String PostalAddress
        {
            get { return postalAddress; }
            set { postalAddress = value; }
        }
    }
}