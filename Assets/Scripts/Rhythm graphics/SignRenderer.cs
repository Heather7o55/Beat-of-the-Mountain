using UnityEngine;
public class SignRenderer : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public GameObject spawn;
    public GameObject destroy;
    private double lastSongPosition;
    private double songDeltaTime;
    private double t;
    private Transform cameraTransform;
    void Start()
    {
        // This just ensures that we dont accidentally calculate a song delta time value as being the length the song has been playing, 
        // causing signs to instantly snap to their final position
        lastSongPosition = rhythmManager.songPosition;
        cameraTransform = Camera.main.transform;
        // this increments the position of the lerp percentage to be the difference between when this sign was actually spawned,
        // and when it was supposed to be spawned, keeping everything in time with the beat
        t += 1d * (rhythmManager.songPosition - (rhythmManager.activeSong.beatLengthInSeconds * rhythmManager.currentBeat));
    }

    // Update is called once per frame
    void Update()
    {
        // Race condition check, just in case the get component assignment in the spawner gets delayed. 
        // also checks the song is active, just in case the song is paused during this signs lifetime
        if(spawn == null || destroy == null || rhythmManager == null || !rhythmManager.activeSong.active) return;
        // Makes sure the sign is always looking at the camera (old school psx/doom style billboard effect)
        RenderBillboard();
        CalculateSongDeltaTime();
        // Using the custom song delta time lerps the sign position to be in line with the left side pillar in time with the beat
        LerpSignPosition();
    }
    private void RenderBillboard()
    {
        transform.LookAt(cameraTransform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
    private void CalculateSongDeltaTime()
    {
        // Calculates a delta time (time elapsed between frames) but using the song position so its in time with the beat, 
        // as unity delta time causes things to drift out of sync with the beat
        songDeltaTime = (float) (rhythmManager.songPosition - lastSongPosition);
        // also increments the t value for the lerp using that delta time value
        t += .5d * songDeltaTime;
        lastSongPosition = rhythmManager.songPosition;
    }
    private void LerpSignPosition()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,Mathf.Lerp(spawn.transform.position.z,destroy.transform.position.z, (float) t));
        if(t > 1f) Destroy(gameObject);
    }
}
