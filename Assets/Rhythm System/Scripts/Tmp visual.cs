using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class Tmpvisual : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void destory(float time)
    {
        Destroy(gameObject, time);
    }
}
