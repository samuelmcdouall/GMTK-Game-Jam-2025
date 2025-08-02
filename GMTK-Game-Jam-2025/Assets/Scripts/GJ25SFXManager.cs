using UnityEngine;

public class GJ25SFXManager : MonoBehaviour
{
    [SerializeField]
    AudioSource _sfxAS;

    [SerializeField]
    float _sfxVolume;

    public AudioClip BrewKickOff;
    public AudioClip BrewReady;
    public AudioClip PickUpDrink;
    public AudioClip TrashDrink;
    public AudioClip PortalTraverse;
    public AudioClip GiveDrink;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFXClip(AudioClip clip)
    {
        _sfxAS.PlayOneShot(clip, _sfxVolume);
    }
}
