using System.Collections.Generic;

namespace BlockBuster.Models
{
  public class Movie
  {
    public Movie()
    {
      this.JoinEntities = new HashSet<GenreMovie>();
    }
    public int MovieId { get; set; }
    public string MovieName { get; set; }
    public int MovieCopies { get; set; }
    public virtual ICollection<GenreMovie> JoinEntities { get; set; }
  }
}