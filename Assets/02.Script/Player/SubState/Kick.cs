using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Kick : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {
        SoundManager.Instance.PlayerAttackSound();
    }

    public void Exit(PlayerController Send)
    {

    }

    public void HandleInput(PlayerController Send)
    {

    }

    public void LogicUpdate(PlayerController Send)
    {

        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("DiveKick") && 
            Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0f)
        {
            if (Send.CheckGrounded())
            {
                Send.ChangeState(PlayerController.EState.Movement);
            }
        }

    }

    public void PhysicsUpdate(PlayerController Send)
    {
        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("DiveKick"))
        {
            if (Send.IsFlip) Send.RB.velocity = new Vector2(-8f, Send.RB.velocity.y);

            if (!Send.IsFlip) Send.RB.velocity = new Vector2(8f, Send.RB.velocity.y);
        }
    }
}
