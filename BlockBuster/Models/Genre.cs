using System.Collections.Generic;

namespace BlockBuster.Models
{
  public class Genre
  {
    public Genre()
    {
      this.JoinEntities = new HashSet<GenreMovie>();
    }

    public int GenreId { get; set; }
    public string GenreName { get; set; }
    public virtual ICollection<GenreMovie> JoinEntities { get; set; }
  }
}