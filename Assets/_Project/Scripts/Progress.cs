using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


    
public class Progress : MonoBehaviour
{
    [SerializeField] private List<Levels> _levelsInfo;
    [SerializeField] private PlayerInfo _playerInfo;

    public PlayerInfo PlayerInfoRef
    {
        get => _playerInfo;
        set => _playerInfo = value;
    }

    public List<Levels> LevelsInfo => _levelsInfo;
    public static Progress ProgressInstance;

    
    #if UNITY_WEBGL____ 
    [DllImport("__Internal")]
    private static extern void CollectPlayerData();

    [DllImport("__Internal")]
    private static extern void SavePlayerData(string data);

#endif

    private void Awake()
    {
        _playerInfo = new PlayerInfo();
        DontDestroyOnLoad(this.gameObject);

        
        if (ProgressInstance == null) {
            ProgressInstance = this;
        } else {
            Destroy(gameObject);
        }
        
        SetLevel(0);
        SetScore(0);
    #if UNITY_WEBGL____ 
       CollectPlayerData();
    #endif 
    }
    

    public void SetScore(int score)
    {
        _playerInfo.CurrentScore = score;
    }

    public void SetLevel(int level)
    {
        _playerInfo.Level = level;
    }

    public int GetScore()
    {
        return _playerInfo.CurrentScore; 
    }

    public int GetLevel()
    {
        return _playerInfo.Level;
    }
    
    public void SetPlayerInfo(string value)
    {
        PlayerInfoRef = JsonUtility.FromJson<PlayerInfo>(value);
    }
    
    public void SavePlayerInfo()
    {
        string jsonstring = JsonUtility.ToJson(PlayerInfoRef);
#if UNITY_WEBGL____ 
        SavePlayerData(jsonstring);
#endif 
    }
    
    [Serializable]
    public class PlayerInfo
    {
        public int Level;
        public int CurrentScore;
    }
    
    [Serializable]
    public struct Levels
    {
        public int Level;
        public int NeededScore;
    }
}

