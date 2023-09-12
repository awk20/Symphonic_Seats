namespace SymphonicSeats2.Models;

public class ChcekoutResponseModel
{
    // Each payment goes to a session which each have an id
    // Get all session data from the Id
    public string SessionId { get; set; }

    // Publishable key
    // Sends a repsonse when purchase is clicked and opens Strip payment window 
    // and passes the data into Stripe
    public string? PubKey { get; set; }
}