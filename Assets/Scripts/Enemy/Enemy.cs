using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public FiniteStateMachine<Enemy> enemyFSM;

    [SerializeField] [Min(0f)] private float maxHealth;
    public HealthComponent Health
    {
        get; private set;
    }

    private Dictionary<DamageType, float> resistances = new Dictionary<DamageType, float>();
    public Dictionary<DamageType, float> Resistances
    {
        get
        {
            if (resistances == null)
            {
                resistances = new Dictionary<DamageType, float>();
            }
            return resistances;
        }
    }


    //properties for patrolling AI
    public float movementSpeed;
    public float attackRadius = 30f;
    public Transform target;
    internal NavMeshAgent navMesh;
    [SerializeField] public Transform[] destinations;
   
    [SerializeField] internal bool isInRange;
    [SerializeField] internal float timer;
    [SerializeField] internal float maxTime = 0f;

    void Awake()
    {

        navMesh = GetComponent<NavMeshAgent>();
        enemyFSM = new FiniteStateMachine<Enemy>(this);
        enemyFSM.AddState(new EnemyIdleState(enemyFSM, target));
        enemyFSM.AddState(new EnemyAttackState(enemyFSM, target));

        Health = new HealthComponent(maxHealth);
        resistances.Add(DamageType.PHYSICAL, 5f);
        resistances.Add(DamageType.FIRE, -5.0f);
    }

    void Start()
    {
        enemyFSM.SwitchState(typeof(EnemyIdleState));
    }

    public void Update()
    {
        enemyFSM.Update();
    }

    public void FixedUpdate()
    {
    }

    public void TakeDamage(float _damage, DamageType _damageType = DamageType.PHYSICAL)
    {
        Health.Value -= _damage;
    }

    

}
