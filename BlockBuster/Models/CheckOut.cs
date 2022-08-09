using System;

namespace BlockBuster.Models
{
  public class CheckOut
  {
    public int CheckOutId { get; set; }
    public int CopyId  { get; set; }
    public int PatronId { get; set; }
    public DateTime DueDate { get; set; }
    public bool OverDue { get; set; }
    public virtual Copy Copy { get; set; }
    public virtual Patron Patron { get; set; }
  }
}