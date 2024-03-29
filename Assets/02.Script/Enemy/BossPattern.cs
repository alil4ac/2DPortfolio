using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    [SerializeField]
    private List<EnemyController.EState> eStates = new();

    private int index = 0;

    private void Awake()
    {

    }

    public EnemyController.EState CallPattern()
    {
        return eStates[index];
    }

    public void SetValue()
    {
        index++;
        if(index == eStates.Count)
        {
            index = 0;
        }
    }
}
