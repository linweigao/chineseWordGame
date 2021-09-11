using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MessageController : MonoBehaviour
{
    public Image imageGo;
    public TMP_Text textGo;

    public float lineHeight = 90f;
    public float marginImage = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Layout(Message message, float width)
    {
        var containerRect = this.gameObject.GetComponent<RectTransform>();
        var imageRect = imageGo.GetComponent<RectTransform>();
        var textRect = textGo.GetComponent<RectTransform>();

        var textExpectWidth = width;

        if (message.Type == MessageType.SystemMessage)
        {
            imageGo.gameObject.SetActive(false);
        }
        else if (message.Type == MessageType.SelfMessage)
        {
            imageRect.localPosition = new Vector3(width - imageRect.sizeDelta.x, imageRect.localPosition.y);
            textExpectWidth = width - imageRect.sizeDelta.x - marginImage;
        }
        else if (message.Type == MessageType.UserMessage)
        {
            textExpectWidth = width - imageRect.sizeDelta.x - marginImage;
        }

        var textSize = PopulateString(message.Content, textExpectWidth, lineHeight);

        textRect.sizeDelta = new Vector2(textSize.Width, (textSize.LineCount + 1) * lineHeight);
        if (message.Type == MessageType.SystemMessage)
        {
            // Magic number -50
            textRect.localPosition = new Vector3(textRect.sizeDelta.x / 2f - 50, textRect.localPosition.y);
        }
        else if (message.Type == MessageType.SelfMessage)
        {
            textRect.localPosition = new Vector3(imageRect.localPosition.x - marginImage - textRect.sizeDelta.x / 2f - 50, imageRect.localPosition.y);
        }

        containerRect.sizeDelta = new Vector2(width, Math.Max(textRect.sizeDelta.y, imageRect.sizeDelta.y)); ;
    }

    private TextSize PopulateString(string text, float width, float height)
    {
        textGo.text = text;
        var textWidth = textGo.preferredWidth;
        var lineCount = 0;
        if (textWidth > width)
        {
            lineCount = (int)(textWidth / width);
            textWidth = width;
        }

        var retVal = new TextSize { LineCount = lineCount, Width = textWidth, Height = textGo.preferredHeight };
        Debug.Log(retVal);

        return retVal;
    }

    public class TextSize
    {
        public int LineCount { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public override string ToString()
        {
            return string.Format("Line: {0}, Width: {1}", LineCount, Width);
        }
    }
}

public static class RectTransformExtensions
{
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
}
