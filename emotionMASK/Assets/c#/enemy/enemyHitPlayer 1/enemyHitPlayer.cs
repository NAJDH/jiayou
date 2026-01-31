using UnityEngine;

//public class EnemyHitbox : MonoBehaviour
//{
//    [Header("伤害设置")]
//    public int damageAmount = 10; // 敌人造成的伤害值

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        // 1. 检查是不是碰到了玩家
//        // ⚠️ 确保你的玩家物体 Tag 设置为 "Player"
//        Debug.Log("敌人攻击判定触发！！！！！！！！！！！！！！！");
//        if (other.CompareTag("Player"))
//        {
//            playerStateManager.enemyHitPlayerDamage(damageAmount);
//            Debug.Log("敌人命中玩家，造成 " + damageAmount + " 点伤害！！！！！！！！！！！！！！！");
//        }
//    }
//}


public class EnemyHitbox : MonoBehaviour
{
    [Header("伤害设置")]
    public int damageAmount = 10; // 敌人造成的伤害值

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 确保玩家 Tag 为 "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("敌人攻击判定触发，造成 " + damageAmount + " 点伤害");
            playerStateManager.enemyHitPlayerDamage(damageAmount);

            // 命中后自动关闭当前命中判定物体，避免重复命中
            // 如果父物体上挂有 HitTrigger，希望由父控制关闭，也可调用父的 OnHitRegistered
            var parentTrigger = GetComponentInParent<HitTrigger>();
            if (parentTrigger != null)
            {
                parentTrigger.OnHitRegistered();
            }
            else
            {
                // 没有 HitTrigger 的情况下，直接把自己关掉
                gameObject.SetActive(false);
            }
        }
    }
}