using System.Collections.Generic;

namespace BlockBuster.Models
{
  public class Copy
  {
    public Copy()
    {

    }
    public Copy(string copyName, int movieId)
    {
      CopyName = copyName;
      MovieId = movieId;
      CheckedOut = false;
    }
    
    public string CopyName { get; set; }
    public int CopyId { get; set; }
    public int MovieId { get; set; }
    public bool CheckedOut { get; set; }
    
    public virtual Movie Movie { get; set; }
  }
}