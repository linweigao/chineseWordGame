using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelfMessageController : MonoBehaviour
{
    public GameObject containerGo;
    public Image imageGo;
    public TMP_Text textGo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var containerRect = containerGo.GetComponent<RectTransform>();
        var imageRect = imageGo.GetComponent<RectTransform>();
        var textRect = textGo.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(containerRect.sizeDelta.x - imageRect.sizeDelta.x - 20, containerRect.sizeDelta.y);
    }
}
