using System;
using System.ComponentModel.DataAnnotations;

namespace weddingplanner.Models
{
  public class CreateWeddingViewModel : BaseEntity
  {
    [Required]
    public string Proposer { get; set; }

    [Required]
    public string Proposee { get; set; }

    [Required]
    [FutureDate]
    public DateTime WeddingDate { get; set; }

    [Required]
    public string Address { get; set; }
  }
}