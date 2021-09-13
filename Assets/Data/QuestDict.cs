using System.Collections.Generic;

public class QuestDict : Dictionary<QuestId, Quest>
{
    private static QuestDict instance = new QuestDict();

    public static QuestDict Instance
    {
        get { return instance; }
    }

    public const string DefaultQuest = "Tree1";
    public const string DefaultNoHint = "这里帮不了你，童鞋靠你自己了!";
    public const string DefaultWrongAnswer = "好像你的话没起什么作用!";
    public const string DefaultPreQuestNotMeet = "是不是还有什么线索没有发现？";

    private QuestDict()
    {
        Quest quest = new Quest()
        {
            Location = Location.Tree,
            Id = QuestId.Start,
            Msg = new Message
            {
                Content = "你醒了，脑后好疼。为什么那么黑呢？应该是夜里，怎么没有<color=yellow>月光</color>呢？\n得先找到<color=yellow>月亮</color>吧！\n你不知道怎么做。那打个？求助一下。",
                Type = MessageType.SystemMessage
            },
            Hints = new List<Hint>
            {
                new Hint { Type = HintType.Message, Message = "文字！优美文字就是这个世界的力量！\n月光的文字。那就试试看“<color=yellow>床前明月光</color>”吧！" },
                new Hint { Type = HintType.Response, Message = "床前明月光" }
            },
            Answers = new List<string> { "月" },
            NextMessage = "月亮照亮了世界，你可以看清周围的一切。",
            NextQuestId = QuestId.Name
        };

        this.Add(quest);

        quest = new Quest()
        {
            Location = Location.Tree,
            Id = QuestId.Name,
            Msg = new Message { Content = "终于你想起了这个世界的力量。也想起来你的名字：" },
            NextQuestId = QuestId.Map
        };

        this.Add(quest);

        quest = new Quest()
        {
            Location = Location.Tree,
            Id = QuestId.Map,
            Msg = new Message { Content = "文友[##name##]，看来终于清醒了。下面看下周围的环境吧。打个#看看吧。" },
        };

        this.Add(quest);

        quest = new Quest
        {
            Location = Location.Tree,
            Id = QuestId.BlackMan,
            Msg = new Message { Content = "那里有一群黑衣人，可能是你就是被他们打的。但是太远了，看不清。" },
            Hints = new List<Hint>
            {
                new Hint{ Message="如果有<color=yellow>千里眼</color>的能力就好了。"}
            },
            NextMessage = "能看见了!黑衣人身上有<color=green>林府</color>的腰牌。怎么是他们？",
            NextQuestId = QuestId.GoHome
        };

        this.Add(quest);

        quest = new Quest
        {
            Location = Location.Tree,
            Id = QuestId.GoHome,
            Msg = new Message { Content = "天色很晚了，还是快点回家吧！玉环姐可能要等急了。" },
            NextQuestId = QuestId.MeetYuHuan
        };

        this.Add(quest);

        quest = new Quest
        {
            Location = Location.Home,
            Id = QuestId.MeetYuHuan,
            Msg = new Message { Content = "到家了！有个小姑娘正在门口张望。看到她，一下子竟然无法形容。" },
            Hints = new List<Hint>
            {
                new Hint {Message = "无暇的脸庞，朴素的麻衣，真的无法修饰。"}
            },
            PreQuestIds =
            {
                QuestId.BlackMan
            },
            NextQuestId = QuestId.TalkYuHuan
        };

        this.Add(quest);

        quest = new Quest
        {
            Location = Location.Home,
            Id = QuestId.TalkYuHuan,
            Msg = new Message { Content = "##name##，你怎么才回来啊？啊！受伤了，快让我看看。疼不疼啊？", From= NPC.YuHuan, Type= MessageType.NPCMessage }
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

