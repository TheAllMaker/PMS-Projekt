﻿using System;

namespace MediTrack
{
    internal class Patient
    {
       public string prename;
       public string surname;
       public DateTime birthday;

        public Patient(string prename, string surname, DateTime birthday) {
            this.prename = prename;
            this.surname = surname;
            this.birthday = birthday;
        }
    }
}