using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJump : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {
        if (Send.IsJump == true && Send.DoubleJump == false)
        {
            Send.RB.velocity = Vector2.up * Send.P_Info.JumpPose;

            Send.DoubleJump = true;

            SoundManager.Instance.JumpSound();
        }
    }

    public void Exit(PlayerController Send)
    {

    }

    public void HandleInput(PlayerController Send)
    {
        if (CommonCollection.AttackBtn) { Send.ChangeState(PlayerController.EState.Attack); }

        if (CommonCollection.KickBtn) { Send.ChangeState(PlayerController.EState.Kick); }
            
        if (CommonCollection.InputX < 0) { Send.transform.localScale = Send.P_Info.SetLeftVec; }

        if (CommonCollection.InputX > 0) { Send.transform.localScale = Send.P_Info.SetRightVec; }
    }

    public void LogicUpdate(PlayerController Send)
    {
        if (Send.RB.velocity.y < 0 && Send.CheckGrounded())
        {
            Send.ChangeState(PlayerController.EState.Movement);
        }
        else if(Send.RB.velocity.y == 0.0f)
        {
            Send.ChangeState(PlayerController.EState.Movement);
        }
        else { return; }
    }

    public void PhysicsUpdate(PlayerController Send)
    {
        Send.RB.velocity = new Vector2(CommonCollection.InputX * Send.P_Info.P_Speed, Send.RB.velocity.y);
    }
}
