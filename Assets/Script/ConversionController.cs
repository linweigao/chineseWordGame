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
        List<string> dialog = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            dialog.Add("Hello world " + i.ToString());
        }

        dialog.Add("Hello world ! Hello world ! Hello world ! Hello world ! Hello world ! Hello world !");

        this.LoadDialog(dialog);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadDialog(List<string> dialog)
    {
        var rectTransform = content.GetComponent<RectTransform>();

        foreach (var message in dialog)
        {
            var messageObj = Instantiate(messagePrefab, content.transform);
            var text = messageObj.GetComponentInChildren<TMP_Text>();
            text.text = message;
            messageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
        }

        //var layoutGroup = content.GetComponent<LayoutGroup>();
        //LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
    }
}
