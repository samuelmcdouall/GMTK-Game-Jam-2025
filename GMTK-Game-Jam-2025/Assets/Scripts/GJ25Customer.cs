using UnityEngine;

public class GJ25Customer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    //[SerializeField]
    //float _bonusPayment;

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

    void Start()
    {
        ReadyForDrink = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            //print("Nearby barrel");
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
