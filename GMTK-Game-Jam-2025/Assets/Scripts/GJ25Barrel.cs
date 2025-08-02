using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GJ25Barrel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GJ25Player _player;
    [SerializeField]
    BarrelStatus _barrelStatus;
    [SerializeField]
    int _drinkCost;
    float _timer;
    [SerializeField]
    float _brewingTime;
    [SerializeField]
    Slider _brewingSlider;
    [SerializeField]
    GameObject _drink;
    [SerializeField]
    GJ25Player.Drink _drinkType;
    [SerializeField]
    float _readyPeriod;
    [SerializeField]
    float _flashPeriod;
    [SerializeField]
    float _flashInterval;
    float _flashTimer;
    [SerializeField]
    bool _unlimitedLife;


    void Start()
    {

    }

    public void ResetBarrel()
    {
        _brewingSlider.value = 0.0f; 
        _brewingSlider.gameObject.SetActive(false);
        _barrelStatus = BarrelStatus.Idle;
        _drink.SetActive(false);
        _timer = _brewingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_barrelStatus == BarrelStatus.Brewing)
        {
            _brewingSlider.value = 1.0f - (_timer / _brewingTime);
            if (_timer < 0.0f)
            {
                _brewingSlider.gameObject.SetActive(false);
                _drink.SetActive(true);
                _barrelStatus = BarrelStatus.DrinkReady;
                _timer = _readyPeriod;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
        else if (_barrelStatus == BarrelStatus.DrinkReady && !_unlimitedLife)
        {
            if (_timer < 0.0f)
            {
                _barrelStatus = BarrelStatus.DrinkFlashing;
                _timer = _flashPeriod;
                _flashTimer = _flashInterval;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
        else if (_barrelStatus == BarrelStatus.DrinkFlashing)
        {
            if (_timer < 0.0f)
            {
                _barrelStatus = BarrelStatus.Idle;
                _drink.SetActive(false);
            }
            else
            {
                _timer -= Time.deltaTime;
            }
            if (_flashTimer < 0.0f)
            {
                _drink.SetActive(!_drink.activeSelf);
                _flashTimer = _flashInterval;
            }
            else
            {
                _flashTimer -= Time.deltaTime;
            }
        }
    }


    public void InteractWithBarrel()
    {
        if (_barrelStatus == BarrelStatus.Idle && _drinkCost <= _player.Gold)
        {
            _player.Gold -= _drinkCost;
            _barrelStatus = BarrelStatus.Brewing;
            _timer = _brewingTime;
            _brewingSlider.value = 0.0f;
            _brewingSlider.gameObject.SetActive(true);
        }
        else if (_barrelStatus == BarrelStatus.Brewing)
        {
            // Do nothing, don't want to interrupt
        }
        else if (_barrelStatus == BarrelStatus.DrinkReady || _barrelStatus == BarrelStatus.DrinkFlashing)
        {
            if (_player.CarryingDrink == GJ25Player.Drink.None)
            {
                _player.GiveDrink(_drinkType);
                _drink.SetActive(false);
                _barrelStatus = BarrelStatus.Idle;
            }
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print("Nearby barrel");
            _player.SetNearbyBarrel(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.SetNearbyBarrel(null);
        }
    }

    public enum BarrelStatus
    {
        Idle,
        Brewing,
        DrinkReady,
        DrinkFlashing
    }
}
