using UnityEngine;
public class SignRenderer : MonoBehaviour
{
    public float spawn;
    public float destroy;
    private double t;
    private Transform cameraTransform;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        // this increments the position of the lerp percentage to be the difference between when this sign was actually spawned,
        // and when it was supposed to be spawned, keeping everything in time with the beat
        t += .5d * RhythmManager.TimeSinceLastBeat;
    }

    // Update is called once per frame
    void Update()
    {
        // Makes sure the sign is always looking at the camera (old school psx/doom style billboard effect)
        RenderBillboard();
        // Using the custom song delta time lerps the sign position to be in line with the left side pillar in time with the beat
        LerpSignPosition();
    }
    private void RenderBillboard()
    {
        transform.LookAt(cameraTransform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
    private void LerpSignPosition()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
        gameObject.transform.position.y,
        Mathf.Lerp(spawn, destroy, (float) (t += .5d * RhythmManager.SongDeltaTime)));
        if(t > 1f) Destroy(gameObject);
    }
}
