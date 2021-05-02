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
        public string ReleaseDate { get; set; }
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
        public int Duration { get; set; }
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
                Console.WriteLine(new String('~', 90));
                Console.WriteLine("What do you want to do? [V]iew Artists, [A]dd New Music Data, Artist [S]tatus, or [Q]uit? ");
                Console.WriteLine(new String('~', 90));
                var answer = Console.ReadLine().ToUpper();
                Console.WriteLine();


                // IF VIEW
                if (answer == "V")
                {

                    Console.WriteLine(new String('~', 114));
                    Console.WriteLine("What do you want to do? View [A]ll Bands, [S]elect an Artist & View Their Discography, or View by [R]elease Date? ");
                    Console.WriteLine(new String('~', 114));
                    var choice = Console.ReadLine().ToUpper();
                    Console.WriteLine();

                    // IF VIEW ALL BANDS
                    if (choice == "A")
                    {
                        var bands = context.Bands.Count();
                        Console.WriteLine(new String('-', 30));
                        Console.WriteLine($"There are {bands} in the database!");
                        Console.WriteLine(new String('-', 30));

                        // To see all of the bands in database
                        foreach (var band in context.Bands)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{band.Name}");
                            Console.WriteLine();
                        }
                    }


                    // To select a band and view their albums in database
                    // else if (choice == "S")
                    // {
                    //     Console.WriteLine();
                    //     Console.Write("Which artist did you want to select? ");
                    //     var selection = Console.ReadLine();
                    //     Console.WriteLine();

                    //     if (selection == "Band.Id")
                    //     {

                    //     }

                    // }

                    // IF VIEW BY RELEASE DATE
                    else if (choice == "R")
                    {
                        var ReleaseDate = context.Albums.Include(album => album.Bands).OrderBy(album => album.ReleaseDate);

                        foreach (var album in ReleaseDate)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"The album {album.Title} by {album.Bands.Name}");
                            Console.WriteLine($"was released on {album.ReleaseDate}");
                            Console.WriteLine();

                        }
                    }


                }

                // IF ADD
                else if (answer == "A")
                {
                    Console.WriteLine(new String('~', 58));
                    Console.WriteLine("What do you want add? A [N]ew Artist, [A]lbum, or [S]ong? ");
                    Console.WriteLine(new String('~', 58));
                    var choice = Console.ReadLine().ToUpper();
                    Console.WriteLine();

                    // IF ADD NEW BAND
                    if (choice == "N")
                    {
                        var newBand = new Band();
                        newBand.Id = PromptForInteger("What is the ID number of the new artist/band? ");
                        newBand.Name = PromptForString("What is the name of the new artist/band? ");
                        newBand.CountryOfOrigin = PromptForString("What is the artist/band's country of origin? ");
                        newBand.NumberOfMembers = PromptForInteger("How many members does this artist/band have? ");
                        newBand.Website = PromptForString("What is the artist/band's website? ");
                        newBand.Style = PromptForString("What is the artist/band's genre? ");
                        newBand.IsSigned = PromptForString("Are they signed? [Yes/No] ");
                        newBand.ContactName = PromptForString("Who is the manager for this artist/band? ");
                        newBand.ContactPhoneNumber = PromptForInteger("What their extention? [4 Digits] ");

                        context.Bands.Add(newBand);
                        context.SaveChanges();
                    }

                    // IF ADD NEW ALBUM
                    else if (choice == "A")
                    {
                        var newAlbum = new Album();
                        newAlbum.Id = PromptForInteger("What is the ID number of the new album? ");
                        newAlbum.Title = PromptForString("What is the name of the new album? ");
                        newAlbum.IsExplicit = PromptForString("Is the album explicit ? [Yes/No] ");
                        newAlbum.ReleaseDate = PromptForString("When was the album released ? [MM/DD/YYY] ");
                        newAlbum.BandId = PromptForInteger("What is the band ID of the new album? ");

                        context.Albums.Add(newAlbum);
                        context.SaveChanges();

                    }
                    // IF ADD NEW SONG
                    else if (choice == "S")
                    {
                        var newSong = new Song();
                        newSong.Id = PromptForInteger("What is the ID number of the new ? ");
                        newSong.TrackNumber = PromptForInteger("What is the track number of the new song? ");
                        newSong.Title = PromptForString("What is the name of the new song? ");
                        newSong.Duration = PromptForInteger("What is the duration of the song? [Seconds] ");
                        newSong.AlbumId = PromptForInteger("What is the album ID number? ");

                        context.Songs.Add(newSong);
                        context.SaveChanges();
                    }
                }

                //IF STATUS
                else if (answer == "S")
                {
                    Console.WriteLine(new String('~', 62));
                    Console.WriteLine("Did you want to [V]iew or [U]pdate an Artist's signing status? ");
                    Console.WriteLine(new String('~', 62));
                    var choice = Console.ReadLine().ToUpper();
                    Console.WriteLine();

                    if (choice == "V")
                    {
                        Console.WriteLine("The Artists that ARE currently signed are:");
                        Console.WriteLine();
                        foreach (var band in context.Bands)
                        {
                            if (band.IsSigned == "Yes")
                                Console.WriteLine(band.Name);
                        }
                        Console.WriteLine();
                        Console.WriteLine();

                        Console.WriteLine("The Artists that are currently NOT signed are:");
                        Console.WriteLine();
                        foreach (var band in context.Bands)
                        {
                            if (band.IsSigned == "No")
                                Console.WriteLine(band.Name);
                        }
                    }



                    else if (choice == "U")
                    {
                        var name = PromptForString("What is the name of the artist/band you want to update?");

                        var selectedBand = context.Bands.FirstOrDefault(band => band.Name == name);
                        if (selectedBand == null)
                        {
                            Console.WriteLine("Sorry! There are no artist/band by that name in the database!");
                        }
                        else
                        {
                            var isOrNot = PromptForString($"Do you want to [S]ign or [L]et go of {name}? ").ToUpper();
                            if (isOrNot == "D")
                            {
                                Console.WriteLine($"You let go of *{name}*");
                                isOrNot = "False";
                                selectedBand.IsSigned = isOrNot;
                                context.SaveChanges();
                            }
                            else if (isOrNot == "D")
                            {
                                Console.WriteLine($"You signed *{name}*");
                                isOrNot = "True";
                                selectedBand.IsSigned = isOrNot;
                                context.SaveChanges();
                            }
                        }
                    }

                }


                //IF QUIT 
                else if (answer == "Q")
                {
                    whileRunning = false;
                    Console.WriteLine("THANKS FOR USING THE SDG MUSIC DATABASE.... GOODBYE :)");
                }
            }

        }

    }
}

