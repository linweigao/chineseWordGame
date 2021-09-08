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
        
        this.conversionController.AddMessage(currentQuest.Msg, false);

        // TODO: Guide
        this.Hint();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hint()
    {
        var message = new Message
        {
            Content = "?",
            Type = MessageType.SelfMessage
        };

        this.conversionController.AddMessage(message);

        if (currentQuest.Hints != null && currentQuest.Hints.Count > 0)
        {
            foreach (var hint in currentQuest.Hints)
            {
                if (hint.Type == HintType.Message)
                {
                    this.conversionController.AddMessage(new Message { Content = hint.Message, Type = MessageType.SystemMessage });
                }
                else if (hint.Type == HintType.Response)
                {
                    this.Reponse(hint.Message);
                }
            }
        }
        else
        {
            this.conversionController.AddMessage(new Message { Content = "这里帮不了你，童鞋靠你自己了!", Type = MessageType.SystemMessage });
        }
    }

    public void Reponse(string input)
    {
        var message = new Message
        {
            Content = input,
            Type = MessageType.SelfMessage
        };

        this.conversionController.AddMessage(message);
    }
}
