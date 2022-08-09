using System.Collections.Generic;

namespace BlockBuster.Models
{
  public class Patron
  {
    public int PatronId { get; set; }
    public string PatronName { get; set; }
    public virtual ICollection<CheckOut> JoinEntities { get; set; }
  }
}