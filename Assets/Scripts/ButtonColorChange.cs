using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public Color normalColor = Color.white;
    public Color hoverColor = new(191f/255f, 223f/255f, 237f/255f); // Hex: #bfdfed

    private void Start()
    {
        // Set the initial color
        SetButtonTextColor(normalColor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the text color on hover
        SetButtonTextColor(hoverColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restore the original text color when not hovered
        SetButtonTextColor(normalColor);
    }

    private void SetButtonTextColor(Color color)
    {
        // Set the text color of the button using TextMeshPro's color property
        buttonText.color = color;
    }
}
