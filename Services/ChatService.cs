using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
namespace apiPelis.Services
{
    public class ChatService
    {
        private readonly IMongoCollection<Chat> _chats;

        public ChatService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:Connection"]);
            var database = client.GetDatabase(config["MongoDB:Database"]);
            _chats = database.GetCollection<Chat>("Chats");
        }


        // En MongoDbChatService.cs, dentro de GuardarMensaje
public async Task GuardarMensaje(string chatId, Mensaje mensaje)
{
    // ...
    Console.WriteLine($"MongoDB: Buscando/Creando chat con ID: {chatId}");
    Console.WriteLine($"MongoDB: Mensaje a agregar: Remitente={mensaje.Remitente}, Contenido={mensaje.Contenido}, Timestamp={mensaje.Timestamp}");

    var filter = Builders<Chat>.Filter.Eq(c => c.Id, chatId);
    var update = Builders<Chat>.Update.Push(c => c.Mensajes, mensaje);
    var options = new UpdateOptions { IsUpsert = true };

    var result = await _chats.UpdateOneAsync(filter, update, options);

    Console.WriteLine($"MongoDB Upsert Result: MatchedCount={result.MatchedCount}, ModifiedCount={result.ModifiedCount}, UpsertedId={result.UpsertedId}");

    if (result.MatchedCount == 0 && result.UpsertedId != null)
    {
        Console.WriteLine($"Nuevo chat '{chatId}' creado en la base de datos.");
    }
    else if (result.MatchedCount > 0)
    {
        Console.WriteLine($"Mensaje añadido al chat existente '{chatId}'.");
    }
    // ...
}

        // Método para obtener un chat por su ID
        public async Task<Chat> ObtenerChatPorId(string chatId)
        {
            return await _chats.Find(c => c.Id == chatId).FirstOrDefaultAsync();
        }

        public async Task<List<Mensaje>> ObtenerMensajesDeChat(string chatId)
        {
            var chat = await _chats.Find(c => c.Id == chatId).FirstOrDefaultAsync();
            // If chat is found, return its messages. Otherwise, return an empty list.
            return chat?.Mensajes ?? new List<Mensaje>();
        }
    }
}