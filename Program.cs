using System.Diagnostics.Metrics;

namespace СS_interfaces
{
    enum Genre
    {
        Comedy, Horror, Adventure, Drama
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

        private string[] toString = new string[]{ "Comedy", "Horror", "Adventure", "Drama" };

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
            Movie movie = new Movie(Title, Country, genre, Year, Rating);

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

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}