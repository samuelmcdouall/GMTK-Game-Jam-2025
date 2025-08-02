using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GJ25GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GJ25Player _player;
    [SerializeField]
    int _targetGold;
    int _currentNight;

    //[SerializeField]
    //float _realTimePerGameHour; // in seconds
    //[SerializeField]
    //float _hourTimer;
    [SerializeField]
    float _totalLevelTime;
    [SerializeField]
    float _levelTimer;
    [SerializeField]
    Slider _levelTimeSlider;
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

    [SerializeField]
    List<GJ25Barrel> _barrels;

    //[SerializeField]
    //TMP_Text _instructionsText;

    [SerializeField]
    InputActionReference _pause;
    [SerializeField]
    Canvas _pauseCanvas;

    public GameState CurrentGameState { get => _currentGameState; set => _currentGameState = value; }

    void Start()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        print(Application.targetFrameRate);
        _instructionsCanvas.gameObject.SetActive(true);
        ResetNotOrderedLists();
        foreach (GJ25Barrel barrel in _barrels)
        {
            barrel.ResetBarrel();
        }
        _orderTimer = Random.Range(_minOrderTime, _maxOrderTime);
        //_hourTimer = _realTimePerGameHour;
        _levelTimer = _totalLevelTime;
        _waitingGreenDrinksText.text = "0";
        _waitingBlueDrinksText.text = "0";
        _waitingPurpleDrinksText.text = "0";
        _waitingOrangeDrinksText.text = "0";
        _waitingRedDrinksText.text = "0";
        _levelTime.text = $"{_gameHourTime} PM";
        _targetGold = 100; // will be 150 after first day

        //_instructionsText.text = "Your minions have had a long, hard day fighting heroes and they could use a drink! Your new establishment “The Drinking Rings” should help with this! \n\n" +
        //                         "Use the <color=#FFD500>barrels to brew</color> different drinks. Making a drink <color=#FFD500>costs gold</color> and more expensive ones take <color=#FFD500>longer to brew</color> but can be sold for a <color=#FFD500>higher price</color>\n\n" +
        //                         "Make sure to <color=#FFD500>pick up the drink</color> after you’ve made it. If you don’t want the drink you’re holding then it can be <color=#FFD500>discarded into the black portal</color> in the center. Be aware once a drink is discarded, it is lost forever!\n\n" +
        //                         "Use the <color=#FFD500>portals to traverse</color> between the different coloured rings\n\n" +
        //                         "Each minion will only take the <color=#FFD500>drink associated with their colour ring</color>. For example, those on the <color=#FFD500>blue ring</color> will only take <color=#FFD500>blue drinks</color>\n\n" +
        //                         "When a minion is <color=#FFD500>ready for a drink</color>, a <color=#FFD500>marker</color> will appear above them. You’ll also see how many minions of each ring wants a drink by the <color=#FFD500>icons in the top right hand corner</color>\n\n" +
        //                         "To stay open and continue to the next night you’ll need to gather <color=#FFD500>enough gold before closing time</color>\n\n" +
        //                         "Use WASD to move and E to interact. Good luck!\n\n";
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


    private void OnEnable()
    {
        _pause.action.started += Pause;
    }

    private void OnDisable()
    {
        _pause.action.started -= Pause;
    }

    void Pause(InputAction.CallbackContext context)
    {
        if (!_pauseCanvas.gameObject.activeSelf)
        {
            _pauseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            //Cursor.visible = true;
        }
        else
        {
            _pauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            //Cursor.visible = false;
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

            if (_levelTimer < 0.0f)
            {
                //if (_gameHourTime == 11)
                {
                    if (_player.Gold >= _targetGold)
                    {
                        _orderTimer = Random.Range(_minOrderTime, _maxOrderTime);
                        _levelTimer = _totalLevelTime;
                        ResetNotOrderedLists();
                        foreach (GJ25Barrel barrel in _barrels)
                        {
                            barrel.ResetBarrel();
                        }
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
                        _player.ResetPlayer();
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
                //else
                //{
                //    //_gameHourTime++;
                //    //_levelTime.text = $"{_gameHourTime} PM";
                //    //_hourTimer = _realTimePerGameHour;
                //}
            }
            else
            {
                _levelTimer -= Time.deltaTime;
                _levelTimeSlider.value = _levelTimer / _totalLevelTime;
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
        if (_targetGold <= 150)
        {
            _targetGold += 50;
        }
        else if (_targetGold <= 400)
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
        //Cursor.visible = false;
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
