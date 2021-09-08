using System;
using System.Collections.Generic;

public class Quest
{
    public string Id { get; set; }

    public Message Msg { get; set; }

    public List<Response> Responses { get; set; }

}
