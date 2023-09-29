using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu
{
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _recordTable;
    [SerializeField] private TextMeshProUGUI _toolTipText;
    [SerializeField] private Image _healthValue;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _DeathPanel;
    [SerializeField] private TextMeshProUGUI _finishInfo;
    [SerializeField] private Player _player;
   // [SerializeField] private GameManager _gameManager;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<Player>();
        //_player.HealthComponent.onHealthChange += UpdateHealth;
        
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
    
    public void FinishGame()
    {
        Time.timeScale = 0f;
        if (!_finishPanel)
            return;
        
        _finishPanel.SetActive(true);
        
        if (!_finishInfo)
            return;


        _finishInfo.text = $"Ваш рекорд: {GameManager.Score}\n змеек";

    }
    public void LoseGame()
    {
        _DeathPanel.SetActive(true);
    }
    
    public void UpdateMessage(string value)
    {
        if(_messageText)
            _messageText.text = value.ToString();
    }
    
    public void UpdateRecord(int value)
    {
        if(_recordTable)
            _recordTable.text = "Рекорд: "+ value.ToString();
    }
    
    public void UpdateHealth(float value)
    {
        _healthValue.fillAmount = value / 100;
    }
}