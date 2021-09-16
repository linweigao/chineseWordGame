using System;
using System.Collections.Generic;

public class LocationMap
{
    public Location Location { get; set; }
    public string MiddleText { get; set; }
    public string TopLeftText { get; set; }
    public string TopRightText { get; set; }
    public string BottomLeftText { get; set; }
    public string BottomRightText { get; set; }
    public QuestId MiddleQuest { get; set; }
    public QuestId TopLeftQuest { get; set; }
    public QuestId TopRightQuest { get; set; }
    public QuestId BottomLeftQuest { get; set; }
    public QuestId BottomRightQuest { get; set; }
}

public class MapDict: Dictionary<Location, LocationMap>
{
    private static MapDict instance = new MapDict();

    public static MapDict Instance
    {
        get { return instance; }
    }

    private MapDict()
    {
        LocationMap map = new LocationMap
        {
            Location = Location.村后树林,
            MiddleText = "路",
            TopLeftText = "<color=yellow>山山山山\n 山山 山</color>",
            BottomLeftText = "<color=green>林 林 林 林\n 林</color> 我 <color=green>林\n 林 林 林</color>",
            TopRightText = "<color=black>黑衣人</color>",
            TopRightQuest = QuestId.BlackMan,
            BottomRightText = "家",
            BottomRightQuest = QuestId.MeetYuHuan
        };

        this.Add(map.Location, map);
    }
}
