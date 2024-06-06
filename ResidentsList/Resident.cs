using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ResidentsList.Residents;

namespace ResidentsList
{
    public class Resident
    {
        public NameSurname Person { get; private set; }
        public Place Address { get; private set; }

        char _gender;
        public char Gender
        {
            get { return _gender; }
            private set
            {
                _gender = value == 'M' ? 'M' : 'W';
            }
        }

        public Resident(char gender = 'M', NameSurname nameSurname = new NameSurname(), Place address = new Place())
        {
            Person = nameSurname;
            Address = address;
            Gender = gender;
        }

        public override string ToString()
        {
            return $" Person - {Person.ToString()}\n Gender - {Gender}\n Address - {Address.ToString()}\n";
        }
    }
}
