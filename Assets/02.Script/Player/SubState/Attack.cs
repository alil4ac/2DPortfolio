using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {
        if (!Send.IsJump)
        {
            Send.RB.velocity = Vector2.zero;
            Send.RB.sharedMaterial = Send.FullFriction;
        }
        SoundManager.Instance.PlayerAttackSound();
    }

    public void Exit(PlayerController Send)
    {
        SoundManager.Instance.P_AttackIsReady = true;
    }

    public void HandleInput(PlayerController Send)
    {

    }

    public void LogicUpdate(PlayerController Send)
    {
        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Fall") &&
            Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            if (Send.CheckGrounded())
            {
                Send.ChangeState(PlayerController.EState.Movement);
            }
            else { Send.ChangeState(PlayerController.EState.Jump); }
        }

        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch_Attack") &&
            Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            if (Input.GetKey(KeyCode.S)) Send.ChangeState(PlayerController.EState.Crouch);

            else Send.ChangeState(PlayerController.EState.Movement);
        }

        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Standing") &&
            Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Send.ChangeState(PlayerController.EState.Movement);
        }
    }

    public void PhysicsUpdate(PlayerController Send)
    {

    }
}
