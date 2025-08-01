using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GJ25GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GJ25Player _player;
    [SerializeField]
    int _targetGold;
    int _currentNight;
    

    [SerializeField]
    float _minOrderTime;
    [SerializeField]
    float _maxOrderTime;
    float _orderTimer;

    [SerializeField]
    GJ25DrinkOrderChances _nightOneChances;
    [SerializeField]
    GJ25DrinkOrderChances _nightTwoChances;
    [SerializeField]
    GJ25DrinkOrderChances _nightThreeOnwardsChances;

    [SerializeField]
    List<GJ25Customer> _greenCustomers;
    List<GJ25Customer> _notOrderedGreenCustomers = new List<GJ25Customer>();
    [SerializeField]
    List<GJ25Customer> _blueCustomers; 
    [SerializeField]
    List<GJ25Customer> _purpleCustomers; 
    [SerializeField]
    List<GJ25Customer> _orangeCustomers; 
    [SerializeField]
    List<GJ25Customer> _redCustomers;

    [SerializeField]
    Canvas _gameCanvas;
    [SerializeField]
    TMP_Text _nightText;
    [SerializeField]
    TMP_Text _targetGoldText;
    [SerializeField]
    TMP_Text _currentGoldText;

    [SerializeField]
    Canvas _introCanvas;
    [SerializeField]
    TMP_Text _nightIntroText;
    [SerializeField]
    TMP_Text _targetGoldIntroText;
    [SerializeField]
    TMP_Text _currentGoldIntroText;

    void Start()
    {
        foreach(GJ25Customer customer in _greenCustomers)
        {
            _notOrderedGreenCustomers.Add(customer);
        }
        NewNight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutCustomerBackInNotOrdered(GJ25Customer customer, GJ25Player.Drink type)
    {
        if (type == GJ25Player.Drink.Green)
        {
            _notOrderedGreenCustomers.Add(customer);
        }
    }

    void NewNight()
    {
        _currentNight++;
        if (_targetGold <= 400)
        {
            _targetGold += 100;
        }
        else if (_targetGold <= 1250)
        {
            _targetGold += 250;
        }
        else
        {
            _targetGold += 500;
        }
        _introCanvas.gameObject.SetActive(true);
        _gameCanvas.gameObject.SetActive(false);
        _nightIntroText.text = $"Night: {_currentNight}";
        _currentGoldIntroText.text = $"Gold: {_player.Gold}";
        _targetGoldIntroText.text = $"Target Gold: {_targetGold}";
        Invoke("TurnOffIntroBox", 5.0f);
    }


    void TurnOffIntroBox()
    {
        _introCanvas.gameObject.SetActive(false);
        _gameCanvas.gameObject.SetActive(true);
        _nightText.text = $"Night: {_currentNight}";
        _currentGoldText.text = $"Gold: {_player.Gold}";
        _targetGoldText.text = $"Target Gold: {_targetGold}";
    }


    public void SetCurrentGoldText(int amount)
    {
        _currentGoldText.text = $"Gold: {amount}";
    }

    void ChooseRandomCustomerToOrder(GJ25DrinkOrderChances chances)
    {
        float rand = Random.Range(0.0f, 1.0f);
        float greenChance = chances.GreenDrinkChance;
        float blueChance = greenChance + chances.BlueDrinkChance;
        float purpleChance = blueChance + chances.PurpleDrinkChance;
        float orangeChance = purpleChance + chances.OrangeDrinkChance;
        float redChance = orangeChance + chances.RedDrinkChance;
        
        if (rand < greenChance)
        {
            int r = Random.Range(0, _notOrderedGreenCustomers.Count - 1);
            GJ25Customer chosenCustomer = _notOrderedGreenCustomers[r];
            chosenCustomer.ReadyForDrink = true;
            _notOrderedGreenCustomers.Remove(chosenCustomer);

        }
        else if (rand < blueChance)
        {

        }
    }
}
