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
    List<GJ25Customer> _notOrderedBlueCustomers = new List<GJ25Customer>();
    [SerializeField]
    List<GJ25Customer> _purpleCustomers;
    List<GJ25Customer> _notOrderedPurpleCustomers = new List<GJ25Customer>();
    [SerializeField]
    List<GJ25Customer> _orangeCustomers;
    List<GJ25Customer> _notOrderedOrangeCustomers = new List<GJ25Customer>();
    [SerializeField]
    List<GJ25Customer> _redCustomers;
    List<GJ25Customer> _notOrderedRedCustomers = new List<GJ25Customer>();

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

    [SerializeField]
    Canvas _instructionsCanvas;

    [SerializeField]
    GameState _currentGameState;

    [SerializeField]
    int _waitingGreenDrinks;
    [SerializeField]
    TMP_Text _waitingGreenDrinksText;
    [SerializeField]
    int _waitingBlueDrinks;
    [SerializeField]
    TMP_Text _waitingBlueDrinksText;
    [SerializeField]
    int _waitingPurpleDrinks;
    [SerializeField]
    TMP_Text _waitingPurpleDrinksText;
    [SerializeField]
    int _waitingOrangeDrinks;
    [SerializeField]
    TMP_Text _waitingOrangeDrinksText;
    [SerializeField]
    int _waitingRedDrinks;
    [SerializeField]
    TMP_Text _waitingRedDrinksText;

    void Start()
    {
        foreach (GJ25Customer customer in _greenCustomers)
        {
            _notOrderedGreenCustomers.Add(customer);
        }
        foreach (GJ25Customer customer in _blueCustomers)
        {
            _notOrderedBlueCustomers.Add(customer);
        }
        foreach (GJ25Customer customer in _purpleCustomers)
        {
            _notOrderedPurpleCustomers.Add(customer);
        }
        foreach (GJ25Customer customer in _orangeCustomers)
        {
            _notOrderedOrangeCustomers.Add(customer);
        }
        foreach (GJ25Customer customer in _redCustomers)
        {
            _notOrderedRedCustomers.Add(customer);
        }
        _orderTimer = Random.Range(_minOrderTime, _maxOrderTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentGameState == GameState.NightGameplay)
        {
            if (_orderTimer < 0.0f)
            {
                GJ25DrinkOrderChances currentChances;
                if (_currentNight == 1)
                {
                    currentChances = _nightOneChances;
                }
                else if (_currentNight == 2)
                {
                    currentChances = _nightTwoChances;
                }
                else
                {
                    currentChances = _nightThreeOnwardsChances;
                }
                ChooseRandomCustomerToOrder(currentChances);
                _orderTimer = Random.Range(_minOrderTime, _maxOrderTime);
            }
            else
            {
                _orderTimer -= Time.deltaTime;
            }
        }
    }

    public void ContinueFromInstructions()
    {
        _instructionsCanvas.gameObject.SetActive(false);
        NewNight();
        _currentGameState = GameState.NightIntro;
    }

    public void PutCustomerBackInNotOrdered(GJ25Customer customer, GJ25Player.Drink type)
    {
        switch (type)
        {
            case GJ25Player.Drink.Green:
                _notOrderedGreenCustomers.Add(customer);
                _waitingGreenDrinks--;
                _waitingGreenDrinksText.text = $"{_waitingGreenDrinks}";
                break;
            case GJ25Player.Drink.Blue:
                _notOrderedBlueCustomers.Add(customer);
                _waitingBlueDrinks--;
                _waitingBlueDrinksText.text = $"{_waitingBlueDrinks}";
                break;
            case GJ25Player.Drink.Purple:
                _notOrderedPurpleCustomers.Add(customer);
                _waitingPurpleDrinks--;
                _waitingPurpleDrinksText.text = $"{_waitingPurpleDrinks}";
                break;
            case GJ25Player.Drink.Orange:
                _notOrderedOrangeCustomers.Add(customer);
                _waitingOrangeDrinks--;
                _waitingOrangeDrinksText.text = $"{_waitingOrangeDrinks}";
                break;
            case GJ25Player.Drink.Red:
                _notOrderedRedCustomers.Add(customer);
                _waitingRedDrinks--;
                _waitingRedDrinksText.text = $"{_waitingRedDrinks}";
                break;
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
        //_player.ResetPlayer();
        Invoke("TurnOffIntroBox", 5.0f);
    }


    void TurnOffIntroBox()
    {
        _introCanvas.gameObject.SetActive(false);
        _gameCanvas.gameObject.SetActive(true);
        _nightText.text = $"Night: {_currentNight}";
        _currentGoldText.text = $"Gold: {_player.Gold}";
        _targetGoldText.text = $"Target Gold: {_targetGold}";
        //_player.CanMove();
        _currentGameState = GameState.NightGameplay;
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
            int r = Random.Range(0, _notOrderedBlueCustomers.Count - 1);
            GJ25Customer chosenCustomer = _notOrderedBlueCustomers[r];
            chosenCustomer.ReadyForDrink = true;
            _notOrderedBlueCustomers.Remove(chosenCustomer);
        }
        else if (rand < purpleChance)
        {
            int r = Random.Range(0, _notOrderedPurpleCustomers.Count - 1);
            GJ25Customer chosenCustomer = _notOrderedPurpleCustomers[r];
            chosenCustomer.ReadyForDrink = true;
            _notOrderedPurpleCustomers.Remove(chosenCustomer);
        }
        else if (rand < orangeChance)
        {
            int r = Random.Range(0, _notOrderedOrangeCustomers.Count - 1);
            GJ25Customer chosenCustomer = _notOrderedOrangeCustomers[r];
            chosenCustomer.ReadyForDrink = true;
            _notOrderedOrangeCustomers.Remove(chosenCustomer);
        }
        else if (rand < redChance)
        {
            int r = Random.Range(0, _notOrderedRedCustomers.Count - 1);
            GJ25Customer chosenCustomer = _notOrderedRedCustomers[r];
            chosenCustomer.ReadyForDrink = true;
            _notOrderedRedCustomers.Remove(chosenCustomer);
        }
    }

    public enum GameState
    {
        Instructions,
        NightIntro,
        NightGameplay,
        GameOver
    }
}
