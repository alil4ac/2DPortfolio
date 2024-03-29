using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{
    public enum EItem
    {
        RedPotion = 0,
        Purpotion = 1,
        Pie = 2,
        Chicken = 3,
        Coffee = 4,
        Milk = 5,
    }

    private int recoverhp = 0;

    public int RecoverHP { get { return recoverhp; } }

    private int recovermp = 0;

    public int RecoverMP { get { return recovermp; } }

    private bool hpup = false;

    public bool HPUP { get { return hpup; } }

    private bool mpup = false;

    public bool MPUP { get { return mpup; } }

    private bool isdespawn = true;

    public bool IsDespawn { get { return isdespawn; } }

    public void SetItem(string name)
    {
        switch (name)
        {
            case "RedPotion":
                recoverhp = 1000;
                hpup = true;
                isdespawn = false;
                break;

            case "Purpotion":
                recovermp = 1000;
                mpup = true;
                isdespawn = false;
                break;

            case "Pie":
                recoverhp = 50;
                break;

            case "Chicken":
                recoverhp = 100;
                break;

            case "Coffee":
                recovermp = 30;
                break;

            case "Milk":
                recovermp = 50;
                break;
        }
    }
}
