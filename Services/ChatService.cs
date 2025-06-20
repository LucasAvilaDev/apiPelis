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


        // Método para guardar un mensaje en el chat
        public async Task GuardarMensaje(string chatId, Mensaje mensaje)
        {
            var filter = Builders<Chat>.Filter.Eq(c => c.Id, chatId);
            var update = Builders<Chat>.Update.Push(c => c.Mensajes, mensaje);
            await _chats.UpdateOneAsync(filter, update);
        }

        // Método para obtener un chat por su ID
        public async Task<Chat> ObtenerChatPorId(string chatId)
        {
            return await _chats.Find(c => c.Id == chatId).FirstOrDefaultAsync();
        }
    }
}
