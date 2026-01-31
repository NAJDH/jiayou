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

    private bool spawned = false;
    private float length = 1f;

    private void Awake()
    {
        length = CalculateLength();
    }

    private void Update()
    {
        if (spawned || player == null) return;

        // 当前物体右边缘
        float rightEdge = transform.position.x + length * 0.5f;

        // 玩家接近右边缘时生成
        if (player.position.x >= rightEdge - edgeThreshold)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x += length * spawnOffsetMultiplier;

            GameObject clone = Instantiate(gameObject, spawnPos, transform.rotation, transform.parent);

            // 防止克隆体立刻再次生成
            diban cloneScript = clone.GetComponent<diban>();
            if (cloneScript != null)
            {
                cloneScript.spawned = false;
                cloneScript.player = player;
            }

            spawned = true;
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
