using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {
        Send.Anim.SetTrigger("Dead");
        SoundManager.Instance.PlayerDeadSound();
    }

    public void Exit(PlayerController Send)
    {
        UIManager.Instance.SetGameOver();
    }

    public void HandleInput(PlayerController Send)
    {

    }

    public void LogicUpdate(PlayerController Send)
    {
        if(Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Death") &&
           Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Send.ChangeState(PlayerController.EState.Movement);
        }
    }

    public void PhysicsUpdate(PlayerController Send)
    {

    }
}
