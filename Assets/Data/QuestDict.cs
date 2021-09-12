using System.Collections.Generic;

public class QuestDict : Dictionary<string, Quest>
{
    private static QuestDict instance = new QuestDict();

    public static QuestDict Instance
    {
        get { return instance; }
    }

    public const string DefaultQuest = "Tree1";
    public const string DefaultNoHint = "这里帮不了你，童鞋靠你自己了!";
    public const string DefaultWrongAnswer = "好像你的话没起什么作用!";

    private QuestDict()
    {
        Quest quest = new Quest()
        {
            Location = Location.Tree,
            Id = 1,
            Msg = new Message
            {
                Content = "你醒了，脑后好疼。为什么那么黑呢？应该是夜里，怎么没有<color=yellow>月光</color>呢？\n得先找到<color=yellow>月亮</color>吧！\n你不知道怎么做。那打个？求助一下。",
                Type = MessageType.SystemMessage
            },
            Hints = new List<Hint>
            {
                new Hint { Type = HintType.Message, Message = "文字！优美文字就是这个世界的力量！\n月光的文字。让文字的声音让这个世界听见：" },
                new Hint { Type = HintType.Response, Message = "床前明月光" }
            },
            Answers = new List<string> { "月" },
            NextMessage = "月亮照亮了世界，你可以看清周围的一切。",
            NextQuestId = 2
        };

        this.Add(quest);

        quest = new Quest()
        {
            Location = Location.Tree,
            Id = 2,
            Msg = new Message { Content = "终于你想起了这个世界的力量。也想起来你的名字：" },
            NextQuestId = 3
        };

        this.Add(quest);

        quest = new Quest()
        {
            Location = Location.Tree,
            Id = 3,
            Msg = new Message { Content = "文友[##name##]，看来终于清醒了。下面看下周围的环境吧。打个#看看吧。" },
        };

        this.Add(quest);

        quest = new Quest
        {
            Location = Location.Tree,
            Id = 4,
            Msg = new Message { Content = "那里有一群黑衣人，可能是你就是被他们打的。但是太远了，你看不清。" },
            Hints = new List<Hint>
            {
                new Hint{Type=HintType.Message, Message="看不清楚需要加强你的<color=yellow>眼睛</color>的能力。"}
            }
        };

        this.Add(quest);
    }

    public void Add(Quest quest)
    {
        this.Add(quest.Key, quest);
    }

    public Quest Get(Location location, int id)
    {
        return this[location.ToString() + id.ToString()];
    }

    public bool CheckQuest(Quest quest, string answer)
    {
        return true;
    }
}

