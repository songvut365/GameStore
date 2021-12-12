using System.Collections;
using GameStore.Data;

namespace GameStore.Models
{
  public class Cart
  { 
    public int id {get; set;}
    public int gameId {get; set;}
    public int count {get; set;}
    public decimal totalPrice {get; set;}
  }
}