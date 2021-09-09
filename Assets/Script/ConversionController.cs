using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ConversionController : MonoBehaviour
{
    public GameObject content;
    public GameObject messagePrefab;

    private bool isTyping;
    private PlayerState player;

    void Awake()
    {
        player = PlayerState.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator AddMessage(Message message)
    {
        if (this.isTyping)
        {
            yield return new WaitWhile(() => this.isTyping);
        }

        var messageGo = Instantiate(messagePrefab, content.transform);
        var textGo = messageGo.GetComponentInChildren<TMP_Text>();
        textGo.text = message.Content;
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator TypeMessage(Message message, float typeSpeed = 0.05f)
    {
        string msg = message.Content.Replace("##name##", this.player.Name);
        Debug.Log("TypeMessage: " + msg);
        if (this.isTyping)
        {
            yield return new WaitWhile(() => this.isTyping);
        }

        isTyping = true;
        var messageGo = Instantiate(messagePrefab, content.transform);
        var textGo = messageGo.GetComponentInChildren<TMP_Text>();
        textGo.text = "";
        yield return TypewriterEffect(textGo, messageGo, msg, typeSpeed);

        yield return new WaitForSecondsRealtime(0.5f);
        isTyping = false;
    }

    public IEnumerator AddMessages(List<Message> messages)
    {
        foreach (var message in messages)
        {
            yield return AddMessage(message);
        }
    }

    private IEnumerator TypewriterEffect(TMP_Text textGo, GameObject parent, string text, float typeSpeed = 0.05f)
    {
        foreach (var character in text.ToCharArray())
        {
            textGo.text += character;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
