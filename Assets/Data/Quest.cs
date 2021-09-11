using System;
using System.Collections.Generic;

public class Quest
{
    public Location Location { get; set; }

    public int Id { get; set; }

    public Message Msg { get; set; }

    public List<string> Answers { get; set; }

    public List<Hint> Hints { get; set; }

    public int NextQuestId { get; set; }

    public Location NextQuestLocation { get; set; }

    public string NextMessage { get; set; }

    public string Key { get { return Location.ToString() + Id.ToString(); } }
}
