using System;
public enum HintType
{
    Message,
    Response,
}

public class Hint
{
    public HintType Type { get; set; }

    public string Message { get; set; }
}
