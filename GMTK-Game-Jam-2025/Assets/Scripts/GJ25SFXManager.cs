using UnityEngine;
using UnityEngine.UI;

public class GJ25SFXManager : MonoBehaviour
{
    [SerializeField]
    AudioSource _sfxAS;

    [SerializeField]
    float _sfxVolume;

    [SerializeField]
    Slider _sfxSlider;

    public AudioClip BrewKickOff;
    public AudioClip BrewReady;
    public AudioClip PickUpDrink;
    public AudioClip TrashDrink;
    public AudioClip PortalTraverse;
    public AudioClip GiveDrink;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        _sfxSlider.value = _sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFXClip(AudioClip clip)
    {
        _sfxAS.PlayOneShot(clip, _sfxVolume);
    }

    public void OnChangeSFXSlider()
    {
        PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);
        PlayerPrefs.Save();
        _sfxVolume = _sfxSlider.value;
    }
}
