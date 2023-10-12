
using System;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.SceneManagement;


public class Yandex : MonoBehaviour
    {
        public bool _firstEntry = false;
        public int _advCount = 0;

        public static Yandex YandexSDKInstance;
        
#if UNITY_WEBGL 
        [DllImport("__Internal")]
        private static extern void RateGame();
        
           
        [DllImport("__Internal")]
        private static extern void ShowAdv();
        
          [DllImport("__Internal")]
        private static extern void ShowRewardedAdv();
            
#endif 

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            
            if (YandexSDKInstance == null) {
                YandexSDKInstance = this;
            } else {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (!_firstEntry && SceneManager.GetActiveScene().name == "MainMenu")
            {
                if (_advCount % 2 == 0)
                {
                    #if UNITY_WEBGL
                        ShowAdv();
                    #endif 
                }

                _advCount++;

            }
            else
            {
                _firstEntry = false;
            }
        }

        public void AddReward()
        {
            if (GameManager.GameManagerInstance)
            {
                GameManager.GameManagerInstance.Rebirth();
            }
        }
        public void ShowRewardedAdvYandex()
        {
#if UNITY_WEBGL
            Debug.Log("MY DEBUG Call Js RewardedAdv");
            ShowRewardedAdv();
#endif 
        }
        
        public void RateMyGame()
        {
            
#if UNITY_WEBGL 
            RateGame();
#endif 
        }

    }

