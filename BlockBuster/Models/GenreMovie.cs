namespace BlockBuster.Models
{
  public class GenreMovie
  {
    public int GenreMovieId { get; set; }
    public int MovieId { get; set;}
    public int GenreId { get; set; }
    public virtual Movie Movie { get; set; }
    public virtual Genre Genre { get; set; }
  }
}