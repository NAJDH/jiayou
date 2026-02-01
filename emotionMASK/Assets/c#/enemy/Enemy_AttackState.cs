using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    // 全局冷却（秒），和下次允许攻击的时间点（全局）
    public static float globalAttackCooldown = 2f;
    public static float nextAllowedAttackTime = 0f;

    public Enemy_AttackState(Enemy enemybase, EnemyStateMachine stateMachine, string animBoolName) : base(enemybase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        enemybase.SetZeroVelocity();
        // 记录下一次允许攻击的时间（全局）
        nextAllowedAttackTime = Time.time + globalAttackCooldown;
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (movementOver)
            stateMachine.ChangeState(enemybase.battleState);
    }
}
