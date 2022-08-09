using System;

namespace BlockBuster.Models
{
  public class CheckOut
  {
    public CheckOut()
    {
      DueDate = DateTime.Now.AddDays(3);
    }
    public int CheckOutId { get; set; }
    public int CopyId  { get; set; }
    public int PatronId { get; set; }
    public DateTime DueDate { get; set; }
    public virtual Copy Copy { get; set; }
    public virtual Patron Patron { get; set; }
  }
}