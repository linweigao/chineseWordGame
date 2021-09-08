using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private ConversionController conversionController;

    void Awake()
    {
        this.conversionController = this.GetComponent<ConversionController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;


        Quest quest = new Quest()
        {
            Id = "Tree1",
            Msg = new Message
            {
                Content = "你醒了，脑后好疼。为什么那么黑呢？应该是夜里，怎么没有月光呢？",
                Type = MessageType.SystemMessage
            }
        };

        this.conversionController.AddMessage(quest.Msg);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleInput(string input)
    {
        var message = new Message
        {
            Content = input,
            Type = MessageType.SelfMessage
        };

        this.conversionController.AddMessage(message);
    }
}
