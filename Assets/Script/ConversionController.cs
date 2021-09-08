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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMessage(Message message)
    {
        var messageGo = Instantiate(messagePrefab, content.transform);
        var textGo = messageGo.GetComponentInChildren<TMP_Text>();
        textGo.text = "";

        Debug.LogFormat("PreferredWidth & Height: {0}, {1}", textGo.preferredWidth, textGo.preferredHeight);

        messageGo.GetComponent<RectTransform>().sizeDelta = new Vector2(textGo.preferredWidth, textGo.preferredHeight);
        StartCoroutine(TypewriterEffect(textGo, messageGo, message.Content));
    }

    public void LoadHistory(List<Message> messages)
    {
        foreach (var message in messages)
        {
            var messageObj = Instantiate(messagePrefab, content.transform);
            var text = messageObj.GetComponentInChildren<TMP_Text>();
            text.text = message.Content;
            messageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
        }
    }

    private IEnumerator TypewriterEffect(TMP_Text textGo, GameObject parent, string text)
    {
        foreach (var character in text.ToCharArray())
        {
            textGo.text += character;
            yield return new WaitForSeconds(0.05f);
        }

        // TODO:
        Debug.Log(textGo.textBounds.size.ToString());
        Debug.LogFormat("PreferredWidth & Height: {0}, {1}", textGo.preferredWidth, textGo.preferredHeight);
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, textGo.textBounds.size.y);
    }
}
