using System;
using System.Collections.Generic;

[Serializable]
public class PlayerState
{
    public const string PlayerStatePath = "play.dat";

    private static object obj = new object();
    private static PlayerState instance;

    public static PlayerState Instance
    {
        get
        {
            if (instance == null)
            {
                lock (obj)
                {
                    instance = SaveLoadUtil.LoadData<PlayerState>(PlayerStatePath);
                    if (instance == null)
                    {
                        instance = new PlayerState();
                    }
                }
            }

            return instance;
        }
    }

    private PlayerState()
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

