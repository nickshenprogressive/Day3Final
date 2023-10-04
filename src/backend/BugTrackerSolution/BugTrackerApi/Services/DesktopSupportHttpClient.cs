﻿namespace BugTrackerApi.Services;

public interface IDesktopSupportHttpClient
{
    Task<SupportTicketResponse> SendSupportTicketAsync(SupportTicketRequest request);
}

public class DesktopSupportHttpClient : IDesktopSupportHttpClient
{

    private readonly HttpClient _httpClient;

    public DesktopSupportHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SupportTicketResponse> SendSupportTicketAsync(SupportTicketRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/support-tickets", request);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<SupportTicketResponse>();

        if (content is null)
        {
            throw new Exception("deal with this - won't happen");
        }
        return content;
    }


}


public record SupportTicketRequest
{
    public string Software { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
}

public record SupportTicketResponse
{
    public Guid TicketId { get; set; }
    public SupportTicketRequest Request { get; set; } = new();
}