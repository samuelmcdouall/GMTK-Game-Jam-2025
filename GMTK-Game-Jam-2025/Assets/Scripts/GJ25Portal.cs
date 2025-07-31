using UnityEngine;

public class GJ25Portal : MonoBehaviour
{
    [SerializeField]
    GJ25Player _player;
    [SerializeField]
    Transform _portalToLocation;
    [SerializeField]
    GJ25Player.CurrentLevel _level;

    void Start()
    {
        
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
