using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SelectionController : MonoBehaviour
{
    public Image image;
    public TMP_Text text;

    public UnityEvent onHover;
    public UnityEvent onClick;

    void Start()
    {
        var textEvent = text.GetComponent<TextEventHandler>();
        textEvent.onHover.AddListener(textHover);
        textEvent.onClick.AddListener(textClick);
    }

    public void SetSelection(bool selection)
    {
        image.gameObject.SetActive(selection);
    }

    private void textHover()
    {
        this.SetSelection(true);
        this.onHover.Invoke();
    }

    private void textClick()
    {
        this.SetSelection(true);
        this.onClick.Invoke();
    }
}
