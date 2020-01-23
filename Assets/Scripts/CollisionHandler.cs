using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] GameObject deathFX;

    private void OnTriggerEnter(Collider other)
    {

        StartDeathSequence();

    }

    private void StartDeathSequence()
    {
        print("Player dying");

        SendMessage("OnPlayerDeath");
        deathFX.SetActive(true);
        Invoke("reloadLevel", loadLevelDelay);
    }

    private void reloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
