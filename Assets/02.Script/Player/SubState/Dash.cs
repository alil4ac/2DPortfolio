using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {
        Send.gameObject.layer = LayerMask.NameToLayer("Ignore");

        switch (Send.IsFlip)
        {
            case true:
                Send.RB.velocity = -Send.P_Info.P_DashPose;
                break;

            case false:
                Send.RB.velocity = Send.P_Info.P_DashPose;
                break;
        }
        SoundManager.Instance.DashSound();
    }

    public void Exit(PlayerController Send)
    {
        Send.Render.color = new Color(1, 1, 1, 1);

        if(Send.P_Info.Inveincible_Time <= 0)
        {
            Send.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    public void HandleInput(PlayerController Send)
    {
        if (CommonCollection.AttackBtn) { Send.ChangeState(PlayerController.EState.Attack); }
    }

    public void LogicUpdate(PlayerController Send)
    {
        Send.Render.color = Color.Lerp(new Color(1, 1, 1, 1),
                                       new Color(0, 1, 1, 1),
                                       Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    public void PhysicsUpdate(PlayerController Send)
    {

    }
}
