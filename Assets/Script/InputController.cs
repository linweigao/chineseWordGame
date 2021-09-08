using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputController : MonoBehaviour
{
    public TMP_InputField input;

    private GameController gameController;

    void Awake()
    {
        input.onSubmit.AddListener(InputSubmitted);
        this.gameController = this.GetComponent<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InputSubmitted(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            return;
        }

        input.text = string.Empty;
        this.gameController.HandleInput(data.Trim());
    }
}
