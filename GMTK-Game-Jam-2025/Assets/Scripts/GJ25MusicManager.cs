using System.Collections.Generic;
using UnityEngine;

public class GJ25MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioSource _musicAS;
    [SerializeField]
    List<AudioClip> _musicTracks;
    void Start()
    {
        _musicAS.clip = _musicTracks[Random.Range(0, _musicTracks.Count - 1)];
        _musicAS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
