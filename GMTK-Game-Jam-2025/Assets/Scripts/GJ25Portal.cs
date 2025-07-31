using UnityEngine;

public class GJ25Portal : MonoBehaviour
{
    [SerializeField]
    GJ25Player _player;
    [SerializeField]
    Transform _portalToLocation;
    public GJ25Player.CurrentLevel Level;
    public Transform PortalToLocation 
    { 
        get => _portalToLocation;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Nearby portal");
            _player.SetNearbyPortal(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.SetNearbyPortal(null);
        }
    }


}
