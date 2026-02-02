using UnityEngine;
using Ink.Runtime;
public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instanceSelf;
    public TextManager textManager;
    void Awake()
    {
        if(instanceSelf != null || textManager == null) return;
        
    }
    void Update()
    {
        
    }
}
