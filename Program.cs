﻿using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
logger.Info(scrubbedFile);
MovieFile movieFile = new MovieFile(scrubbedFile);

// Create a new instance of the Movie class
Movie movie = new Movie();

// Prompt the user to enter the movie title
Console.WriteLine("Enter the movie title:");
movie.title = Console.ReadLine();

// Prompt the user to enter the movie genres
Console.WriteLine("Enter the movie genres (separated by commas):");
string genres = Console.ReadLine();
movie.genres = genres.Split(',').ToList();

// Prompt the user to enter the movie director
Console.WriteLine("Enter the movie director:");
movie.director = Console.ReadLine();

// Prompt the user to enter the movie running time
Console.WriteLine("Enter the movie running time (in minutes):");
int runningTimeInMinutes = int.Parse(Console.ReadLine());
movie.runningTime = TimeSpan.FromMinutes(runningTimeInMinutes);

// Add the movie to the list of movies
List<Movie> Movies = new List<Movie>();

// Display all movies
Console.WriteLine("All movies:");
foreach (Movie m in Movies)
{
    Console.WriteLine($"ID: {m.mediaId}, Title: {m.title}, Genres: {string.Join(", ", m.genres)}, Director: {m.director}, Running Time: {m.runningTime}");
}

logger.Info("Program ended");