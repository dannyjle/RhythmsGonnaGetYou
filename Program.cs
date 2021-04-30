using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Band
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

    class Album
    {

        // Accessing the list of Albums from our database from our C# code.

        public int Id { get; set; }
        public string Title { get; set; }
        public string IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int BandId { get; set; }

        // This tells the Albums model that it can use the Bands property to return a Bands object.
        public Band Bands { get; set; }

    }

    class Song
    {

        // Accessing the list of Songs from our database from our C# code.

        public int Id { get; set; }
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public int AlbumId { get; set; }

        // This tells the Songs model that it can use the Albums property to return a Albums object.
        public Album Albums { get; set; }

    }


    class RhythmsGonnaGetYouContext : DbContext
    {
        // Define a Bands property that is a DbSet.
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

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
            var context = new RhythmsGonnaGetYouContext();
            //Create a greeting/welcome for “SDG MUSIC DATABASE” 

            Console.WriteLine();
            Console.WriteLine(new String('*', 34));
            Console.WriteLine("WELCOME TO THE SDG MUSIC DATABASE!");
            Console.WriteLine(new String('*', 34));
            Console.WriteLine();



            var whileRunning = true;

            while (whileRunning)


            {
                // Make a simple menu screen
                Console.WriteLine();
                Console.Write("What do you want to do? [V]iew Artists, [A]dd New Music Data, Artist [S]tatus, or [Q]uit ? ");
                var answer = Console.ReadLine().ToUpper();
                Console.WriteLine();



                if (answer == "V")
                {

                    Console.WriteLine();
                    Console.Write("What do you want to do? View [A]ll Bands, [S]elect A Band & View Their Album(s), or View by Release [D]ate ? ");
                    var choice = Console.ReadLine().ToUpper();
                    Console.WriteLine();

                    if (choice == "A")
                    {
                        // View all bands
                        var bands = context.Bands.Count();
                        Console.WriteLine(new String('-', 30));
                        Console.WriteLine($"There are {bands} in the database!");
                        Console.WriteLine(new String('-', 30));

                        // To see all of the bands, we can use a foreach loop:
                        foreach (var band in context.Bands)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{band.Name}");
                            Console.WriteLine();
                        }
                    }

                    else if (choice == "S")
                    {

                    }
                    // code to view albums 
                    // Console.WriteLine();

                    // var bandsWithAlbums = context.Albums.Include(album => album.Bands);

                    // foreach (var album in bandsWithAlbums)
                    // {
                    //     Console.WriteLine($"There is an album named {album.Title} by {album.Bands.Name}");
                    // }

                    // else if (choice == "S")
                    // {

                    // }

                }

                //IF Quit 
                else if (answer == "Q")
                {
                    whileRunning = false;
                    Console.WriteLine("THANKS FOR USING THE SDG MUSIC DATABASE.... GOODBYE :)");
                }
            }

        }

    }
}

