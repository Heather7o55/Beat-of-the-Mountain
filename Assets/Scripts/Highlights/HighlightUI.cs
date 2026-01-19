using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/* 
    This is a generic, apply to the ui button, create a duplicate of the object you want to be highlighted, squash and stretch it, 
    apply the highlight material, attach to this script, then disable it. 
*/
public class HighlightUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject highlight;
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.SetActive(false);
    }
}
