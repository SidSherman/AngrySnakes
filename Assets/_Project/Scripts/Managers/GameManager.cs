using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private Menu _mainMenu;
    [SerializeField] private SoundManager _soundManager;

    public SoundManager SoundManager => _soundManager;

    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;
   
    [SerializeField] private Yandex _yandexSdk;
    [SerializeField] private Progress _progressInstance;

    [SerializeField] private List<EnemySpawner> _spawners;

    [SerializeField] private float _advGift = 1.0f;
    //private PlayerControlls _input;
    private int _gameState = 2;
    private static int _score = 0;
    private static int _recordScore = 0;

   
    
    private int PAUSE_STATE = 1;
    private int GAME_STATE = 2;
    private int CUTSCENE_STATE = 3;
   
    public static int Score { get => _score; set => _score = value; }

    public static GameManager GameManagerInstance;
    
    private void Awake()
    {
        GameManagerInstance = this;
        
        if (!_soundManager)
        {
            _soundManager = SoundManager.SoundManagerInstance;
        }
        if (!_progressInstance)
        {
            _progressInstance = Progress.ProgressInstance;
        }
        
    }

    private void Start()
    {

        if (!_yandexSdk)
            _yandexSdk = FindObjectOfType<Yandex>();
            
        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
        
        _spawners.AddRange(spawners);
        
        if (_progressInstance)
        {
            _score = _progressInstance.GetScore();
            _recordScore = _progressInstance.LevelsInfo[_progressInstance.GetLevel()].NeededScore;
        }


        if (_gameMenu)
        {
            _gameMenu.UpdateRecord(_recordScore);
            _gameMenu.UpdateCurrentScore(_score);
            
        }
        _gameState = GAME_STATE;
        Time.timeScale = 1f;
        
       
    }
    
    public void TogglePauseInput(InputAction.CallbackContext context)
    {
        TogglePause();
    }
    
    public void TogglePause()
    {
        //Debug.Log("TogglePause");
        if (_gameState == GAME_STATE)
        {
            Pause();
        }
        else if (_gameState == PAUSE_STATE)
        {
            Play();
        }
    }
    public void Pause()
    {
       // Debug.Log("Pause");
        if(_gameMenu)
            _gameMenu.PauseGame();
        Time.timeScale = 0f;
        _gameState = PAUSE_STATE;
     
    }

    public void Play()
    {
       // Debug.Log("Play");
        if(_gameMenu)
            _gameMenu.ResumeGame();
        Time.timeScale = 1f;
        _gameState = GAME_STATE;
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        _gameState = PAUSE_STATE;
        if (_gameMenu)
        {
            
            _gameMenu.LoseGame();
        }
        
        _progressInstance.SavePlayerInfo();
        _soundManager.PlaySound(_loseSound);
    }
    
    public void UpdateScore(int value)
    {
        _score +=value;
        
       
        if (_progressInstance)
        {
           // _progressInstance.SetScore(_score);
            if (_progressInstance.LevelsInfo[_progressInstance.GetLevel()].NeededScore <= _score)
            {
                if (_progressInstance.LevelsInfo.Count > _progressInstance.GetLevel() + 1)
                {
                    _progressInstance.SetLevel(_progressInstance.GetLevel() + 1);
                    _recordScore = _progressInstance.LevelsInfo[_progressInstance.GetLevel()].NeededScore;
                    _progressInstance.SetScore(_score);
                }
            }
        }

        if (_gameMenu)
        {
            _gameMenu.UpdateCurrentScore(_score);
            if( _progressInstance)
                _gameMenu.UpdateRecord(_progressInstance.LevelsInfo[_progressInstance.GetLevel()].NeededScore);
        }
        
        if(_mainMenu)
            _mainMenu.UpdateScore();
    }
    
    public void UnsetCameraFollowObject()
    {
     //   _camera.Follow = null;
    }

    public void ShowRewardedAdv()
    {
        Debug.Log("MY DEBUG ShowRewardedAdv BEFORE YandexSKD CHECK");
        if (_yandexSdk != null)
        {
            _yandexSdk.ShowRewardedAdvYandex();
            Debug.Log("MY DEBUG ShowRewardedAdv");
        }
            
    }
    
    public void Rebirth()
    {
        foreach (EnemySpawner spawner in _spawners)
        {
            spawner.SlowDownSpawn(_advGift);
            spawner.ClearEnemies();
        }

        _progressInstance.SavePlayerInfo();
        TogglePause();
    }
}


