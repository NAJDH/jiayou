using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoveBar : MonoBehaviour
{
    public Image LoveValue;
    public Image LoveBarFrame;
    public int maxKills = 20;
    private float currentLoveValue;

    private void Update()
    {
        CalculateTheLoveValue();
        LoveValue.fillAmount = currentLoveValue;
    }


    private void CalculateTheLoveValue()
    {
        currentLoveValue = (float)EnemyManager.EnemyKilled / (float)maxKills;
    }
}
