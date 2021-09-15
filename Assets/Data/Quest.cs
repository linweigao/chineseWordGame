using System;
using System.Collections.Generic;

public enum QuestId
{
    None=0,
    Start,
    Name,
    Map,
    BlackMan,
    GoHome,
    MeetYuHuan,
    TalkYuHuan,
}

public class Quest
{
    public Quest()
    {
        this.PreQuestIds = new List<QuestId>();
    }

    public Location Location { get; set; }

    public QuestId Id { get; set; }

    public Message Msg { get; set; }

    public List<Tag> Answers { get; set; }

    public List<Hint> Hints { get; set; }

    public List<QuestId> PreQuestIds { get; set; }

    public string PreQuestNotMeet { get; set; }

    public QuestId NextQuestId { get; set; }

    public string NextMessage { get; set; }

    public bool AutoNext { get; set; }
}
