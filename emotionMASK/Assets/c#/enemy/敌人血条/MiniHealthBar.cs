using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHealthBar : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    private void OnEnable()
    {
        enemy.OnFlipped += HandleFlip;
    }

    private void HandleFlip() => transform.rotation = Quaternion.identity;
}
