using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class TextEventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Inspector

    public Color NormalColor = Color.black;
    public Color HoverColor = Color.black;
    public Color PressColor = Color.black;
    public Color DisabledColor = Color.gray;

    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;

    #endregion Inspector

    private bool _isInteractive = true;
    public bool interactive
    {
        get
        {
            return _isInteractive;
        }
        set
        {
            _isInteractive = value;
            UpdateColor();
        }
    }

    private bool isPressed;
    private bool isHover;

    private TMP_Text textComponent;
    private TMP_Text TextComponent
    {
        get
        {
            if (!textComponent)
            {
                textComponent = GetComponent<TMP_Text>();
            }

            return textComponent;
        }
    }

    private void UpdateColor()
    {
        //if (!interactive)
        //{
        //    TextComponent.color = DisabledColor;
        //    return;
        //}

        //if (isPressed)
        //{
        //    TextComponent.color = PressColor;
        //    return;
        //}

        //if (isHover)
        //{
        //    TextComponent.color = HoverColor;
        //    return;
        //}

        //TextComponent.color = NormalColor;
    }

    #region IPointer Callbacks

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.onClick.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isHover) return;
        isPressed = true;
        UpdateColor();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isHover) return;
        isPressed = false;
        UpdateColor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;
        UpdateColor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
        isPressed = false;
        UpdateColor();
    }

    #endregion IPointer Callbacks
}