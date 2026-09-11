namespace TicketingSystem.Api.Common;

public sealed record ApiResponse<T>(T? Data, object? Links = null);