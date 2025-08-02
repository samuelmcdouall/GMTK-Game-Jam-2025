using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GJ25MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioSource _musicAS;
    [SerializeField]
    List<AudioClip> _musicTracks;
    [SerializeField]
    Slider _musicSlider;
    [SerializeField]
    float _musicVolume;
    void Start()
    {
        _musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
        _musicSlider.value = _musicVolume;
        _musicAS.volume = _musicVolume;

        _musicAS.clip = _musicTracks[Random.Range(0, _musicTracks.Count - 1)];
        _musicAS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeMusicValue()
    {
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        PlayerPrefs.Save();
        _musicVolume = _musicSlider.value;
        _musicAS.volume = _musicVolume;
    }
}
