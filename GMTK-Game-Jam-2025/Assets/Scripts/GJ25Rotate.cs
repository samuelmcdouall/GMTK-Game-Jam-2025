using UnityEngine;

public class GJ25Rotate : MonoBehaviour
{
    [SerializeField]
    Vector3 _rotationAngle;
    [SerializeField]
    float _rotationSpeed;

    void Update()
    {
        transform.Rotate(_rotationAngle * _rotationSpeed * Time.deltaTime);
    }
}