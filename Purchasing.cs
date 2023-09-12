using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using SymphonicSeats2.Models;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Client.Pages;

public partial class Index
{
    [Inject]
    public HttpClient HttpClient { get; set; } = default!;

    [Inject]
    public IJSRuntime JsRuntime { get; set; } = default!;

    private List<CollectionItem>? _items;
    private IEnumerable<CollectionItem[]>? _productChunksOf4;

    private const string DevApiBaseAddress = "https://localhost:5270";

    protected async Task OnInitializedAsync()
    {
        _items = await HttpClient.GetFromJsonAsync<List<CollectionItem>>($"{DevApiBaseAddress}/products");

        if (_items is not null)
        {
            _productChunksOf4 = _items.Chunk(4);
        }
    }

    private async Task OnClickBtnBuyNowAsync(CollectionItem item)
    {
        var response = await HttpClient.PostAsJsonAsync($"{DevApiBaseAddress}/checkout", item);

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        var checkoutOrderResponse = JsonConvert.DeserializeObject<ChcekoutResponseModel>(responseBody);

        // Opens up Stripe.
        await JsRuntime.InvokeVoidAsync("checkout", checkoutOrderResponse.PubKey,
            checkoutOrderResponse.SessionId);
    }
}