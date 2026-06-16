using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject prefab;
    public Transform position;
    void Start()
    {
        Instantiate(prefab);
    }
    private void OnTriggerExit(Collider other)
    {
        Instantiate(prefab);
    }
}
