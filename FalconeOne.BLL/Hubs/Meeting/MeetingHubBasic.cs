﻿using FalconeOne.BLL.Hubs.Common;
using FalconeOne.BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace FalconeOne.BLL.Hubs.Meeting
{

    public class MeetingHubBasic : BaseHub
    {
        public MeetingHubBasic(IUserLookupService userLookupService, ITenantProvider tenantProvider, ITokenValidationService tokenValidationService) : base(userLookupService, tenantProvider, tokenValidationService)
        {
        }

        public async Task sendOffer(string callerId, string calleeId, string message)
        {
            await Clients.Client(calleeId).SendAsync("receiveOffer", message, callerId);
        }

        public async Task sendAnswer(string callerId, string message)
        {
            await Clients.Client(callerId).SendAsync("receiveAnswer", Context.ConnectionId, message);
        }

        public async Task sendIceCandidate(string message)
        {
            await Clients.All.SendAsync("receiveIceCandidate", message);
        }
    }
}
