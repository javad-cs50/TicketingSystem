using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem.Application.Abstractions.Authentication;

public sealed record RefreshTokenResult(string Token, DateTime ExpireAt);

