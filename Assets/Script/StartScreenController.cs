using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    public GameObject start;
    public GameObject load;

    private SelectionController startSelection;
    private SelectionController loadSelection;

    private bool startNew = true;

    // Use this for initialization
    void Start()
    {
        startSelection = this.start.GetComponent<SelectionController>();
        loadSelection = this.load.GetComponent<SelectionController>();

        this.SetSelection(startNew);

        startSelection.onHover.AddListener(() => this.SetSelection(true));
        loadSelection.onHover.AddListener(() => this.SetSelection(false));

        startSelection.onClick.AddListener(() => this.Start(true));
        loadSelection.onClick.AddListener(() => this.Start(false));

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.SetSelection(true);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.SetSelection(false);
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter))
        {
            this.Start(this.startNew);
        }
    }

    private void SetSelection(bool start)
    {
        this.startNew = start;
        startSelection.SetSelection(start);
        loadSelection.SetSelection(!start);
    }

    private void Start(bool startNew)
    {
        SetSelection(startNew);
        if (startNew)
        {
            PlayerState.Instance = new PlayerState();
        }
        else
        {
            PlayerState.Instance = PlayerState.Load();
        }

        StartCoroutine(this.LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
