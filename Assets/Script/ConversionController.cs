using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ConversionController : MonoBehaviour
{
    public GameObject scrollView;
    public GameObject content;
    public GameObject messagePrefab;
    public GameObject selfMessagePrefab;

    private bool isTyping;
    private PlayerState player;
    private ScrollRect scrollRect;

    void Awake()
    {
        player = PlayerState.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.scrollRect = this.scrollView.GetComponent<ScrollRect>();
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

        var messageGo = CreateMessage(message);
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
        var messageGo = CreateMessage(message);
        var textGo = messageGo.GetComponentInChildren<TMP_Text>();
        textGo.text = "";
        yield return TypewriterEffect(textGo, messageGo, msg, typeSpeed);

        this.ScrollToBottom();

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

    private void ScrollToBottom()
    {
        this.scrollRect.normalizedPosition = new Vector2(0, 0);
    }

    private GameObject CreateMessage(Message message)
    {
        if (message.Type == MessageType.SelfMessage)
        {
            return Instantiate(selfMessagePrefab, content.transform);
        }
        else if (message.Type == MessageType.SystemMessage)
        {
            return Instantiate(messagePrefab, content.transform);
        }

        return null;
    }
}
