using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 添加UI命名空间

public class playerBeenATKState : playerState
{
    public playerBeenATKState(player player, playerStateMachine stateMachine, string animBoolName) 
        : base(player, stateMachine, animBoolName)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0f, 0f);
        AudioManager.PlayAudio("a");
        player.StartCoroutine(ShowDamageFlash()); // 启动闪烁协程
    }

    private IEnumerator ShowDamageFlash()
    {
        // 创建临时Image
        GameObject damageUI = new GameObject("DamageFlash");
        RectTransform rect = damageUI.AddComponent<RectTransform>();
        Image image = damageUI.AddComponent<Image>();
        
        // 设置为全屏
        rect.SetParent(GameObject.Find("Canvas").transform, false);
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        
        // 设置半透明红色
        image.color = new Color(1f, 0f, 0f, 0.1f);
        
        // 等待0.2秒后销毁
        yield return new WaitForSeconds(0.2f);
        Object.Destroy(damageUI);
    }

    public override void Update()
    {
        base.Update();
        if(playerStateManager.isBeHit == false)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
