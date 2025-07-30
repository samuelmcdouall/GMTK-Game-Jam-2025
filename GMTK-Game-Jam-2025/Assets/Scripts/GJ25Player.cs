using UnityEngine;
using UnityEngine.InputSystem;

public class GJ25Player : MonoBehaviour
{
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _idleAnimation = Animator.StringToHash("Idle");
        _moveAnimation = Animator.StringToHash("Move");
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = new Vector3(_move.action.ReadValue<Vector2>().x, 0.0f, _move.action.ReadValue<Vector2>().y);

        transform.position += _moveSpeed * Time.deltaTime * _moveDirection;

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
            transform.position = _nearbyPortal.PortalToLocation.position;
        }
    }
}
