using UnityEngine;

public class GJ25Float : MonoBehaviour
{
    public Vector3 floatSpeed;
    private bool goingUp = true;
    public float floatRate;
    private float floatTimer;
    [SerializeField]
    GameObject _pauseCanvas;

    void Update()
    {
        if (!_pauseCanvas.activeSelf)
        {
            floatTimer += Time.deltaTime;
            transform.Translate(floatSpeed);

            if (goingUp && floatTimer >= floatRate)
            {
                goingUp = false;
                floatTimer = 0;
                floatSpeed = -floatSpeed;
            }

            else if (!goingUp && floatTimer >= floatRate)
            {
                goingUp = true;
                floatTimer = 0;
                floatSpeed = -floatSpeed;
            }
        }
    }
}