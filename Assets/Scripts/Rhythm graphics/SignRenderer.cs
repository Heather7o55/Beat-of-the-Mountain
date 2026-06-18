using UnityEngine;

public class SignRenderer : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public double lastSongPosition;
    public double songDeltaTime;
    public double t;
    public GameObject spawn;
    public GameObject destroy;
    private Transform cameratransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSongPosition = rhythmManager.songPosition;
        cameratransform = Camera.main.transform;
        t += 1d * (rhythmManager.songPosition - (rhythmManager.activeSong.beatLengthInSeconds * rhythmManager.currentBeat));
    }

    // Update is called once per frame
    void Update()
    {
        if(spawn == null || destroy == null || rhythmManager == null || !rhythmManager.activeSong.active) return;
        transform.LookAt(cameratransform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        songDeltaTime = (float) (rhythmManager.songPosition - lastSongPosition);
        t += .5d * songDeltaTime;
        gameObject.transform.position= new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,Mathf.Lerp(spawn.transform.position.z,destroy.transform.position.z, (float) t));
        lastSongPosition = rhythmManager.songPosition;
        if(t > 1f)
        {
            t = 0f;
            Destroy(gameObject);
        }
    }
}
