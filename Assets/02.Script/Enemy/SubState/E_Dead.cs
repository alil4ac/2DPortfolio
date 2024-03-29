using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Dead : IState<EnemyController>
{
    public void Enter(EnemyController Send)
    {
        Send.RB.velocity = Vector2.zero;

        Send.Anim.SetTrigger("Dead");

        Send.gameObject.layer = LayerMask.NameToLayer("Ignore");

        Send.E_Info.GetExp(GameManager.Instance.PlayerTr.GetComponent<PlayerController>().P_Info);
    }

    public void Exit(EnemyController Send)
    {
        //Send.gameObject.layer = LayerMask.NameToLayer("Enemy");

        UIManager.Instance.EndBossHP(Send.gameObject.name);

        PoolManager.Instance.Despawn(Send.gameObject);
    }

    public void HandleInput(EnemyController Send)
    {

    }

    public void LogicUpdate(EnemyController Send)
    {
        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            Send.Anim.SetInteger("E_Type", 99);
        }

        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && 
           Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Send.ChangeState(EnemyController.EState.E_AI);
        }
    }

    public void PhysicsUpdate(EnemyController Send)
    {

    }
}
