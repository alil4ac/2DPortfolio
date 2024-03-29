using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Magic : IState<EnemyController>
{
    float castingtime = 2f;
    Vector3 targetpos;
    Vector3 dir;
    float angle;
    int idx;
    public void Enter(EnemyController Send)
    {
        castingtime = 2f;

        Send.RB.velocity = Vector2.zero;

        targetpos = new Vector3();
        dir = new Vector3();
        
    }

    public void Exit(EnemyController Send)
    {

        Send.E_Info.ResetAttackTime();
        switch (Send.gameObject.name)
        {
            case "Demon":
                targetpos = GameManager.Instance.PlayerTr.position;

                dir = targetpos - Send.AttackCol.transform.position;

                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                PoolManager.Instance.Spawn("Fireball", Send.AttackCol.transform.position,
                                           Quaternion.AngleAxis(angle + 180, Vector3.forward));
                SoundManager.Instance.FireBallSound();
                break;

            case "Phantom":
                for(int i = 0; i < 5; i++)
                {
                    float posX = Random.Range(-2, 3);
                    Vector3 SpawnPos = new Vector3(Send.transform.position.x + posX, Send.transform.position.y - 0.5f, 1);
                    PoolManager.Instance.Spawn("Skeleton", SpawnPos, Quaternion.identity);
                }
                break;

            case "Ghost":
                targetpos = GameManager.Instance.PlayerTr.position;

                dir = targetpos - Send.AttackCol.transform.position;

                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                PoolManager.Instance.Spawn("FireBlue", Send.AttackCol.transform.position,
                                           Quaternion.AngleAxis(angle + 0, Vector3.forward));

                SoundManager.Instance.FireBlueSound();
                break;

            case "Angel":
                idx = Random.Range(0, 2);
                switch (idx)
                {
                    case 0:
                        for (int i = -2; i < 3; i++)
                        {
                            targetpos = new Vector3(GameManager.Instance.PlayerTr.position.x + i, -1.25f, 0);

                            PoolManager.Instance.Spawn("E_Ice", targetpos, Quaternion.identity);
                        }
                        SoundManager.Instance.IceSound();
                        break;
                    case 1:
                        for (int i = -2; i < 3; i++)
                        {
                            targetpos = new Vector3(GameManager.Instance.PlayerTr.position.x + i, -1.25f, 0);

                            PoolManager.Instance.Spawn("E_Thunder", targetpos, Quaternion.identity);
                        }
                        SoundManager.Instance.ThunderSound();
                        break;
                }
                break;

            case "GhostWolf":
                idx = Random.Range(3, 6);
                for(int i = 0; i < idx; i++)
                {
                    if (Send.transform.localScale.x > 0)
                    {
                        targetpos = new Vector3(Send.transform.position.x + 2f, Send.transform.position.y - 0.75f, 0);

                        GameObject obj = PoolManager.Instance.Spawn("E_Air", targetpos, Quaternion.identity);
                        obj.GetComponent<E_Air>().Direasd(true, Random.Range(3f, 6f));
                    }
                    else if (Send.transform.localScale.x < 0)
                    {
                        targetpos = new Vector3(Send.transform.position.x - 2f, Send.transform.position.y - 0.75f, 0);

                        GameObject obj = PoolManager.Instance.Spawn("E_Air", targetpos, Quaternion.identity);
                        obj.GetComponent<E_Air>().Direasd(false, Random.Range(3f, 6f));
                    }
                    else { return; }
                }
                SoundManager.Instance.AirSound();
                break;
        }
    }

    public void HandleInput(EnemyController Send)
    {

    }

    public void LogicUpdate(EnemyController Send)
    {
        if (Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Casting") &&
           Send.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Send.ChangeState(EnemyController.EState.E_AI);
        }
        else if (castingtime <= 0)
        {
            Send.ChangeState(EnemyController.EState.E_AI);
        }
    }

    public void PhysicsUpdate(EnemyController Send)
    {
        castingtime -= Time.deltaTime;
    }
}
