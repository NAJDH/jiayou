using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diban : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Spawn")]
    // 触发边缘距离阈值
    public float edgeThreshold = 0.5f;
    // 生成位置 = 物体长度 * 该倍数
    public float spawnOffsetMultiplier = 2f;

    [Header("Despawn")]
    // 超过 3 倍长度就销毁
    public float despawnMultiplier = 3f;

    private bool spawnedRight = false;
    private bool spawnedLeft = false;
    private float length = 1f;

    private void Awake()
    {
        length = CalculateLength();
    }

    private void Update()
    {
        if (player == null) return;

        float rightEdge = transform.position.x + length * 0.5f;
        float leftEdge = transform.position.x - length * 0.5f;

        // 新增：玩家必须在本段范围内才允许生成
        bool playerInside = player.position.x >= leftEdge && player.position.x <= rightEdge;

        if (!spawnedRight && playerInside && player.position.x >= rightEdge - edgeThreshold)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x += length * spawnOffsetMultiplier;

            GameObject clone = Instantiate(gameObject, spawnPos, transform.rotation, transform.parent);
            diban cloneScript = clone.GetComponent<diban>();
            if (cloneScript != null)
            {
                cloneScript.player = player;
            }

            spawnedRight = true;
        }

        if (!spawnedLeft && playerInside && player.position.x <= leftEdge + edgeThreshold)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x -= length * spawnOffsetMultiplier;

            GameObject clone = Instantiate(gameObject, spawnPos, transform.rotation, transform.parent);
            diban cloneScript = clone.GetComponent<diban>();
            if (cloneScript != null)
            {
                cloneScript.player = player;
            }

            spawnedLeft = true;
        }

        float distance = Mathf.Abs(player.position.x - transform.position.x);
        if (distance > length * despawnMultiplier)
        {
            Destroy(gameObject);
        }
    }

    private float CalculateLength()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers == null || renderers.Length == 0) return 1f;

        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }
        return bounds.size.x;
    }
}
