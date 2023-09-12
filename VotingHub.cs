using Microsoft.AspNetCore.SignalR;
using SymphonicSeats2.Models;

namespace SymphonicSeats2;


public class VotingHub : Hub
{

    private readonly CollectionItemRepository _repository;

    // Recieve colletion Repo and set it
    public VotingHub(CollectionItemRepository repository)
    {
        _repository = repository;
    }

    // passes in the id of the item being voted on and whther or not it is an up or down vote
    public async Task SendVote(int collectionItemId, bool voteDirection)
    {
        // Call Vote() from CollectionItemRepository.cs
        var newVoteTotal = _repository.Vote(collectionItemId, voteDirection);

        // Communicate with all clieyts and send mesaage 
        // for the specified collection and send each client the newVoteTotal
        await Clients.All.SendAsync("ReceiveVote", collectionItemId, newVoteTotal);
    }
}