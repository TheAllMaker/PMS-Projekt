using System;

namespace Database
{
    public class Patient
    {
        public int id;
        public string firstName;
        public string lastName;
        public DateTime birthday;

        public Patient(string firstName, string lastName, DateTime birthday)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthday = birthday;
        }
    }
}