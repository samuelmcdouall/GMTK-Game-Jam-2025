using UnityEngine;

public class GJ25Trash : MonoBehaviour
{
    [SerializeField]
    GJ25Player _player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print("Nearby barrel");
            _player.NearTrash = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.NearTrash = false;
        }
    }
}
