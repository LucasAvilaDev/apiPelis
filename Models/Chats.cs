

public class Chat
{
    public string Id { get; set; }
    public string ClienteId { get; set; }
    public string AdminId { get; set; }
    public List<Mensaje> Mensajes { get; set; } = new();
}
