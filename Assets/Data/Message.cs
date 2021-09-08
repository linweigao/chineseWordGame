public enum MessageType
{
    SystemMessage,
    SelfMessage,
    UserMessage
}


public class Message
{
    public MessageType Type { get; set; }

    public string Content { get; set; }

    public string From { get; set; }
}