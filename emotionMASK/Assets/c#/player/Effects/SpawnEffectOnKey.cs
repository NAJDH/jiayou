using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffectOnKey : MonoBehaviour
{
    [Header("按键对应的特效预制体：索引0 -> 键1, 索引1 -> 键2 ...")]
    public GameObject[] effectPrefabs = new GameObject[4];

    [Header("生成位置偏移（相对于当前受控形态）")]
    public Vector3 spawnOffset = Vector3.zero;

    [Header("生成后是否把特效设为场景根对象，还是作为此物体的子物体")]
    public bool parentToThis = false;

    [Header("当找不到 PlayerFormManager 或当前形态时的回退位置")]
    public Vector3 fallbackPosition = Vector3.zero;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SpawnEffect(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) SpawnEffect(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) SpawnEffect(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) SpawnEffect(4);
    }

    private void SpawnEffect(int keyIndex)
    {
        if (effectPrefabs == null || effectPrefabs.Length < keyIndex)
        {
            Debug.LogWarning($"SpawnEffectOnKey: 没有为键 {keyIndex} 配置预制体");
            return;
        }

        GameObject prefab = effectPrefabs[keyIndex - 1];
        if (prefab == null)
        {
            Debug.LogWarning($"SpawnEffectOnKey: 键 {keyIndex} 的预制体为空");
            return;
        }

        Vector3 spawnPos = fallbackPosition;

        var mgr = PlayerFormManager.playerForm;
        if (mgr != null)
        {
            // 使用 PlayerFormManager 中公开的 form1..form4 与 currentFormIndex 获取当前受控形态位置
            GameObject currentForm = null;
            switch (mgr.currentFormIndex)
            {
                case 1: currentForm = mgr.form1; break;
                case 2: currentForm = mgr.form2; break;
                case 3: currentForm = mgr.form3; break;
                case 4: currentForm = mgr.form4; break;
            }

            if (currentForm != null)
                spawnPos = currentForm.transform.position + spawnOffset;
        }

        GameObject instance = Instantiate(prefab, spawnPos, Quaternion.identity);

        if (parentToThis)
            instance.transform.SetParent(transform, true);

        // 如果实例没有 EffectAutoDestroy 组件，运行时自动添加一个默认的，保证会被销毁
        if (instance.GetComponent<EffectAutoDestroy>() == null)
        {
            instance.AddComponent<EffectAutoDestroy>();
        }
    }
}