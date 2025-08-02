using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GJ25GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GJ25Player _player;
    [SerializeField]
    int _targetGold;
    int _currentNight;

    [SerializeField]
    float _realTimePerGameHour; // in seconds
    [SerializeField]
    float _hourTimer;
    [SerializeField]
    TMP_Text _levelTime;
    [SerializeField]
    int _gameHourTime;
    

    [SerializeField]
    float _minOrderTime;
    [SerializeField]
    float _maxOrderTime;
    [SerializeField]
    float _orderTimer;

    [SerializeField]
    GJ25DrinkOrderChances _nightOneChances;
    [SerializeField]
    GJ25DrinkOrderChances _nightTwoChances;
    [SerializeField]
    GJ25DrinkOrderChances _nightThreeOnwardsChances;

    [SerializeField]
    GameObject _greenCustomers;
    List<GJ25Customer> _notOrderedGreenCustomers = new List<GJ25Customer>();
    [SerializeField]
    GameObject _blueCustomers;
    List<GJ25Customer> _notOrderedBlueCustomers = new List<GJ25Customer>();
    [SerializeField]
    GameObject _purpleCustomers;
    List<GJ25Customer> _notOrderedPurpleCustomers = new List<GJ25Customer>();
    [SerializeField]
    GameObject _orangeCustomers;
    List<GJ25Customer> _notOrderedOrangeCustomers = new List<GJ25Customer>();
    [SerializeField]
    GameObject _redCustomers;
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

    [SerializeField]
    Canvas _gameOverCanvas;
    [SerializeField]
    TMP_Text _gameOverText;

    public GameState CurrentGameState { get => _currentGameState; set => _currentGameState = value; }

    void Start()
    {
        _instructionsCanvas.gameObject.SetActive(true);
        ResetNotOrderedLists();
        _orderTimer = Random.Range(_minOrderTime, _maxOrderTime);
        _hourTimer = _realTimePerGameHour;
        _waitingGreenDrinksText.text = "0";
        _waitingBlueDrinksText.text = "0";
        _waitingPurpleDrinksText.text = "0";
        _waitingOrangeDrinksText.text = "0";
        _waitingRedDrinksText.text = "0";
        _levelTime.text = $"{_gameHourTime} PM";
        _targetGold = 100; // will be 150 after first day
    }

    private void ResetNotOrderedLists()
    {
        _notOrderedGreenCustomers.Clear();
        _notOrderedBlueCustomers.Clear();
        _notOrderedPurpleCustomers.Clear();
        _notOrderedOrangeCustomers.Clear();
        _notOrderedRedCustomers.Clear();
        foreach (Transform customer in _greenCustomers.transform)
        {
            _notOrderedGreenCustomers.Add(customer.gameObject.GetComponent<GJ25Customer>());
            customer.gameObject.GetComponent<GJ25Customer>().ReadyForDrink = false;
        }
        foreach (Transform customer in _blueCustomers.transform)
        {
            _notOrderedBlueCustomers.Add(customer.gameObject.GetComponent<GJ25Customer>());
            customer.gameObject.GetComponent<GJ25Customer>().ReadyForDrink = false;
        }
        foreach (Transform customer in _purpleCustomers.transform)
        {
            _notOrderedPurpleCustomers.Add(customer.gameObject.GetComponent<GJ25Customer>());
            customer.gameObject.GetComponent<GJ25Customer>().ReadyForDrink = false;
        }
        foreach (Transform customer in _orangeCustomers.transform)
        {
            _notOrderedOrangeCustomers.Add(customer.gameObject.GetComponent<GJ25Customer>());
            customer.gameObject.GetComponent<GJ25Customer>().ReadyForDrink = false;
        }
        foreach (Transform customer in _redCustomers.transform)
        {
            _notOrderedRedCustomers.Add(customer.gameObject.GetComponent<GJ25Customer>());
            customer.gameObject.GetComponent<GJ25Customer>().ReadyForDrink = false;
        }
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

            if (_hourTimer < 0.0f)
            {
                if (_gameHourTime == 11)
                {
                    if (_player.Gold >= _targetGold)
                    {
                        _orderTimer = Random.Range(_minOrderTime, _maxOrderTime);
                        _hourTimer = _realTimePerGameHour;
                        ResetNotOrderedLists();
                        _waitingGreenDrinks = 0;
                        _waitingGreenDrinksText.text = "0";
                        _waitingBlueDrinks = 0;
                        _waitingBlueDrinksText.text = "0";
                        _waitingPurpleDrinks = 0;
                        _waitingPurpleDrinksText.text = "0";
                        _waitingOrangeDrinks = 0;
                        _waitingOrangeDrinksText.text = "0";
                        _waitingRedDrinks = 0;
                        _waitingRedDrinksText.text = "0";
                        _gameHourTime = 7;
                        _levelTime.text = $"{_gameHourTime} PM";
                        NewNight();
                        _currentGameState = GameState.NightIntro;
                        _player.ResetPosition();
                    }
                    else
                    {
                        _gameCanvas.gameObject.SetActive(false);
                        _currentGameState = GameState.GameOver;
                        string plural = "nights";
                        if (_currentNight == 1)
                        {
                            plural = "night";
                        }
                        _gameOverText.text = $"Game Over!\n\nYou were open for {_currentNight} {plural} and gathered {_player.Gold} gold!";
                        _gameOverCanvas.gameObject.SetActive(true);
                    }
                }
                else
                {
                    _gameHourTime++;
                    _levelTime.text = $"{_gameHourTime} PM";
                    _hourTimer = _realTimePerGameHour;
                }
            }
            else
            {
                _hourTimer -= Time.deltaTime;
            }
        }
    }

    public void ClickRetryButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void ClickStartButton()
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
        if (_targetGold <= 250)
        {
            _targetGold += 50;
        }
        else if (_targetGold <= 900)
        {
            _targetGold += 100;
        }
        else
        {
            _targetGold += 200;
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
        _currentGameState = GameState.NightGameplay;

        // Spawn first drink to get going 
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
        if (rand < greenChance && _notOrderedGreenCustomers.Count > 0)
        {
            int r = Random.Range(0, _notOrderedGreenCustomers.Count - 1);
            GJ25Customer chosenCustomer = _notOrderedGreenCustomers[r];
            chosenCustomer.ReadyForDrink = true;
            _notOrderedGreenCustomers.Remove(chosenCustomer);
            _waitingGreenDrinks++;
            _waitingGreenDrinksText.text = $"{_waitingGreenDrinks}";

        }
        else if (rand < blueChance && _notOrderedBlueCustomers.Count > 0)
        {
            int r = Random.Range(0, _notOrderedBlueCustomers.Count - 1);
            GJ25Customer chosenCustomer = _notOrderedBlueCustomers[r];
            chosenCustomer.ReadyForDrink = true;
            _notOrderedBlueCustomers.Remove(chosenCustomer);
            _waitingBlueDrinks++;
            _waitingBlueDrinksText.text = $"{_waitingBlueDrinks}";
        }
        else if (rand < purpleChance && _notOrderedPurpleCustomers.Count > 0)
        {
            int r = Random.Range(0, _notOrderedPurpleCustomers.Count - 1);
            GJ25Customer chosenCustomer = _notOrderedPurpleCustomers[r];
            chosenCustomer.ReadyForDrink = true;
            _notOrderedPurpleCustomers.Remove(chosenCustomer);
            _waitingPurpleDrinks++;
            _waitingPurpleDrinksText.text = $"{_waitingPurpleDrinks}";
        }
        else if (rand < orangeChance && _notOrderedOrangeCustomers.Count > 0 && chances.OrangeDrinkChance != 0.0f) // check for orange and red drinks as they should not be spawning in the 1st/2nd level, even if the other levels are full up
        {
            int r = Random.Range(0, _notOrderedOrangeCustomers.Count - 1);
            GJ25Customer chosenCustomer = _notOrderedOrangeCustomers[r];
            chosenCustomer.ReadyForDrink = true;
            _notOrderedOrangeCustomers.Remove(chosenCustomer);
            _waitingOrangeDrinks++;
            _waitingOrangeDrinksText.text = $"{_waitingOrangeDrinks}";
        }
        else if (rand < redChance && _notOrderedRedCustomers.Count > 0 && chances.RedDrinkChance != 0.0f)
        {
            int r = Random.Range(0, _notOrderedRedCustomers.Count - 1);
            GJ25Customer chosenCustomer = _notOrderedRedCustomers[r];
            chosenCustomer.ReadyForDrink = true;
            _notOrderedRedCustomers.Remove(chosenCustomer);
            _waitingRedDrinks++;
            _waitingRedDrinksText.text = $"{_waitingRedDrinks}";
        }
    }

    public enum GameState
    {
        Instructions,
        NightIntro,
        NightGameplay,
        GameOver
    }

    // check this is running with UI, for one infinite night, then do others
}
