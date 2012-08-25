using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace useMO1
{
    class Program
    {
        public List<Person> everyone;
        public string fileName = "C:\\peoples.txt";

        static void Main(string[] args)
        {
            new Program();
        }

        private Program()
        {
            //if(!File.Exists(fileName))
            //{
            //    File.Create(fileName);
            //}
            everyone = new List<Person>();
            read();
            menu();
        }

        public Program(string fileName)
        {
            this.fileName = fileName;
            everyone = new List<Person>();
            read();
        }

        public void menu()
        {
            while(true)
            {
                Console.WriteLine("enter exit anytime to exit the program");
                Console.WriteLine("1: add person");
                Console.WriteLine("2: update person");
                Console.WriteLine("3: delete person");
                Console.WriteLine("4: see everyone");
                Console.WriteLine();
                string temp = Console.ReadLine();
                try
                {
                    int menuNumber = int.Parse(temp);
                    if(menuNumber == 1)
                            addPerson();
                    else if(menuNumber == 2)
                            updatePerson();
                    else if(menuNumber == 3)
                            deletePerson();
                    else if(menuNumber == 4)
                            readEveryone();
                    else
                    {
                        Console.WriteLine("did not enter in number within menu");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("did not enter in a number");
                    Console.WriteLine(e);
                }
            }
        }

        private bool checkRepeat(Person person)
        {
            bool isNotRepeat = true;
            if(everyone.Where(s => s.email == person.email).Count() > 0 )
            {
                Console.WriteLine("THERE CAN ONLY BE ONE!");
                isNotRepeat = false;
            }
            return isNotRepeat;
        }

        private void addPerson()
        {
            Person person = new Person();
            Console.WriteLine("enter first name");
            person.fName = readLine();
            Console.WriteLine("enter last name");
            person.lName = readLine();
            Console.WriteLine("enter email");
            person.email = readLine();
            addPerson(person);
        }

        public void addPerson(Person person)
        {
            if(checkRepeat(person))
            {
                everyone.Add(person);
                write();
            }
        }

        public void updatePerson()
        {
            Console.WriteLine("enter in email");
            string tempEmail = Console.ReadLine();
            Person tempPerson = new Person();
            var person = everyone.First(s => s.email == tempEmail);
            if (person != null)
            {
                Console.WriteLine("enter new first name");
                tempPerson.fName = readLine();
                Console.WriteLine("enter new last name");
                tempPerson.lName = readLine();
                Console.WriteLine("enter new email");
                tempPerson.email = readLine();
                if(checkRepeat(tempPerson))
                {
                    updatePerson(person, tempPerson);
                }
            }
            else
            {
                Console.WriteLine("that person is not alive, sorry");
            }
        }

        public void updatePerson(Person personStart, Person personEnd)
        {
            if(checkRepeat(personEnd))
            {
                everyone.Remove(personStart);
                everyone.Add(personEnd);
                write();
            }
        }

        public void deletePerson()
        {
            Console.WriteLine("enter in email");
            string email = readLine();
            var person = everyone.First(s => s.email == email);
            if(person != null)
            {
                deletePerson(person);
            }
            else
            {
                Console.WriteLine("That person is not alive, or has already been removed");
            }
        }

        public void deletePerson(Person person)
        {
            everyone.Remove(person);
            Console.WriteLine("EXTERMINATE");
            write();
        }

        public void readEveryone()
        {
            Console.WriteLine("Listing everyone");
            Console.WriteLine();
            Console.WriteLine("first name, last name, email");
            foreach (var person in everyone)
            {
                Console.WriteLine(person.fName + "," + person.lName + "," + person.email);
            }
            Console.WriteLine();
        }

        public void write()
        {
            using (StreamWriter file = new StreamWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            {
                foreach (var person in everyone)
                {
                    person.removeComma();
                    file.WriteLine(person.fName + "," + person.lName + "," + person.email);
                }
            }
        }

        public void read()
        {
            var everythingInFile = File.ReadAllLines(fileName);
            foreach (var s in everythingInFile)
            {
                var personParts = s.Split(',');
                Person person = new Person();
                person.fName = personParts[0];
                person.lName = personParts[1];
                person.email = personParts[2];
                everyone.Add(person);
            }
        }

        public string readLine()
        {
            string input = Console.ReadLine();
            if (input.ToLower().Equals("exit"))
                Environment.Exit(0);
            return input;
        }
    }




    public class Person
    {
        public string fName { get; set; }
        public string lName { get; set; }
        public string email { get; set; }

        public void removeComma()
        {
            fName = fName.Replace(',',' ');
            lName = lName.Replace(',',' ');
            email = email.Replace(',',' ');
        }
    }
}
