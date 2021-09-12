using System.Collections.Generic;

public class PlayerState
{
    private static PlayerState instance = new PlayerState();

    public static PlayerState Instance
    {
        get { return instance; }
    }

    private PlayerState()
    {
        this.PassedQuests = new List<QuestId>();
    }

    public string Name { get; set; }

    public QuestId CurrentQuestId { get; set; }

    public List<QuestId> PassedQuests { get; set; }
}

