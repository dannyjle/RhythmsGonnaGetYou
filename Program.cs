using System;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Bands
    {

        // Accessing the list of Bands from our database from our C# code.

        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public string IsSigned { get; set; }
        public string ContactName { get; set; }
        public int ContactPhoneNumber { get; set; }

    }

    class Albums
    {

        // Accessing the list of Albums from our database from our C# code.

        public int Id { get; set; }
        public string Title { get; set; }
        public string IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }

        // This tells the Albums model that it can use the Bands property to return a Bands object.
        public Bands Bands { get; set; }

    }

    class Songs
    {

        // Accessing the list of Songs from our database from our C# code.

        public int Id { get; set; }
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public int AlbumId { get; set; }

        // This tells the Songs model that it can use the Albums property to return a Albums object.
        public Albums Albums { get; set; }

    }


    class RhythmsGonnaGetYouContext : DbContext
    {
        // Define a Bands property that is a DbSet.
        public DbSet<Bands> Bands { get; set; }
        public DbSet<Albums> Albums { get; set; }
        public DbSet<Songs> Songs { get; set; }

        // Define a method required by EF that will configure our connection
        // to the database.
        //
        // DbContextOptionsBuilder is provided to us. We then tell that object
        // we want to connect to a postgres database named MusicDatabase on
        // our local machine.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=MusicDatabase");
        }

    }
    class Program
    {

        // Boilerplate code for prompt input for both string/int

        // Method to take user's string prompt input
        static string PromptForString(string prompt)
        {


            Console.Write(prompt);
            var userInput = Console.ReadLine();
            return userInput;
        }

        // Method to take user's int prompt input
        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);
            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }
        }
        static void Main(string[] args)
        {
            //Create a greeting/welcome for “SDG MUSIC DATABASE” 

            Console.WriteLine();
            Console.WriteLine(new String('*', 34));
            Console.WriteLine("WELCOME TO THE SDG MUSIC DATABASE!");
            Console.WriteLine(new String('*', 34));
            Console.WriteLine();

            //Make a simple menu screen
            Console.WriteLine();
            Console.Write("What do you want to do? [V]iew Artists, [A]dd New Music Data, Artist [S]tatus, or [Q]uit ? ");
            var answer = Console.ReadLine().ToUpper();
            Console.WriteLine();




            var context = new RhythmsGonnaGetYouContext();


            // To see all of the bands, we can use a foreach loop:
            foreach (var band in context.Bands)
            {
                Console.WriteLine($"There is a band named {band.Name} in the database!");
            }

        }
    }
}
