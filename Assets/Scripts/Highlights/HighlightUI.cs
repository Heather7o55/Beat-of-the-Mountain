using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject highlight;
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.SetActive(false);
    }
}
