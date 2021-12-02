// Code by Samuel Ward

using System.Text.Json;

namespace guests {
    public class GuestBook {
        private string filename = @"guestbook.json";
        private List<Guest> guests = new List<Guest>();

        // Read all existing guests
        public GuestBook() {
            if (File.Exists(@"guestbook.json")==true) {
                string jsonString = File.ReadAllText(filename);
                guests = JsonSerializer.Deserialize<List<Guest>>(jsonString);
            }
        }

        // Add new guest
        public Guest addGuest(Guest guest) {
            guests.Add(guest);
            marshal();
            return guest;
        }

        // Delete existing guest
        public int delGuest(int index) {
            guests.RemoveAt(index);
            marshal();
            return index;
        }

        // Returns existing guests as part of a List<T> Class
        public List<Guest> getGuests() {
            return guests;
        }

        // Serializes and saves guests to a .json file
        private void marshal() {
            var jsonString = JsonSerializer.Serialize(guests);
            File.WriteAllText(filename, jsonString);
        }
    }

    public class Guest {
        // Name of guest
        private string name;
        public string Name {
            set { this.name = value; }
            get { return this.name; }
        }

        // Message of guest
        private string post;
        public string Post {
            set { this.post = value; }
            get { return this.post; }
        }
    }

    // Class to run the console application
    class Program {
        static void Main(string[] args) {
            GuestBook guestbook = new GuestBook();
            int i = 0;

            while (true) {
                Console.Clear();
                Console.CursorVisible = false;

                Console.WriteLine("SAMUELS GÄSTBOK!\n");

                Console.WriteLine("1. Skriv i gästboken");
                Console.WriteLine("2. Ta bort inlägg\n");
                Console.WriteLine("X. Avsluta\n");

                i = 0;
                foreach (Guest guest in guestbook.getGuests()) {
                    Console.WriteLine("[" + i++ + "] " + guest.Name + " - " + guest.Post);
                }

                // Menu system
                int inp = (int) Console.ReadKey(true).Key;
                switch (inp) {
                    case '1':
                        Console.CursorVisible = true;

                        Console.Write("Ange namn: ");
                        string guestname = Console.ReadLine();
                        Console.Write("Ange meddelande: ");
                        string guestpost = Console.ReadLine();

                        Guest obj = new Guest();
                        obj.Name = guestname;
                        obj.Post = guestpost;

                        if (!String.IsNullOrEmpty(guestname) && !String.IsNullOrEmpty(guestpost)) {
                            guestbook.addGuest(obj);
                        }
                        break;
                    
                    case '2':
                        Console.CursorVisible = true;

                        Console.Write("Ange index att radera: ");
                        string index = Console.ReadLine();
                        guestbook.delGuest(Convert.ToInt32(index));
                        break;
                    
                    case 88:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}