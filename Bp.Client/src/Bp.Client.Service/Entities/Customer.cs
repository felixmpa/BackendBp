using System;
using Bp.Common;

namespace Bp.Client.Service.Entities
{
    public class Customer : IEntity
    {
        public int Id { get; set; }

        public int PersonId { get; set; } // Foreign Key
        public Person Person { get; set; }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Password cannot be null or whitespace.", nameof(value));
                // TODO: Hash and salt the password before assigning it.
                password = value;
            }
        }
        public CustomerStatus Status { get; set; }
    }

    public enum CustomerStatus
    {
        Inactive = 0,
        Active = 1
    }
}