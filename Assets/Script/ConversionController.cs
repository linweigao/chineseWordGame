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

    private bool isTyping;
    private PlayerState player;
    private ScrollRect scrollRect;
    private RectTransform contentRect;

    void Awake()
    {
        player = PlayerState.Instance;

        this.scrollRect = this.scrollView.GetComponent<ScrollRect>();
        this.contentRect = this.content.GetComponent<RectTransform>();
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

        var messageGo = CreateMessage(message);
        messageGo.textGo.text = message.Content;
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
        var messageController = CreateMessage(message);
        var textGo = messageController.textGo;
        textGo.text = "";
        yield return TypewriterEffect(textGo, msg, typeSpeed);

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

    private IEnumerator TypewriterEffect(TMP_Text textGo, string text, float typeSpeed = 0.05f)
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

    private MessageController CreateMessage(Message message)
    {
        var childCount = content.transform.childCount;
        var messageGo = Instantiate(messagePrefab, content.transform);
        var messageController = messageGo.GetComponent<MessageController>();
        messageController.Layout(message, contentRect.rect.width);
        messageGo.transform.SetSiblingIndex(childCount - 1);

        return messageController;
    }
}
