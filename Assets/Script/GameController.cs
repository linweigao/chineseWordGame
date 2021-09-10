using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Font font;

    private ConversionController conversionController;
    private PlayerState player;
    private QuestDict questDict;
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

        // TODO: Load Player state;
        player = PlayerState.Instance;

        // TODO: Load history

        // Load last/current quest
        var quest = questDict[QuestDict.DefaultQuest];
        if (!string.IsNullOrEmpty(player.CurrentQuestId))
        {
            quest = questDict[player.CurrentQuestId];
        }
        this.currentQuest = quest;

        Canvas.ForceUpdateCanvases();
        StartCoroutine(PlayQuest(quest));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleInput(string input)
    {
        if (input == "?" || input == "？")
        {
            StartCoroutine(this.Hint());
        }
        else
        {
            if (currentQuest.Id == "Tree2")
            {
                this.player.Name = input;
            }

            StartCoroutine(this.Reponse(input));
        }
    }

    private IEnumerator PlayQuest(Quest quest)
    {
        this.currentQuest = quest;
        this.player.CurrentQuestId = quest.Id;

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
            yield return this.conversionController.TypeMessage(new Message { Content = "这里帮不了你，童鞋靠你自己了!", Type = MessageType.SystemMessage });
        }
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

            var nextQuest = questDict[this.currentQuest.NextQuest];
            yield return PlayQuest(nextQuest);

        }
        else
        {
            yield return this.conversionController.TypeMessage(new Message { Content = input, Type = MessageType.SelfMessage });
            yield return this.conversionController.TypeMessage(new Message { Content = "好像你的话没起什么作用!", Type = MessageType.SystemMessage });
        }
    }
}
