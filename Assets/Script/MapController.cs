using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MapController : MonoBehaviour
{
    public Image containerGo;
    public TMP_Text topLeftText;
    public TMP_Text topRightText;
    public TMP_Text bottomLeftText;
    public TMP_Text bottomRightText;
    public TMP_Text middleText;

    public UnityEvent MiddleTextOnClick;
    public UnityEvent TopLeftTextOnClick;
    public UnityEvent TopRightTextOnClick;
    public UnityEvent BottomLeftTextOnClick;
    public UnityEvent BottomRightTextOnClick;

    // Start is called before the first frame update
    void Start()
    {
        var middleTextEvent = this.middleText.GetComponent<TextEventHandler>();
        middleTextEvent.onClick.AddListener(() => MiddleTextOnClick.Invoke());

        var topLeftTextEvent = this.topLeftText.GetComponent<TextEventHandler>();
        topLeftTextEvent.onClick.AddListener(() => TopLeftTextOnClick.Invoke());

        var toprightTextEvent = this.topRightText.GetComponent<TextEventHandler>();
        toprightTextEvent.onClick.AddListener(() => TopRightTextOnClick.Invoke());

        var bottomLeftTextEvent = this.bottomLeftText.GetComponent<TextEventHandler>();
        bottomLeftTextEvent.onClick.AddListener(() => BottomLeftTextOnClick.Invoke());

        var bottomRightTextEvent = this.bottomRightText.GetComponent<TextEventHandler>();
        bottomRightTextEvent.onClick.AddListener(() => BottomRightTextOnClick.Invoke());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
