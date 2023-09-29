using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
   
    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;
    //private PlayerControlls _input;
    private int _gameState = 2;
    private static int _score = 0;

    private int PAUSE_STATE = 1;
    private int GAME_STATE = 2;
    private int CUTSCENE_STATE = 3;
   
    public static int Score { get => _score; set => _score = value; }

    public static GameManager GameManagerInstance;
    private void Start()
    {

        _gameState = GAME_STATE;
        Time.timeScale = 1f;
    }

    private void Update()
    {
       // if(_gameState == GAME_STATE)

    }

    public void TogglePauseInput(InputAction.CallbackContext context)
    {
        TogglePause();
    }
    
    public void TogglePause()
    {
        Debug.Log("TogglePause");
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
        Debug.Log("Pause");
        if(_gameMenu)
            _gameMenu.PauseGame();
        Time.timeScale = 0f;
        _gameState = PAUSE_STATE;
     
    }

    public void Play()
    {
        Debug.Log("Play");
        if(_gameMenu)
            _gameMenu.ResumeGame();
        Time.timeScale = 1f;
        _gameState = GAME_STATE;
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        if (_gameMenu)
        {
            _gameMenu.LoseGame();
        }
        ClearStaticValues();
        _soundManager.PlaySound(_loseSound);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        if(_gameMenu)
            _gameMenu.FinishGame();
        ClearStaticValues();
        _soundManager.PlaySound(_winSound);
    }
    
    public void ClearStaticValues()
    {
        _score = 0;
    }
    
    public void UpdateScore(int value)
    {
        _score +=value;
        if(_gameMenu)
            _gameMenu.UpdateRecord(_score);
    }
    
    public void UnsetCameraFollowObject()
    {
     //   _camera.Follow = null;
    }

   
}