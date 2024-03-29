using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : IState<PlayerController>
{
    float MovePosX;

    public void Enter(PlayerController Send)
    {
        Send.IsJump = false;

        Send.DoubleJump = false;
    }

    public void Exit(PlayerController Send)
    {

    }

    public void HandleInput(PlayerController Send)
    {

        Send.Anim.SetFloat("Horizontal", CommonCollection.InputX);

        if (CommonCollection.InputX < 0) { Send.transform.localScale = Send.P_Info.SetLeftVec; }

        if (CommonCollection.InputX > 0) { Send.transform.localScale = Send.P_Info.SetRightVec; }

        if (CommonCollection.JumpBtn) { Send.ChangeState(PlayerController.EState.Jump); }

        if (CommonCollection.CrouchBtn) { Send.ChangeState(PlayerController.EState.Crouch); }

        if (CommonCollection.DashBtn) { Send.ChangeState(PlayerController.EState.Dash); }

        if (CommonCollection.AttackBtn) { Send.ChangeState(PlayerController.EState.Attack); }

        if (CommonCollection.KickBtn) { Send.ChangeState(PlayerController.EState.Kick); }

        if (CommonCollection.MagicBtn) { Send.ChangeState(PlayerController.EState.Magic); }

        if (CommonCollection.ChangeMagicQ) { Send.P_Info.ChangeMagic_Q(); }

        if (CommonCollection.ChangeMagicE) { Send.P_Info.ChangeMagic_E(); }
    }

    public void LogicUpdate(PlayerController Send)
    {
        if (!Send.CheckGrounded())
        {
            Send.IsJump = true;

            Send.ChangeState(PlayerController.EState.Jump);
        }
    }

    public void PhysicsUpdate(PlayerController Send)
    {
        MovePosX = CommonCollection.InputX;

        Send.SlopeCheck();

        if (!Send.IsSlope)
        {
            Send.RB.velocity = new Vector2(MovePosX * Send.P_Info.P_Speed, 0f);
        }
        else if (Send.IsSlope)
        {
            Send.RB.velocity = new Vector2(Send.P_Info.P_Speed * Send.SlopeNormalPerp.x * -MovePosX,
                                           Send.P_Info.P_Speed * Send.SlopeNormalPerp.y * -MovePosX);
        }

    }
}
