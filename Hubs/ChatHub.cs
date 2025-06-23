using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using apiPelis.Services;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace apiPelis.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> userConnectionMap = new();
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                userConnectionMap[userId] = Context.ConnectionId;
                Console.WriteLine($"[Connected] UserId={userId}, ConnectionId={Context.ConnectionId}");
                await SendActiveUsers();
            }
            else
            {
                Console.WriteLine($"[Warning] Connection without userId. ConnectionId={Context.ConnectionId}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = userConnectionMap.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(userId))
            {
                userConnectionMap.TryRemove(userId, out _);
                Console.WriteLine($"[Disconnected] UserId={userId}, ConnectionId={Context.ConnectionId}");
            }
            await SendActiveUsers();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string senderId, string receiverId, string messageContent)
        {
            var message = new Mensaje
            {
                Remitente = senderId,
                Contenido = messageContent,
                Timestamp = DateTime.UtcNow
            };

            var chatId = string.Join("_", new[] { senderId, receiverId }.OrderBy(id => id));

            await _chatService.GuardarMensaje(chatId, message);
            Console.WriteLine($"[MongoDB] Message saved for chatId={chatId}");

            var receiverConnectionId = GetConnectionId(receiverId);
            var senderConnectionId = GetConnectionId(senderId);

            if (!string.IsNullOrEmpty(receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", senderId, messageContent, chatId);
                Console.WriteLine($"[SignalR] Sent to {receiverId} (Conn: {receiverConnectionId})");
            }

            if (!string.IsNullOrEmpty(senderConnectionId))
            {
                await Clients.Client(senderConnectionId).SendAsync("MessageSentConfirmation", receiverId, messageContent, chatId);
                Console.WriteLine($"[SignalR] Confirmation to {senderId} (Conn: {senderConnectionId})");
            }
        }

        public async Task<List<Mensaje>> GetChatHistory(string chatId)
        {
            var messages = await _chatService.ObtenerMensajesDeChat(chatId);
            Console.WriteLine($"[History] Fetched {messages?.Count ?? 0} messages for chatId={chatId}");
            return messages;
        }

        public async Task<List<string>> GetActiveUsers()
        {
            var activeUserIds = userConnectionMap.Keys.ToList();
            Console.WriteLine($"[ActiveUsers] Requested: {string.Join(", ", activeUserIds)}");
            return await Task.FromResult(activeUserIds);
        }

        private async Task SendActiveUsers()
        {
            var activeUserIds = userConnectionMap.Keys.ToList();
            await Clients.All.SendAsync("ActiveUsers", activeUserIds);
            Console.WriteLine($"[Broadcast] Active users: {string.Join(", ", activeUserIds)}");
        }

        private string GetConnectionId(string userId)
        {
            userConnectionMap.TryGetValue(userId, out string connectionId);
            return connectionId;
        }

        public async Task EnviarMensajeAGrupo(string chatId, string remitente, string contenido)
        {
            var mensaje = new Mensaje
            {
                Remitente = remitente,
                Contenido = contenido,
                Timestamp = DateTime.UtcNow
            };

            await _chatService.GuardarMensaje(chatId, mensaje);
            Console.WriteLine($"[Grupo:{chatId}] Guardado y enviado mensaje de {remitente}");

            await Clients.Group(chatId).SendAsync("RecibirMensaje", remitente, contenido, chatId);
        }
        public async Task UnirseAGrupo(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            Console.WriteLine($"[Grupo:{chatId}] {Context.ConnectionId} se unió");
        }

    }
}