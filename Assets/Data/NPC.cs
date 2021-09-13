using System;
using System.Collections.Generic;

public enum NPC
{
    YuHuan,
}

public class NPCDict : Dictionary<NPC, string>
{
    private static NPCDict instance = new NPCDict();
    public static NPCDict Instance
    {
        get { return instance; }
    }

    public NPCDict()
    {
        this.Add(NPC.YuHuan, "小乔");
    }
}
