using System;
using System.Collections.Generic;

[Serializable]
public class PlayerState
{
    public const string PlayerStatePath = "play.dat";

    private static PlayerState instance = new PlayerState();

    public static PlayerState Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public static PlayerState Load()
    {
        return SaveLoadUtil.LoadData<PlayerState>(PlayerStatePath);
    }

    public PlayerState()
    {
        this.PassedQuests = new List<QuestId>();
    }

    public string Name { get; set; }

    public QuestId CurrentQuestId { get; set; }

    public List<QuestId> PassedQuests { get; set; }

    public void Save()
    {
        SaveLoadUtil.SaveData(this, PlayerStatePath);
    }
}

