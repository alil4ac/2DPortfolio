using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Trace : IState<EnemyController>
{
    float targetPos;
    Vector2 targetVec;
    public void Enter(EnemyController Send)
    {

    }

    public void Exit(EnemyController Send)
    {

    }

    public void HandleInput(EnemyController Send)
    {
        if (Send.RB.velocity.x < -0.1f)
        {
            Send.transform.localScale = Send.E_Info.LocalScaleL;

            Send.Anim.SetFloat("Horizontal", -1f);
        }

        if (Send.RB.velocity.x > 0.1f)
        {
            Send.transform.localScale = Send.E_Info.LocalScaleR;

            Send.Anim.SetFloat("Horizontal", 1f);
        }

        if (Send.RB.velocity.x == 0f)
        {
            Send.Anim.SetFloat("Horizontal", 0f);
        }
    }

    public void LogicUpdate(EnemyController Send)
    {
        targetPos = Vector3.Distance(GameManager.Instance.PlayerTr.position, Send.transform.position);

        targetVec = GameManager.Instance.PlayerTr.position - Send.transform.position;
        if(Send.E_Info.E_AttackRange != 0 && Send.E_Info.E_AttackRange > targetPos && Send.E_Info.AttackTime <= 0)
        {
            if (Send.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (targetVec.x > 0)
                {
                    Send.transform.localScale = Send.E_Info.LocalScaleR;

                    if (Send.E_Info.UseMagic) { Send.ChangeState(EnemyController.EState.E_Magic); }

                    else { Send.ChangeState(EnemyController.EState.E_Attack); }
                }

                if (targetVec.x < 0)
                {
                    Send.transform.localScale = Send.E_Info.LocalScaleL;

                    if (Send.E_Info.UseMagic) { Send.ChangeState(EnemyController.EState.E_Magic); } 

                    else { Send.ChangeState(EnemyController.EState.E_Attack); } 
                }
            }
            else if (Send.gameObject.layer == LayerMask.NameToLayer("Boss"))
            {
                Send.ChangeState(EnemyController.EState.E_AI);
            }
            else { return; }
        }
    }

    public void PhysicsUpdate(EnemyController Send)
    {
        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            Send.RB.velocity = new Vector2(targetVec.normalized.x * Send.E_Info.E_MoveSpeed, Send.RB.velocity.y);
        }
    }
}
