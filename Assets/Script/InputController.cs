using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public TMP_InputField input;
    public Button helpBtn;
    public Button mapBtn;

    private GameController gameController;

    void Awake()
    {
        this.input.onSubmit.AddListener(InputSubmitted);
        this.helpBtn.onClick.AddListener(HelpClicked);
        this.mapBtn.onClick.AddListener(MapClicked);
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

    private void HelpClicked()
    {
        this.gameController.HandleInput("?");
    }

    private void MapClicked()
    {
        this.gameController.HandleInput("#");
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
