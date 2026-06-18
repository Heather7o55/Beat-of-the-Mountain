using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/* 
    This is a generic, apply to the ui button, create a duplicate of the object you want to be highlighted, squash and stretch it, 
    apply the highlight material, attach to this script, then disable it. 
*/
public class HighlightUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource audioSource;
    public string audioClip;
    private AudioClip i;
    [SerializeField] private GameObject highlight;
    void Awake()
    {
        if(audioClip != "") i = Resources.Load<AudioClip>(audioClip);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.SetActive(true);
        if(audioClip != null)
        {
            audioSource.clip = i;
            audioSource.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.SetActive(false);
        if(audioClip != null) audioSource.Stop();
    }
}
