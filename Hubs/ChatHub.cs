using apiPelis.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace apiPelis.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        // Método para enviar un mensaje
        public async Task EnviarMensaje(string chatId, string remitente, string contenido)
        {
            var mensaje = new Mensaje
            {
                Remitente = remitente,
                Contenido = contenido,
                Timestamp = DateTime.UtcNow
            };

            // Guardar mensaje en la base de datos
            await _chatService.GuardarMensaje(chatId, mensaje);

            // Emitir el mensaje a todos los clientes en la sala del chat
            await Clients.Group(chatId).SendAsync("RecibirMensaje", mensaje);
        }

        // Método para que el cliente se una al chat (por ID)
        public async Task UnirseChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        // Método para que el cliente se desconecte del chat
        public async Task DejarChat(string chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
