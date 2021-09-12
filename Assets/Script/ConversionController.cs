using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class ConversionController : MonoBehaviour
{
    public GameObject scrollView;
    public GameObject content;
    public GameObject messagePrefab;
    public GameObject mapPrefab;

    private bool isWorking;
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
        if (this.isWorking)
        {
            yield return new WaitWhile(() => this.isWorking);
        }

        var messageGo = CreateMessage(message);
        messageGo.textGo.text = message.Content;
        this.ScrollToBottom();
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator AddMap(LocationMap map)
    {
        if (this.isWorking)
        {
            yield return new WaitWhile(() => this.isWorking);
        }

        this.isWorking = true;
        this.CreateMap(map);
        yield return new WaitForEndOfFrame();
        this.ScrollToBottom();

        this.isWorking = false;
    }

    public IEnumerator TypeMessage(Message message, float typeSpeed = 0.05f)
    {
        if (this.isWorking)
        {
            yield return new WaitWhile(() => this.isWorking);
        }

        isWorking = true;
        var replacedMsg = message.Content.Replace("##name##", this.player.Name);
        foreach (var msg in replacedMsg.Split('\n'))
        {
            var splitMsg = new Message { Content = msg, From = message.From, Type = message.Type };
            var messageController = CreateMessage(splitMsg);
            var textGo = messageController.textGo;
            textGo.text = "";
            yield return TypewriterEffect(textGo, msg, typeSpeed);

            this.ScrollToBottom();
        }


        yield return new WaitForSecondsRealtime(0.5f);
        isWorking = false;
    }

    private IEnumerator TypewriterEffect(TMP_Text textGo, string text, float typeSpeed = 0.05f)
    {
        bool styleStart = false;
        bool styleApply = false;
        string cachedStyle = "";
        string cachedStyleEnd = "";


        foreach (var character in text.ToCharArray())
        {
            if (character == '<')
            {
                styleStart = true;
            }
            else if (character == '>')
            {
                cachedStyle += character;
                styleStart = false;

                if (cachedStyle.Contains("/"))
                {
                    styleApply = false;
                    cachedStyle = "";
                    cachedStyleEnd = "";
                }
                else
                {
                    styleApply = true;
                    cachedStyleEnd = cachedStyle.Replace("<", "</");
                    if (cachedStyleEnd.Contains("="))
                    {
                        cachedStyleEnd = cachedStyleEnd.Substring(0, cachedStyleEnd.IndexOf("=")) + ">";
                    }
                }
                continue;
            }

            if (styleStart)
            {
                cachedStyle += character;
                continue;
            }

            if (styleApply)
            {
                textGo.text += cachedStyle + character + cachedStyleEnd;
            }
            else
            {
                textGo.text += character;
            }

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

    private MapController CreateMap(LocationMap map)
    {
        var childCount = content.transform.childCount;
        var mapGo = Instantiate(mapPrefab, content.transform);
        var mapController = mapGo.GetComponent<MapController>();
        mapGo.transform.SetSiblingIndex(childCount - 1);

        if (!string.IsNullOrEmpty(map.MiddleText))
        {
            mapController.middleText.text = map.MiddleText;
            
        }
        if (!string.IsNullOrEmpty(map.TopLeftText))
        {
            mapController.topLeftText.text = map.TopLeftText;

        }
        if (!string.IsNullOrEmpty(map.TopRightText))
        {
            mapController.topRightText.text = map.TopRightText;

        }
        if (!string.IsNullOrEmpty(map.BottomLeftText))
        {
            mapController.bottomLeftText.text = map.BottomLeftText;

        }
        if (!string.IsNullOrEmpty(map.BottomRightText))
        {
            mapController.bottomRightText.text = map.BottomRightText;

        }

        return mapController;
    }
}
