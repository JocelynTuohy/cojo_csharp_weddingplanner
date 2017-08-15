using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace weddingplanner.Models
{
  public class Wedding : BaseEntity
  {
    public int WeddingId { get; set; }
    [Required]
    public string Proposer { get; set; }
    [Required]
    public string Proposee { get; set; }
    [Required]
    [FutureDate]
    public DateTime WeddingDate { get; set; }
    [Required]
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UserId { get; set; }
    public User Creator { get; set; }
    public List<RSVP> RSVPs { get; set; }
    public Wedding()
    {
      RSVPs = new List<RSVP>();
    }
  }
}