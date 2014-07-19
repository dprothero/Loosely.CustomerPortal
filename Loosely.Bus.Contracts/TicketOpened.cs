namespace Loosely.Bus.Contracts
{
  public interface TicketOpened
  {
    string Id { get; }
    string CustomerEmail { get; set; }
    string Message { get; set; }
  }
}
