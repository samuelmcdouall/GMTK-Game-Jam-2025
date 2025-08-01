using UnityEngine;
using UnityEngine.InputSystem;

public class GJ25Player : MonoBehaviour
{
    CharacterController _playerCC;
    [SerializeField]
    float _moveSpeed;
    [SerializeField]
    InputActionReference _move;
    Vector3 _moveDirection;
    [SerializeField]
    Transform _modelTransform;

    [SerializeField]
    InputActionReference _interact;

    [SerializeField]
    Animator _ani;
    int _idleAnimation;
    int _moveAnimation;

    [SerializeField]
    GJ25Portal _nearbyPortal;

    [SerializeField]
    GJ25Barrel _nearbyBarrel;

    [SerializeField]
    GJ25Customer _nearbyCustomer;

    float _targetYPos;

    Drink _currentDrinkCarrying;
    [SerializeField]
    GameObject _greenDrinkCarried;
    [SerializeField]
    GameObject _blueDrinkCarried;
    [SerializeField]
    GameObject _purpleDrinkCarried;
    [SerializeField]
    GameObject _orangeDrinkCarried;
    [SerializeField]
    GameObject _redDrinkCarried;

    [SerializeField]
    CurrentLevel _currentLevel;

    [SerializeField]
    GJ25GameManager _gameManager;

    public Drink CarryingDrink 
    { 
        get => _currentDrinkCarrying; 
        set => _currentDrinkCarrying = value; 
    }
    public int Gold 
    { 
        get => _gold;
        set
        {
            _gold = value;
            _gameManager.SetCurrentGoldText(value);
        }
    }

    [SerializeField]
    int _gold;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerCC = GetComponent<CharacterController>();
        _idleAnimation = Animator.StringToHash("Idle");
        _moveAnimation = Animator.StringToHash("Move");
        _targetYPos = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = new Vector3(_move.action.ReadValue<Vector2>().x, 0.0f, _move.action.ReadValue<Vector2>().y);
        
        //transform.position += _moveSpeed * Time.deltaTime * _moveDirection;
        _playerCC.Move(_moveSpeed * Time.deltaTime * _moveDirection);
        if (_moveDirection != Vector3.zero)
        {
            _modelTransform.rotation = Quaternion.LookRotation(_moveDirection);
            //_ani.Play(_moveAnimation);
        }
        else
        {
            //_ani.Play(_idleAnimation);
        }
    }

    private void LateUpdate()
    {
        _playerCC.enabled = false;
        transform.position = new Vector3(transform.position.x, _targetYPos, transform.position.z);
        _playerCC.enabled = true;
    }



    private void OnEnable()
    {
        _interact.action.started += Interact;
    }

    private void OnDisable()
    {
        _interact.action.started -= Interact;
    }

    public void SetNearbyPortal(GJ25Portal portal)
    {
        _nearbyPortal = portal;
    }

    public void SetNearbyBarrel(GJ25Barrel barrel)
    {
        _nearbyBarrel = barrel;
    }

    public void SetNearbyCustomer(GJ25Customer customer)
    {
        _nearbyCustomer = customer;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        print("Interacted!");
        //if (_nearbyPortal)
        //{
        //    TeleportPlayer();
        //}
        if (_nearbyBarrel)
        {
            _nearbyBarrel.InteractWithBarrel();
        }
        else if (_nearbyCustomer && _nearbyCustomer.ReadyForDrink && _nearbyCustomer.DesiredDrink == _currentDrinkCarrying)
        {
            Gold += _nearbyCustomer.Payment;
            _nearbyCustomer.InteractWithCustomer();
            _currentDrinkCarrying = Drink.None;
            _greenDrinkCarried.SetActive(false);
            _blueDrinkCarried.SetActive(false);
            _purpleDrinkCarried.SetActive(false);
            _orangeDrinkCarried.SetActive(false);
            _redDrinkCarried.SetActive(false);
        }
    }

    public void TeleportPlayer(Vector3 position, CurrentLevel newLevel)
    {
        print($"Going to portal location: {position}");
        _playerCC.enabled = false;
        transform.position = position;
        _playerCC.enabled = true;
        _targetYPos = position.y;
        _currentLevel = newLevel;
    }

    public void GiveDrink(Drink drink)
    {
        _currentDrinkCarrying = drink;
        switch (drink)
        {
            case Drink.Green:
                _greenDrinkCarried.SetActive(true);
                break;
            case Drink.Blue:
                _blueDrinkCarried.SetActive(true);
                break;
            case Drink.Purple:
                _purpleDrinkCarried.SetActive(true);
                break;
            case Drink.Orange:
                _orangeDrinkCarried.SetActive(true);
                break;
            case Drink.Red:
                _redDrinkCarried.SetActive(true);
                break;
        }
    }

    public enum CurrentLevel
    {
        Base,
        One,
        Two,
        Three,
        Four,
        Five
    }

    public enum Drink
    {
        None,
        Green,
        Blue,
        Purple,
        Orange,
        Red
    }
}
