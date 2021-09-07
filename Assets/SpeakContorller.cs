using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakContorller : MonoBehaviour
{
    public Text textObject;

    // Start is called before the first frame update
    void Start()
    {
        // textObject.text = "";
        // StartCoroutine(TypewriterEffect("欢迎光临！欢迎光临！欢迎光临！欢迎光临！欢迎光临！欢迎光临！欢迎光临！欢迎光临！欢迎光临！欢迎光临！欢迎光临！欢迎光临！欢迎光临！欢迎光临！"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TypewriterEffect(string text)
    {
        foreach (var character in text.ToCharArray())
        {
            if (character == '！')
            {
                textObject.text += "<color=green>" + character + "</color>";
            }
            else
            {
                textObject.text += character;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
