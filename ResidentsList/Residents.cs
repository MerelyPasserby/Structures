namespace ResidentsList
{
    public class Residents
    {
        public Resident[] Locals { get; private set; } = new Resident[0];
        public void FormArray()
        {
            int size;
            Random rand = new Random();
            Console.WriteLine("\nHow big array do you want?~");

            while (!(int.TryParse(Console.ReadLine(), out size)) || size <= 0)
            {
                Console.WriteLine("Come again plz.");
            }

            Resident[] temp = new Resident[size];
            for (int i = 0; i < size; i++)
            {
                temp[i] = new Resident(i % 2 == 0 ? 'M' : 'W', new NameSurname("N" + i, "S" + i),
                   new Place("Street" + rand.Next(1, 6), "City" + i));
            }

            Locals = temp;
            Console.WriteLine($"\nArray of {size} elements created!~");
            return;
        }

        public void AddElements()
        {
            if (Locals.Length == 0)
            {
                FormArray();
                return;
            }

            int size;
            Random rand = new Random();
            Console.WriteLine("\nHow much elements do you want to add?~");

            while (!(int.TryParse(Console.ReadLine(), out size)) || size <= 0)
            {
                Console.WriteLine("Come again plz.");
            }

            Resident[] temp = new Resident[Locals.Length + size];

            for (int i = 0; i < Locals.Length; i++)
            {
                temp[i] = Locals[i];
            }

            for (int i = Locals.Length; i < Locals.Length + size; i++)
            {
                temp[i] = new Resident(i % 2 == 0 ? 'M' : 'W', new NameSurname("N" + i, "S" + i),
                    new Place("Street" + rand.Next(1, 6), "City" + i));
            }

            Locals = temp;
            Console.WriteLine($"\nYou added {size} elements to array!~");
            return;
        }

        public void Clear()
        {
            Locals = new Resident[0];
            Console.WriteLine("\nArray cleared!~");
        }

        public void Show()
        {
            if (Locals.Length == 0)
            {
                Console.WriteLine("Sorry, array is empty.");
                return;
            }

            Console.WriteLine("\nHere you are, my Dear~\n");
            foreach (var local in Locals)
                Console.WriteLine(local);

            Console.WriteLine("\nArray shown!~");
        }

        public void SortbyStreet()
        {
            if (Locals.Length == 0)
            {
                Console.WriteLine("Sorry, array is empty.");
                return;
            }

            int i;
            Place key;
            Resident tmp = null;
            for (int j = 1; j < Locals.Length; j++)
            {
                key = Locals[j].Address;
                tmp = Locals[j];
                i = j - 1;
                while (i >= 0 && Locals[i].Address > key)
                {
                    Locals[i + 1] = Locals[i];
                    i--;
                }

                Locals[i + 1] = tmp;
            }

            Console.WriteLine("\nArray sorted!~");
            //10^4 2-3 seconds
            //10^5 ~5 minutes
        }

        public void FindElementsByStreet()
        {
            if (Locals.Length == 0)
            {
                Console.WriteLine("Sorry, array is empty.");
                return;
            }

            Console.Write("\nEnter street you want to look into:");
            string str = Console.ReadLine();
            bool isFound = false;

            foreach (var local in Locals)
            {
                if (local.Address.Street == str)
                {
                    Console.WriteLine(local);
                    if (!isFound)
                        isFound = true;
                }
            }
            if (!isFound) Console.WriteLine("\nSorry, notning found. Maybe your input is wrong.");
            else
                Console.WriteLine("\nHere you are, my Dear~");
        }
       
    }
}