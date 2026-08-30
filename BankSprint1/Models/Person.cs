using System;

namespace BankSprint1.Models
{
    /// <summary>
    /// Abstract base class for any person known to the bank.
    /// Marked abstract because a bare "Person" is never a valid banking user on its own -
    /// only a specific role (e.g. Customer) should ever be created.
    /// </summary>
    public abstract class Person
    {
        // Private backing fields - encapsulated, only reachable through the properties below.
        private string name;
        private string contactDetails;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ContactDetails
        {
            get { return contactDetails; }
            set { contactDetails = value; }
        }

        protected Person(string name, string contactDetails)
        {
            this.name = name;
            this.contactDetails = contactDetails;
        }

        /// <summary>
        /// Every concrete role must describe itself. Forces subclasses to override
        /// this instead of relying on a generic, meaningless default.
        /// </summary>
        public abstract string GetProfileSummary();
    }
}
