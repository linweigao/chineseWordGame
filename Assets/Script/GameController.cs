using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Font font;

    private ConversionController conversionController;
    private PlayerState player;
    private QuestDict questDict;
    private MapDict mapDict;
    private Quest currentQuest;

    void Awake()
    {
        this.conversionController = this.GetComponent<ConversionController>();
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
        var quest = questDict[QuestDict.DefaultQuest];
        if (!string.IsNullOrEmpty(player.CurrentQuestId))
        {
            quest = questDict[player.CurrentQuestId];
        }

        Canvas.ForceUpdateCanvases();
        this.PlayQuest(Location.Tree, 4);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayQuest(Location location, int id)
    {
        Quest quest = this.questDict.Get(location, id);
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
            if (currentQuest.Key == "Tree2")
            {
                this.player.Name = input;
            }

            StartCoroutine(this.Reponse(input));
        }
    }

    private IEnumerator PlayQuestAsync(Quest quest)
    {
        this.currentQuest = quest;
        this.player.CurrentQuestId = quest.Key;

        yield return this.conversionController.TypeMessage(currentQuest.Msg);
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
        if (questDict.CheckQuest(this.currentQuest, input))
        {
            yield return this.conversionController.TypeMessage(new Message { Content = input, Type = MessageType.SelfMessage }, 0.5f);

            if (!string.IsNullOrEmpty(this.currentQuest.NextMessage))
            {
                yield return this.conversionController.TypeMessage(new Message { Content = this.currentQuest.NextMessage });
            }

            var location = this.currentQuest.NextQuestLocation == Location.None ? this.currentQuest.Location : this.currentQuest.NextQuestLocation;
            var nextQuest = questDict[location.ToString() + this.currentQuest.NextQuestId.ToString()];
            yield return PlayQuestAsync(nextQuest);

        }
        else
        {
            yield return this.conversionController.TypeMessage(new Message { Content = input, Type = MessageType.SelfMessage });
            yield return this.conversionController.TypeMessage(new Message { Content = QuestDict.DefaultWrongAnswer, Type = MessageType.SystemMessage });
        }
    }
}
