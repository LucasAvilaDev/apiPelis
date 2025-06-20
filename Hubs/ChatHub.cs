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

            // En tu ChatHub.cs, dentro de EnviarMensaje
            try
            {
                Console.WriteLine($"Guardando mensaje en el chat {chatId}: {mensaje.Contenido} de {mensaje.Remitente} a las {mensaje.Timestamp}");
                await _chatService.GuardarMensaje(chatId, mensaje);
                Console.WriteLine("Mensaje guardado exitosamente en DB.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR al guardar mensaje en DB: {ex.Message}");
                // Loggear la excepción completa para más detalles
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }

            // Emitir el mensaje a todos los clientes en la sala del chat
            await Clients.Group(chatId).SendAsync("RecibirMensaje", mensaje);
        }

        // Método para que el cliente se una al chat (por ID)
        public async Task UnirseChat(string chatId)
        {
            Console.WriteLine($"Cliente conectado: {Context.ConnectionId}");

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        // Método para que el cliente se desconecte del chat
        public async Task DejarChat(string chatId)
        {
            Console.WriteLine($"Cliente desconectado: {Context.ConnectionId}");

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
