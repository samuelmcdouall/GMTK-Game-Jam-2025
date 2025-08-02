using UnityEngine;
using UnityEngine.VFX;

public class GJ25Portal : MonoBehaviour
{
    [SerializeField]
    GJ25Player _player;
    [SerializeField]
    Transform _portalToLocation;
    [SerializeField]
    GJ25Player.CurrentLevel _level;
    GJ25SFXManager _sfxManager;

    void Start()
    {
        _sfxManager = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<GJ25SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    print("Nearby portal");
        //    _player.SetNearbyPortal(this);
        //}
        if (other.gameObject.CompareTag("Player"))
        {
            _player.TeleportPlayer(_portalToLocation.position, _level);
            _sfxManager.PlaySFXClip(_sfxManager.PortalTraverse);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    _player.SetNearbyPortal(null);
        //}
    }


}
