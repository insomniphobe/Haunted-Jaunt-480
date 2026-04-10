using UnityEngine;

public class RadioMusicTrigger : MonoBehaviour
{

    public float maxVolume = 0.5f;
    public float maxHearingDistance = 3.5f;

    public GameObject player;
    public AudioSource ambienceAudioSource;
    private AudioSource radioAudioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        radioAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        if (distanceToPlayer <= maxHearingDistance || radioAudioSource.volume != 0)
        {
            // Lerp between 0 and the max radio/ambience volume using the percentage of
            // how far in-between we are from 0 and maxHearingDistance from the radio
            float clampedDistance = Mathf.Clamp(distanceToPlayer, 0, maxHearingDistance);
            float volumeLerp = Mathf.Lerp(0, maxVolume, clampedDistance / maxHearingDistance);

            // On top of the radio:     Ambience Volume: 0,     Radio Volume: Max
            // Outside radio range:     Ambience Volume: Max,   Radio Volume: 0
            ambienceAudioSource.volume = volumeLerp;
            radioAudioSource.volume = maxVolume - volumeLerp;
        }
    }
}
