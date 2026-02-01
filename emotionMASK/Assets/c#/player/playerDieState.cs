using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDieState : playerState
{
    public playerDieState(player player, playerStateMachine stateMachine, string animBoolName) 
        : base(player, stateMachine, animBoolName)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        playerStateManager.failure = true;
        //SceneManager.LoadScene(0);
    }
    public override void Update()
    {
        base.Update();

        //SceneManager.LoadScene(0);
        
    }
    public override void Exit()
    {
        base.Exit();
    }
}
