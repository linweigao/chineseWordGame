using System;
using System.Collections.Generic;

public class Quest
{
    public string Id { get; set; }

    public Message Msg { get; set; }

    public List<string> Answers { get; set; }

    public List<Hint> Hints { get; set; }

    public string Next { get; set; }
}
