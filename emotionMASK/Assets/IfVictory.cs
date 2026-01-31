using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfVictory : MonoBehaviour
{
    public int enemyKillMax = 20; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyManager.EnemyKilled == enemyKillMax)
        {
            Debug.Log("Victory!");
        }
    }
}
