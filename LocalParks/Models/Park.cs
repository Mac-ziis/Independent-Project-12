using System.ComponentModel.DataAnnotations;

namespace LocalParks.Models
{
  public class Park
  {
    [Required]
    public int ParkId { get; set; }
    public string Name { get; set; }
    public string Location {get; set;}
    public string Summary {get; set; }
  
  }
}