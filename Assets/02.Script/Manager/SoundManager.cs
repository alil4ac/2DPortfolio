using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingleTone<SoundManager>
{
    private AudioSource MainThema;

    #region AudioSource

    [SerializeField]
    private AudioSource P_AttackSource;

    [SerializeField]
    private AudioSource EnemyDeadSource;

    [SerializeField]
    private AudioSource EnemyHitSource;

    [SerializeField]
    private AudioSource AirSource;

    [SerializeField]
    private AudioSource BossDeadSource;

    [SerializeField]
    private AudioSource FireBallSource;

    [SerializeField]
    private AudioSource LevelUpSource;

    [SerializeField]
    private AudioSource ThunderSource;

    [SerializeField]
    private AudioSource PlayerDeadSource;

    [SerializeField]
    private AudioSource FireBlueSource;

    [SerializeField]
    private AudioSource IceSource;

    [SerializeField]
    private AudioSource DashSource;

    [SerializeField]
    private AudioSource JumpSource;

    #endregion

    #region IsPlayReady

    public bool P_AttackIsReady = true;

    #endregion

    #region Method

    public void PlayerAttackSound()
    {
        if (P_AttackIsReady)
        {
            P_AttackIsReady = false;

            P_AttackSource.Play();
        }
    }

    public void EnemyDeadSound()
    {
        EnemyDeadSource.Play();
    }

    public void EnemyHitSound()
    {
        EnemyHitSource.Play();
    }

    public void AirSound()
    {
        AirSource.Play();
    }

    public void EnemyDeadSound(int layer)
    {
        if (layer == LayerMask.NameToLayer("Boss"))
        {
            BossDeadSource.Play();
        }
        if (layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyDeadSource.Play();
        }
    }

    public void FireBallSound()
    {
        FireBallSource.Play();
    }
    public void LevelUpSound()
    {
        LevelUpSource.Play();
    }

    public void ThunderSound()
    {
        ThunderSource.Play();
    }

    public void PlayerDeadSound()
    {
        PlayerDeadSource.Play();
    }

    public void FireBlueSound()
    {
        FireBlueSource.Play();
    }

    public void IceSound()
    {
        IceSource.Play();
    }

    public void DashSound()
    {
        DashSource.Play();
    }

    public void JumpSound()
    {
        JumpSource.Play();
    }

    #endregion

    private void Awake()
    {
        if (SoundManager.Instance != null && SoundManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            MainThema = this.GetComponent<AudioSource>();
        }
    }

    public void PasueVolum()
    {
        MainThema.volume = 0.25f;
    }

    public void NormalVolum()
    {
        MainThema.volume = 0.5f;
    }
}
