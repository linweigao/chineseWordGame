using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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

        currentQuest = questDict[QuestDict.DefaultQuest];
        if (!string.IsNullOrEmpty(player.CurrentQuestId))
        {
            currentQuest = questDict[player.CurrentQuestId];
        }

        // TODO: Load history

        StartCoroutine(this.conversionController.TypeMessage(currentQuest.Msg));

        // TODO: Guide
        StartCoroutine(this.Hint());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleInput(string input)
    {
        if (input == "?")
        {
            StartCoroutine(this.Hint());
        }
        else
        {
            StartCoroutine(this.Reponse(input));
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
            yield return this.conversionController.TypeMessage(new Message { Content = "这里帮不了你，童鞋靠你自己了!", Type = MessageType.SystemMessage });
        }
    }

    private IEnumerator Reponse(string input)
    {
        if (questDict.CheckQuest(this.currentQuest, input))
        {
            return this.conversionController.TypeMessage(new Message { Content = input, Type = MessageType.SelfMessage }, 0.5f);
        }

        return this.conversionController.TypeMessage(new Message { Content = input, Type = MessageType.SelfMessage });
    }
}
