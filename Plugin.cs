using BepInEx;
using UnityEngine;
using System;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace ReverseGrip
{
    [BepInPlugin("ReverseGrip", "ReverseGrip", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private GameObject rightWeapon;
        private GameObject leftWeapon;

        private void Start()
        {
            HarmonyPatches.ApplyHarmonyPatches();

            
            // Plugin startup logic
            Logger.LogInfo($"Plugin ReverseGrip is loaded!");
        }

        void Update()
        {
            if(leftWeapon == PlayerManager.instance.leftSword.transform.parent.gameObject)
            {
                if (PlayerManager.instance.leftSword.transform.parent.localRotation.y == 0f)
                {
                    leftWeapon.transform.localRotation = Quaternion.Euler(90f, 180f, 0f);
                    rightWeapon.transform.localRotation = Quaternion.Euler(90f, 180f, 0f);
                }
            }
        }
        

        private void OnEnable()
        {
            // Subscribe to the sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            // Unsubscribe from the sceneLoaded event when this script is disabled or destroyed
            SceneManager.sceneLoaded -= OnSceneLoaded;
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (PhotonNetwork.InRoom)
            {
                leftWeapon = PlayerManager.instance.leftSword.transform.parent.gameObject;
                rightWeapon = PlayerManager.instance.rightSword.transform.parent.gameObject;
                Debug.Log(leftWeapon);
            }
            else
            {
                leftWeapon = PlayerManager.instance.leftWeapon;
                rightWeapon = PlayerManager.instance.rightWeapon;
                leftWeapon.transform.localRotation = Quaternion.Euler(90f, 180f, 0f);
                rightWeapon.transform.localRotation = Quaternion.Euler(90f, 180f, 0f);
            }
        }


    }
}

    
