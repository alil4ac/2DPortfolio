using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : IState<PlayerController>
{
    Vector3 SpawnPos;

    public void Enter(PlayerController Send)
    {
        SpawnPos = Send.M_Info.GetSpawnVec(Send.P_Info.EquipMagic, Send.IsFlip, Send.transform.position);

        //SpawnPos += Send.transform.position;
    }

    public void Exit(PlayerController Send)
    {
        switch (Send.P_Info.EquipMagic)
        {
            case 0:
                break;

            case 1:
                GameObject obj = PoolManager.Instance.Spawn(Magic_Info.E_pMagic.Air.ToString(), SpawnPos, Quaternion.identity);

                obj.GetComponent<Air>().Direasd(Send.IsFlip);
                SoundManager.Instance.AirSound();
                break;

            case 2:
                for(int i = 0; i< 5; i++)
                {
                    Vector3 newSpawnPos = new Vector3(((float)i/2.5f) + SpawnPos.x, SpawnPos.y, SpawnPos.z);
                    GameObject obj2 = PoolManager.Instance.Spawn(Magic_Info.E_pMagic.Ice.ToString(), newSpawnPos, Quaternion.identity);

                    obj2.GetComponent<Ice>().Direasd(Send.IsFlip);
                }
                SoundManager.Instance.IceSound();
                break;

            case 3:
                GameObject obj3 = PoolManager.Instance.Spawn(Magic_Info.E_pMagic.Thunder.ToString(), SpawnPos, Quaternion.identity);

                obj3.GetComponent<Thunder>().Direasd(Send.IsFlip);
                SoundManager.Instance.ThunderSound();
                break;
        }
    }

    public void HandleInput(PlayerController Send)
    {

    }

    public void LogicUpdate(PlayerController Send)
    {

    }

    public void PhysicsUpdate(PlayerController Send)
    {

    }
}
