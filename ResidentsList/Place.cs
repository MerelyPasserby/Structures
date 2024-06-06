using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentsList
{
    public struct Place
    {
        string _street;
        string _city;

        public string Street { get { return _street; } }
        public string City { get { return _city; } }

        public Place(string street, string city)
        {
            _street = street;
            _city = city;
        }

        public override string ToString()
        {
            return $" Street - {Street} City - {City}";
        }

        public static bool operator >(Place a, Place b)
        {
            int c = string.Compare(a.Street, b.Street);
            if (c == 1)
                return true;
            else return false;
        }

        public static bool operator <(Place a, Place b)
        {
            int c = string.Compare(a.Street, b.Street);
            if (c != 1)
                return true;
            else return false;
        }
    }
}
