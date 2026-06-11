using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject tmp;
    private void OnTriggerExit(Collider other)
    {
        Instantiate(tmp, gameObject.transform);
    }
}
