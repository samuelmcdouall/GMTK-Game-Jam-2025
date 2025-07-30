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

    float _targetYPos;
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
            _ani.Play(_moveAnimation);
        }
        else
        {
            _ani.Play(_idleAnimation);
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

    private void Interact(InputAction.CallbackContext context)
    {
        print("Interacted!");
        if (_nearbyPortal)
        {
            Vector3 destination = _nearbyPortal.PortalToLocation.position;
            print($"Going to portal location: {destination}");
            _playerCC.enabled = false;
            transform.position = destination;
            _playerCC.enabled = true;
            _targetYPos = destination.y;
        }
    }
}
