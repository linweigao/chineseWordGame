using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ConversionController : MonoBehaviour
{
    public GameObject content;
    public GameObject messagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        DialogMessage dialog = new DialogMessage()
        {
            Id = "Tree1",
            Msg = new Message
            {
                 Content = "你醒了，脑后好疼。为什么那么黑呢？应该是夜里，怎么没有月光呢？",
                 Type = MessageType.SystemMessage
            }
        };

        LoadDialog(dialog);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadDialog(DialogMessage dialog)
    {
        var messageGo = Instantiate(messagePrefab, content.transform);
        var textGo = messageGo.GetComponentInChildren<TMP_Text>();
        textGo.text = "";
        StartCoroutine(TypewriterEffect(textGo, dialog.Msg.Content));
    }

    private void LoadHistory(List<string> dialog)
    {
        foreach (var message in dialog)
        {
            var messageObj = Instantiate(messagePrefab, content.transform);
            var text = messageObj.GetComponentInChildren<TMP_Text>();
            text.text = message;
            messageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
        }
    }

    private IEnumerator TypewriterEffect(TMP_Text textObj, string text)
    {
        foreach (var character in text.ToCharArray())
        {
            textObj.text += character;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
