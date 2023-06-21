using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Director { get; set; }

        public int Year { get; set; }

        public int Duration { get; set; }

        //public string image { get; set; }

        [ForeignKey("categoryId")]
        public int CategoryId { get; set; }
        
        public Category Category{ get; set; }
    }
}
