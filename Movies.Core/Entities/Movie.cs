using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Entities
{
    public class Movie
    {
        [Key]
        public int ID { get; set; }
        public string MovieName { get; set; }
        public string DirectorName { get; set; }
        public string ReleaseYear { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Movie other)
                return false;

            return ID == other.ID &&
                   string.Equals(MovieName, other.MovieName, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(DirectorName, other.DirectorName, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(ReleaseYear, other.ReleaseYear, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID,
                                    MovieName?.ToLowerInvariant(),
                                    DirectorName?.ToLowerInvariant(),
                                    ReleaseYear?.ToLowerInvariant());
        }
    }
}
