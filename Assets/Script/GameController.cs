using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Font font;

    private ConversionController conversionController;
    private TransitionController transitionController;
    private PlayerState player;
    private QuestDict questDict;
    private MapDict mapDict;
    private Quest currentQuest;

    void Awake()
    {
        this.conversionController = this.GetComponent<ConversionController>();
        this.transitionController = this.GetComponent<TransitionController>();
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Load quest;
        questDict = QuestDict.Instance;
        // TODO: Load map;
        mapDict = MapDict.Instance;

        // TODO: Load Player state;
        player = PlayerState.Instance;

        // TODO: Load history

        // Load last/current quest
        var quest = questDict[QuestId.Start];
        if (player.CurrentQuestId != QuestId.None)
        {
            quest = questDict[player.CurrentQuestId];
        }

        Canvas.ForceUpdateCanvases();


        this.PlayQuest(QuestId.Start);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayQuest(QuestId id)
    {
        Quest quest = this.questDict[id];
        StartCoroutine(PlayQuestAsync(quest));
    }

    public void PlayQuest(Quest quest)
    {
        StartCoroutine(PlayQuestAsync(quest));
    }

    public void HandleInput(string input)
    {
        if (input == "?" || input == "？")
        {
            StartCoroutine(this.Hint());
        }
        else if (input == "#")
        {
            StartCoroutine(this.Map());
        }
        else
        {
            if (currentQuest.Id == QuestId.Name)
            {
                this.player.Name = input;
            }

            StartCoroutine(this.Reponse(input));
        }
    }

    private IEnumerator PlayQuestAsync(Quest quest)
    {
        bool meet = quest.PreQuestIds.TrueForAll(q => player.PassedQuests.Contains(q));
        if (!meet)
        {
            string noMeet = QuestDict.DefaultPreQuestNotMeet;
            yield return this.conversionController.TypeMessage(new Message { Content = noMeet });
        }
        else
        {
            if (this.currentQuest == null || this.currentQuest.Location != quest.Location)
            {
                // transit location.
                yield return this.transitionController.TriggerStart();
            }

            this.currentQuest = quest;
            this.player.CurrentQuestId = quest.Id;

            yield return this.conversionController.TypeMessage(currentQuest.Msg);

            if (quest.AutoNext)
            {
                yield return new WaitForSeconds(0.5f);
                var nextQuest = this.questDict[quest.NextQuestId];
                yield return this.PlayQuestAsync(nextQuest);
            }
        }
    }

    private IEnumerator Hint()
    {
        yield return this.conversionController.TypeMessage(new Message { Content = "?", Type = MessageType.SelfMessage });

        if (currentQuest.Hints != null && currentQuest.Hints.Count > 0)
        {
            foreach (var hint in currentQuest.Hints)
            {
                if (hint.Type == HintType.Message)
                {
                    yield return this.conversionController.TypeMessage(new Message { Content = hint.Message, Type = MessageType.SystemMessage });
                }
                else if (hint.Type == HintType.Response)
                {
                    yield return this.Reponse(hint.Message);
                }
            }
        }
        else
        {
            yield return this.conversionController.TypeMessage(new Message { Content = QuestDict.DefaultNoHint, Type = MessageType.SystemMessage });
        }
    }

    private IEnumerator Map()
    {
        yield return this.conversionController.TypeMessage(new Message { Content = "#", Type = MessageType.SelfMessage });

        var map = mapDict[this.currentQuest.Location];
        yield return this.conversionController.AddMap(map);
    }

    private IEnumerator Reponse(string input)
    {
        if (this.CheckQuest(this.currentQuest, input))
        {
            yield return this.conversionController.TypeMessage(new Message { Content = input, Type = MessageType.SelfMessage }, 0.5f);

            if (!string.IsNullOrEmpty(this.currentQuest.NextMessage))
            {
                yield return this.conversionController.TypeMessage(new Message { Content = this.currentQuest.NextMessage });
            }

            player.PassedQuests.Add(this.currentQuest.Id);

            var nextQuest = questDict[this.currentQuest.NextQuestId];
            yield return PlayQuestAsync(nextQuest);

        }
        else
        {
            yield return this.conversionController.TypeMessage(new Message { Content = input, Type = MessageType.SelfMessage });
            yield return this.conversionController.TypeMessage(new Message { Content = QuestDict.DefaultWrongAnswer, Type = MessageType.SystemMessage });
        }
    }

    private bool CheckQuest(Quest quest, string input)
    {
        PoemMatch match = PoemList.Instance.Match(input);
        if (match == null)
        {
            return false;
        }

        return quest.Answers.TrueForAll(a => match.Sentences.Find(m => m.Tags.Contains(a)) != null);
    }
}
