using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_ra_kod
{

    /* CLASS: Person
     * PURPOSE: Used as an object for personal information, loaded into a list of persons
     */
    class Person
    {
        public string name, address, phone, email;
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; phone = T; email = E;
        }

        /* CONSTRUCTOR: Person (public)
         * PURPOSE: Asks the user for data and inserts them into the new person's information
         * PARAMETERS: None
         */
        public Person()
        {
            Console.Write("  1. ange namn:    ");
            name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            address = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            phone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
        }

        /* METHOD: Print (public)
         * PURPOSE: Used to print the personal information of the current person
         * PARAMETERS: None
         * RETURN VALUE: None
         */
        public void Print()
        {
            Console.WriteLine(name + ", " + address + ", " + phone + ", " + email);
        }

        /* METHOD: ModifyPerson (public)
         * PURPOSE: Inserts a new value into the current person's data
         * PARAMETERS: string field - The field to have the new value inserted into
         *             string value - The new value to be inserted
         * RETURN VALUE: None
         */
        public void ModifyPerson(string field, string value)
        {
            switch (field)
            {
                case "namn":
                    name = value;
                    break;
                case "adress":
                    address = value;
                    break;
                case "telefon":
                    phone = value;
                    break;
                case "email":
                    email = value;
                    break;
                default:
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = Load();
            Console.WriteLine("klart!");
            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    Dict.Add(new Person());
                }
                else if (command == "ta bort")
                {
                    Dict = RemovePerson(Dict);
                }
                else if (command == "visa")
                {
                    for (int i = 0; i < Dict.Count(); i++)
                    {
                        Person P = Dict[i];
                        P.Print();
                    }
                }
                else if (command == "ändra")
                {
                    Dict = Modify(Dict);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }

        /* METHOD: Load (static)
         * PURPOSE: Loads data from a file and adds it into a list
         * PARAMETERS: None
         * RETURN VALUE: List<Person> Dict - List with the data loaded from the file
         */
        static List<Person> Load()
        {

            List<Person> Dict = new List<Person>();

            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    // Console.WriteLine(line);
                    string[] word = line.Split('#');
                    // Console.WriteLine("{0}, {1}, {2}, {3}", word[0], word[1], word[2], word[3]);
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }

            return Dict;
        }

        /* METHOD: RemovePerson (static)
         * PURPOSE: Removes a person from the list
         * PARAMETERS: List<Person> Dict - The list with the person to be removed from
         * RETURN VALUE: List<Person> Dict - The updated list
         */
        static List<Person> RemovePerson(List<Person> Dict)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string toBeRemoved = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == toBeRemoved) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", toBeRemoved);
            }
            else
            {
                Dict.RemoveAt(found);
            }
            return Dict;
        }

        /* METHOD: Modify
         * PURPOSE: Changes the value of a person's info
         * PARAMETERS: List<Person> Dict - The list with the person to be modified
         * RETURN VALUE: List<Person> Dict - The updated list
         */
        static List<Person> Modify(List<Person> Dict)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string toBeChanged = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == toBeChanged) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", toBeChanged);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string fieldToChange = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", fieldToChange, toBeChanged);
                string newValue = Console.ReadLine();
                Dict[found].ModifyPerson(fieldToChange, newValue);
            }
            return Dict;
        }
    }
}
