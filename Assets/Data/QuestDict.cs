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
                Content = "你醒了，脑后好疼。为什么那么黑呢？应该是夜里，怎么没有<color=yellow>月光</color>呢？\n是不是忘记了什么？是不是不知道怎么办？打个？看看吧。",
                Type = MessageType.SystemMessage
            },
            Hints = new List<Hint>
            {
                new Hint { Type = HintType.Message, Message = "你好像记起来诗词是这个世界的核心力量。所以你大声的说:"},
                new Hint { Type = HintType.Response, Message ="床前明月光" }
            },
            Answers= new List<string> { "月"},
            NextMessage = "月亮照亮了你的世界。",
            NextQuest = "Tree2"
        };

        this.Add(quest);

        quest = new Quest()
        {
            Id = "Tree2",
            Msg = new Message { Content = "终于你想起了这个世界的力量。也想起来你的名字：" },
            NextQuest ="Tree3"
        };

        this.Add(quest);

        quest = new Quest()
        {
            Id = "Tree3",
            Msg = new Message { Content = "文友[##name##],看来终于清醒了。下面看下周围的环境吧。打个~看看吧。" },
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

