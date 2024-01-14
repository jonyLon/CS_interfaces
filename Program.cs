using System.Collections;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;

namespace СS_interfaces
{
    enum Genre
    {
        Comedy, Horror, Adventure, Drama, Romance, Action, ScienceFiction
    }

    class Director : ICloneable
    {
        protected string FirstName { get; set; }
        protected string LastName { get; set; }

        public Director(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public override string ToString()
        {
            return $"Director: {FirstName} {LastName}";
        }

        public object Clone()
        {
            Director director = new Director(FirstName, LastName);
            return director;
        }
    }
    class Movie : ICloneable, IComparable
    {
        public string Title { get; private set; }
        public string Country { get; private set; }
        public Genre genre { get; private set; }
        public int Year { get; private set; }
        public int Rating { get; private set; }

        private string[] toString = new string[]{ "Comedy", "Horror", "Adventure", "Drama", "Romance", "Action", "ScienceFiction" };

        public Movie(string title, string country, Genre genre, int year, int rating)
        {
            Title = title;
            Country = country;
            this.genre = genre;
            Year = year;
            Rating = rating;
        }
        public override string ToString()
        {
            return $"\tTitle: {Title}\n\tCountry {Country}\n\tGenre {toString[(int)genre]}\n\tYear: {Year}\n\tRating: {Rating}";
        }


        public object Clone()
        {
            Movie movie = new(Title, Country, genre, Year, Rating);

            movie.toString = new string[this.toString.Length];
            for (int i = 0; i < this.toString.Length; i++)
            {
                movie.toString[i] = (string)this.toString[i].Clone();
            }
            return movie;
        }

        public int CompareTo(object? obj)
        {
            if(obj is Movie) {
                return Title.CompareTo(((Movie)obj).Title);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }



    class Cinema : IEnumerable
    {

        public Movie[] movies { get; private set; }
        public string address { get; private set; }

        public Cinema(string address, params Movie[] movies) {
            this.address = address;
            this.movies = movies;
        }

        public void Sort()
        {
            Array.Sort(this.movies);
        }

        public void Sort(IComparer comparer)
        {
            Array.Sort(this.movies, comparer);
        }


        public IEnumerator GetEnumerator()
        {
            foreach (var item in movies)
            {
                yield return item;
            }
        }

        public void ShowMovies()
        {
            foreach (var item in this.movies)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine();
            }
        }
    }
    class YearComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is Movie && y is Movie)
            {
                return ((x as Movie).Year).CompareTo((y as Movie).Year);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
    class RatingComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is Movie && y is Movie)
            {
                return ((x as Movie).Rating).CompareTo((y as Movie).Rating);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Cinema cinema = new Cinema("Unknown",
            new Movie("Inception", "USA", Genre.ScienceFiction, 2010, 8),
            new Movie("The Shawshank Redemption", "USA", Genre.Drama, 1994, 9),
            new Movie("The Dark Knight", "USA", Genre.Action, 2008, 9),
            new Movie("Forrest Gump", "USA", Genre.Comedy, 1994, 8),
            new Movie("Titanic", "USA", Genre.Romance, 1997, 7),
            new Movie("Get Out", "USA", Genre.Horror, 2017, 8));

            cinema.ShowMovies();
            Console.WriteLine("======================= Sort by title ===========================");
            cinema.Sort();
            cinema.ShowMovies();
            Console.WriteLine("======================= Sort by year ===========================");
            cinema.Sort(new YearComparer());
            cinema.ShowMovies();
            Console.WriteLine("======================= Sort by rating ===========================");
            cinema.Sort(new RatingComparer());
            cinema.ShowMovies();
        }
    }
}