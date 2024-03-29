using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Attack : IState<EnemyController>
{
    public void Enter(EnemyController Send)
    {
        Send.RB.velocity = Vector2.zero;
    }

    public void Exit(EnemyController Send)
    {
        Send.E_Info.ResetAttackTime();
    }

    public void HandleInput(EnemyController Send)
    {

    }

    public void LogicUpdate(EnemyController Send)
    {
        if(Send.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            if(Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
               Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                Send.ChangeState(EnemyController.EState.E_AI);
            }
        }
    }

    public void PhysicsUpdate(EnemyController Send)
    {

    }
}
