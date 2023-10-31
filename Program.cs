﻿using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

    static void Main()
    {
        string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
        if (!string.IsNullOrEmpty(scrubbedFile))
        {
            MovieFile movieFile = new MovieFile(scrubbedFile);
            bool continueRunning = true;

            while (continueRunning)
            {
                Console.WriteLine("Movie Library Program");
                Console.WriteLine("1. Display All Movies");
                Console.WriteLine("2. Add a New Movie");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice (1/2/3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllMovies(movieFile);
                        break;
                    case "2":
                        Movie newMovie = GetNewMovie();
                        if (newMovie != null)
                        {
                            movieFile.AddMovie(newMovie);
                            Console.WriteLine("Movie added successfully.");
                        }
                        break;
                    case "3":
                        continueRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        logger.Info("Program ended");
    }

    static Movie GetNewMovie()
    {
        Console.WriteLine("Enter details for the new movie:");
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Director: ");
        string director = Console.ReadLine();
        Console.Write("Running Time (hh:mm:ss): ");
        if (TimeSpan.TryParse(Console.ReadLine(), out TimeSpan runningTime))
        {
            Movie newMovie = new Movie();
            newMovie.title = title;
            newMovie.director = director;
            newMovie.runningTime = runningTime;
            return newMovie;
        }
        else
        {
            Console.WriteLine("Invalid Running Time format. Movie not added.");
            return null;
        }
    }

    static void DisplayAllMovies(MovieFile movieFile)
    {
        Console.WriteLine("All Movies:");
        foreach (Movie movie in movieFile.Movies)
        {
            Console.WriteLine(movie.Display());
        }
    }
}