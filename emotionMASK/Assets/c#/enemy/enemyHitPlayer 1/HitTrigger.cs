using System.Collections;
using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    [Header("命中触发器（如果为空会在子物体中自动查找带 Collider2D 的对象）")]
    public GameObject hitboxObject;

    [Header("可选：自动关闭时长（秒，0 表示不自动关闭，由动画/命中负责关闭）")]
    public float activeDuration = 0f;

    private Coroutine autoDisableCoroutine;

    private void Reset()
    {
        // 默认禁用，确保不会一直开启
        if (hitboxObject != null)
            hitboxObject.SetActive(false);
    }

    private void Awake()
    {
        if (hitboxObject == null)
            FindHitbox();
        if (hitboxObject != null)
            hitboxObject.SetActive(false);
    }

    private void FindHitbox()
    {
        // 在子对象中查找第一个带 Collider2D 的 GameObject
        Collider2D col = GetComponentInChildren<Collider2D>(true);
        if (col != null)
            hitboxObject = col.gameObject;
    }

    /// <summary>
    /// 动画事件可以调用：在攻击关键帧开启触发器
    /// </summary>
    public void ActivateHitbox()
    {
        if (hitboxObject == null)
            FindHitbox();

        if (hitboxObject == null)
        {
            Debug.LogWarning($"{name}: 未找到 hitboxObject，无法激活命中触发器。");
            return;
        }

        hitboxObject.SetActive(true);

        // 如果设置了自动关闭时长，则启动协程在到时关闭（用于备份）
        if (activeDuration > 0f)
        {
            if (autoDisableCoroutine != null) StopCoroutine(autoDisableCoroutine);
            autoDisableCoroutine = StartCoroutine(AutoDisableAfter(activeDuration));
        }
    }

    /// <summary>
    /// 动画事件可以调用：在攻击结束帧关闭触发器
    /// </summary>
    public void DeactivateHitbox()
    {
        if (autoDisableCoroutine != null)
        {
            StopCoroutine(autoDisableCoroutine);
            autoDisableCoroutine = null;
        }

        if (hitboxObject != null)
            hitboxObject.SetActive(false);
    }

    private IEnumerator AutoDisableAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (hitboxObject != null)
            hitboxObject.SetActive(false);
        autoDisableCoroutine = null;
    }

    // 供外部调用（例如子物体命中后想让父控制立即关闭）
    public void OnHitRegistered()
    {
        DeactivateHitbox();
    }
}
