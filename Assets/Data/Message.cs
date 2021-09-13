public enum MessageType
{
    SystemMessage,
    SelfMessage,
    NPCMessage
}


public class Message
{
    public MessageType Type { get; set; }

    public string Content { get; set; }

    public NPC From { get; set; }
}