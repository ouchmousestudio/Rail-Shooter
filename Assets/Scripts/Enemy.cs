using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionFX;
    [SerializeField] Transform parent;

    [SerializeField] int scorePerHit = 13;
    [SerializeField] int health = 2;

    ScoreTracker scoreTracker;

    // Start is called before the first frame update
    void Start()
    {
        AddBoxCollider();
        scoreTracker = FindObjectOfType<ScoreTracker>();
    }

    private void AddBoxCollider()
    {
        Collider enemyBoxCollider = gameObject.AddComponent<BoxCollider>();
        enemyBoxCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (health <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        scoreTracker.ScoreHit(scorePerHit);
        //todo consider hit FX
        health--;
    }

    private void KillEnemy()
    {
        GameObject deathFX = Instantiate(explosionFX, transform.position, Quaternion.identity);
        deathFX.transform.parent = parent;
        Destroy(gameObject);
    }
}
