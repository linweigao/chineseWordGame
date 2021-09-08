using System.Collections.Generic;

public class QuestDict: Dictionary<string, Quest>
{
    private static QuestDict instance = new QuestDict();

    public static QuestDict Instance
    {
        get { return instance; }
    }

    public const string DefaultQuest = "Tree1";

    private QuestDict()
    {
        Quest quest = new Quest()
        {
            Id = "Tree1",
            Msg = new Message
            {
                Content = "你醒了，脑后好疼。为什么那么黑呢？应该是夜里，怎么没有月光呢？",
                Type = MessageType.SystemMessage
            },
            Hints = new List<Hint>
            {
                new Hint { Type = HintType.Message, Message = "你好像记起来诗词是这个世界的核心力量。所以你大声的说:"},
                new Hint { Type = HintType.Response, Message ="床前明月光" }
            }
        };

        this.Add(quest);
    }

    public void Add(Quest quest)
    {
        this.Add(quest.Id, quest);
    }

    public bool CheckQuest(Quest quest, string answer)
    {
        return true;
    }
}

