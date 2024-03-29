using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {
        Send.GetHit();
        Send.RB.velocity = Vector2.zero;
        UIManager.Instance.GetHpBar(Send.P_Info, Send.gameObject.name);
    }

    public void Exit(PlayerController Send)
    {

    }

    public void HandleInput(PlayerController Send)
    {

    }

    public void LogicUpdate(PlayerController Send)
    {
        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt") &&
            Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Send.ChangeState(PlayerController.EState.Movement);
        }
    }

    public void PhysicsUpdate(PlayerController Send)
    {

    }
}
