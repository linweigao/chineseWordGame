using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MessageController : MonoBehaviour
{
    public Image imageGo;
    public Image containerGo;
    public TMP_Text textGo;

    // color
    public Color systemBgColor = Color.black;
    public Color systemTextColor = Color.white;
    public Color selfBgColor = Color.green;
    public Color selfTextColor = Color.black;
    public Color userBgColor = Color.white;
    public Color userTextColor = Color.black;

    // margin
    public float marginLeft = 40f;
    public float marginRight = 40f;
    public float marginBottom = 10f;

    public float marginImage = 40f;

    public float lineHeight = 90f;

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
        var actualWidth = width - marginLeft - marginRight;

        var containerRect = this.gameObject.GetComponent<RectTransform>();
        var imageRect = imageGo.GetComponent<RectTransform>();
        var textContainerRect = containerGo.GetComponent<RectTransform>();
        var textRect = textGo.GetComponent<RectTransform>();

        var textExpectWidth = actualWidth;

        if (message.Type == MessageType.SystemMessage)
        {
            textGo.color = systemTextColor;
            containerGo.color = systemBgColor;
            imageGo.gameObject.SetActive(false);
        }
        else if (message.Type == MessageType.SelfMessage)
        {
            textGo.color = selfTextColor;
            containerGo.color = selfBgColor;
            imageRect.localPosition = new Vector3(width - marginRight - imageRect.sizeDelta.x, imageRect.localPosition.y);
            textExpectWidth = actualWidth - imageRect.sizeDelta.x - marginImage;
        }
        else if (message.Type == MessageType.UserMessage)
        {
            textGo.color = userTextColor;
            containerGo.color = userBgColor;
            textExpectWidth = actualWidth - imageRect.sizeDelta.x - marginImage;
        }

        var textMarginLeft = textRect.offsetMin.x;
        var textMarginRight = -textRect.offsetMax.x;
        textExpectWidth -= textMarginLeft + textMarginRight;

        var textSize = PopulateString(message.Content, textExpectWidth, lineHeight);

        textContainerRect.sizeDelta = new Vector2(textSize.Width + textMarginLeft + textMarginRight, (textSize.LineCount + 1) * lineHeight);
        if (message.Type == MessageType.SystemMessage)
        {
            // Magic number -50
            textContainerRect.localPosition = new Vector3(marginLeft + textContainerRect.sizeDelta.x / 2f - 50, textContainerRect.localPosition.y);
        }
        else if (message.Type == MessageType.SelfMessage)
        {
            textContainerRect.localPosition = new Vector3(imageRect.localPosition.x - marginImage - textContainerRect.sizeDelta.x / 2f - 50, imageRect.localPosition.y);
        }

        containerRect.sizeDelta = new Vector2(width, Math.Max(textContainerRect.sizeDelta.y, imageRect.sizeDelta.y) + marginBottom);
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
