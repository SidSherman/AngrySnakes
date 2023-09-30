using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu
{
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _recordTable;
    [SerializeField] private TextMeshProUGUI _scoreValue;
    [SerializeField] private TextMeshProUGUI _finishScore;
    [SerializeField] private TextMeshProUGUI _toolTipText;
    [SerializeField] private Image _healthValue;
   // [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _DeathPanel;
   
    [SerializeField] private Player _player;
    [SerializeField] private GameManager _gameManager;
    
    private void Start()
    {
        
        if (!_audioSource)
        {
            _audioSource = SoundManager.SoundManagerInstance.DefaultSource;
        }
        if (!_listener)
        {
            _listener = SoundManager.SoundManagerInstance.DefaultListener;
        }
        if (!_sceneManager)
        {
            _sceneManager = SceneLoader.SceneLoaderInstance;
        }
        
        if (!_player)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _player = player.GetComponent<Player>();
        }
        
        if (!_gameManager)
        {
            _gameManager = GameManager.GameManagerInstance;;
        }
        
    }

    public void RestartGame()
    {
        _sceneManager.ReloadScene();

    }
    
    public void PauseGame()
    {
        _gamePanel.SetActive(false);
        _menuPanel.SetActive(true);
        
    }
    
    public void ResumeGame()
    {
        _gamePanel.SetActive(true);
        _menuPanel.SetActive(false);
    }
    
    /*public void FinishGame()
    {
        Time.timeScale = 0f;
        if (!_finishPanel)
            return;
        
        _finishPanel.SetActive(true);
        
        if (!_finishScore)
            return;


        _finishScore.text = $"Ваш счёт: {GameManager.Score}\n змеек";

    }*/
    
    public void LoseGame()
    {
      
        if (!_DeathPanel)
            return;
        
        _DeathPanel.SetActive(true);
        
        if (!_finishScore)
            return;
        
        _finishScore.text = $"Ваш счёт: {GameManager.Score}\n змеек";
        
    }

    public void ReturnToMainMenu(string sceneName = "MainMenu")
    {
        _sceneManager.LoadSceneByName(sceneName);
    }
    public void UpdateMessage(string value)
    {
        if(_messageText)
            _messageText.text = value.ToString();
    }
    
    public void UpdateRecord(int value)
    {
        if(_recordTable)
            _recordTable.text = $"{value} змеек";
    }
    
    public void UpdateCurrentScore(int value)
    {
        if (!_scoreValue)
            return;
        _scoreValue.text = $"{value} змеек";
    }
    
    public void UpdateHealth(float value)
    {
        _healthValue.fillAmount = value / 100;
    }
}