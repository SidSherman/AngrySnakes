
using System;
using System.Runtime.InteropServices;

using UnityEngine;



    public class Yandex : MonoBehaviour
    {
        
#if UNITY_WEBGL____ 
        [DllImport("__Internal")]
        private static extern void RateGame();
#endif 

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void RateMyGame()
        {
            
#if UNITY_WEBGL____ 
            RateGame();
#endif 
        }

    }

