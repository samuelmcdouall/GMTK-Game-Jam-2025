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
    InputActionReference _interact;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = new Vector3(_move.action.ReadValue<Vector2>().x, 0.0f, _move.action.ReadValue<Vector2>().y);
        //print(_moveDirection);
        transform.position += _moveSpeed * Time.deltaTime * _moveDirection;
    }

    private void OnEnable()
    {
        _interact.action.started += Interact;
    }

    private void OnDisable()
    {
        _interact.action.started -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        print("Interacted!");
    }
}
