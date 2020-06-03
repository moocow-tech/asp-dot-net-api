using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace QandA.Hubs
{
    public class AppointmentHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("Message", "Successfully connected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Caller.SendAsync("Message", "Successfully disconnected");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SubscribeAppointment(int appointmentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Appointment-{appointmentId}");
            await Clients.Caller.SendAsync("Message", "Successfully subscribed");
        }

        public async Task UnsubscribeQuestion(int appointmentId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId,
                $"Appointment-{appointmentId}");
            await Clients.Caller.SendAsync("Message",
                "Succesfully unsubscribed");
        }
    }
}
