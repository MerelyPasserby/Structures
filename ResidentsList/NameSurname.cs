using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentsList
{
    public struct NameSurname
    {
        string _name;
        string _surname;

        public string Name { get { return _name; } }
        public string Surname { get { return _surname; } }

        public NameSurname(string name = "Rob", string surname = "White")
        {
            _name = name;
            _surname = surname;
        }

        public override string ToString()
        {
            return $" Name - {Name}, Surname - {Surname}";
        }

    }
}
