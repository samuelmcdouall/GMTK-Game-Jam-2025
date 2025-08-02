using UnityEngine;

public class GJ25Customer : MonoBehaviour
{
    [SerializeField]
    bool _readyForDrink;
    [SerializeField]
    GJ25Player _player;
    [SerializeField]
    GJ25Player.Drink _desiredDrink;
    [SerializeField]
    int _payment;
    [SerializeField]
    GameObject _readyIcon;
    [SerializeField]
    GJ25GameManager _gameManager;

    public bool ReadyForDrink
    {
        get => _readyForDrink;
        set
        {
            _readyForDrink = value;
            _readyIcon.SetActive(value);
        }
    }
    public GJ25Player.Drink DesiredDrink
    {
        get => _desiredDrink;
        set => _desiredDrink = value;
    }
    public int Payment
    {
        get => _payment;
        set => _payment = value;
    }
    public void InteractWithCustomer()
    {
        ReadyForDrink = false;
        _gameManager.PutCustomerBackInNotOrdered(this, _desiredDrink);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.SetNearbyCustomer(this);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.SetNearbyCustomer(null);
        }
    }
}