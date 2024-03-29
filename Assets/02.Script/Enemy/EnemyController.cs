using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Variables

    private Animator _anim;

    public Animator Anim { get { return _anim; } set { _anim = value; } }

    private SpriteRenderer _render;

    public SpriteRenderer Render { get { return _render; } set { _render = value; } }

    private Rigidbody2D _rb;

    public Rigidbody2D RB { get { return _rb; } set { _rb = value; } }

    private int stateindex = 0;

    private Transform magicpos;

    public Transform MagciPos { get { return magicpos; } }


    [System.NonSerialized]
    public BossPattern Pattern;


    [SerializeField]
    private Transform[] patrolpos;

    public Transform[] Patrolpos { get { return patrolpos; } }

    #endregion

    #region Property

    [System.NonSerialized]
    public EnemyInfo E_Info;

    public enum EState
    {
        E_AI = 0,
        E_Patrol = 1,
        E_Trace = 2,
        E_Attack = 3,
        E_Magic = 4,
        E_Dead = 5,
    }

    private StateMachine<EnemyController> statemachine;

    private Dictionary<EState, IState<EnemyController>> m_state = new Dictionary<EState, IState<EnemyController>>();


    [SerializeField]
    private GameObject _attackcol;

    public GameObject AttackCol { get { return _attackcol; } set { _attackcol = value; } }
    #endregion

    #region Method

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CommonCollection.AndLayerToLayerMask(collision.gameObject.layer, CommonCollection.L_PAttack))
        {
            if (collision.GetComponentInParent<PlayerController>() == null)
            {
                Magic_Info m_info = collision.GetComponent<Magic_Info>();

                E_Info.GetDmg(m_info);
                Debug.Log(E_Info.Health);

                _isdead(true);
            }
            else if(collision.GetComponentInParent<PlayerController>() != null)
            {
                PlayerInfo info = collision.GetComponentInParent<PlayerController>().P_Info;

                E_Info.GetDmg(info);
                Debug.Log(E_Info.Health);

                _isdead(false);
            }
            else { return; }
        }
    }
    public void ChangeState(EState estate)
    {
        stateindex = (int)estate;

        _anim.SetInteger("E_Type", stateindex);

        statemachine.ChangeState(m_state[estate]);

        Debug.Log(this.gameObject.name + " Has Change Next State -> : " + estate);
    }

    private void _isdead(bool ismagic = false)
    {
        if (this.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            UIManager.Instance.GetHpBar(E_Info, this.gameObject.name);
        }
        if (E_Info.Health <= 0)
        {
            CameraManager.Instance.SetCameraShake();
            SoundManager.Instance.EnemyDeadSound(this.gameObject.layer);
            ChangeState(EState.E_Dead);
        }
        else
        {
            SoundManager.Instance.EnemyHitSound();  
            if (ismagic)
            {
                StartCoroutine(HitColor(true));

            }
            else 
            {
                StartCoroutine(HitColor(false));
            }
        }
    }

    private IEnumerator HitColor(bool ismagic = false)
    {
        _render.color = new Color(1, 0, 0, 1);

        if(!ismagic) { UnityEngine.Time.timeScale = 0; }

        yield return new WaitForSecondsRealtime(0.15f);

        _render.color = new Color(1, 1, 1, 1);

        if(!ismagic){ UnityEngine.Time.timeScale = 1; }

        yield break;
    }

    public void Init()
    {
        E_Info = this.GetComponent<EnemyInfo>();

        E_Info.SetInfo(this.gameObject.name);

        if (E_Info.UseMagic)
        {
            magicpos = this.GetComponentInChildren<Transform>();
        }

        if (this.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Pattern = this.GetComponent<BossPattern>();
        }
    }
    private void OnEnable()
    {
        E_Info.SetInfo(this.gameObject.name);

        if (E_Info.UseMagic)
        {
            magicpos = this.GetComponentInChildren<Transform>();
        }
    }
    #endregion

    private void Awake()
    {
        Init();

        _anim = this.GetComponent<Animator>();

        _rb = this.GetComponent<Rigidbody2D>();

        _render = this.GetComponent<SpriteRenderer>();

        m_state.Add(EState.E_AI, new E_AI());

        m_state.Add(EState.E_Patrol, new E_Patrol());

        m_state.Add(EState.E_Trace, new E_Trace());

        m_state.Add(EState.E_Attack, new E_Attack());

        m_state.Add(EState.E_Magic, new E_Magic());

        m_state.Add(EState.E_Dead, new E_Dead());

        statemachine = new StateMachine<EnemyController>(this, m_state[EState.E_AI]);

    }

    private void Update()
    {
        statemachine.HandleInput();

        statemachine.LogicUpdate();

        E_Info.SetAttackTime();

    }

    private void FixedUpdate()
    {
        statemachine.PhysicsUpdate();
    }
}
