using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private FiniteStateMachine<Enemy> enemyFSM;

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
    public Transform target;
    public float attackRadius = 30f;
    private NavMeshAgent navMesh;
    [SerializeField] private Transform[] destinations;
   
    private int currentPoint;
    [SerializeField] private bool isInRange;
    [SerializeField] private float timer;
    [SerializeField] private float maxTime = 0f;

    public Enemy()
    {
        enemyFSM = new FiniteStateMachine<Enemy>(this);
        enemyFSM.AddState(new EnemyIdleState(enemyFSM));
        enemyFSM.AddState(new EnemyAttackState(enemyFSM));


        //enemyFSM.SwitchState(typeof(EnemyIdleState));
    }

    void Awake()
    {
        Health = new HealthComponent(maxHealth);
        resistances.Add(DamageType.PHYSICAL, 5f);
        resistances.Add(DamageType.FIRE, -5.0f);
    }

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        CalculateDistanceToTarget(target);
        enemyFSM.Update();
    }

    public void TakeDamage(float _damage, DamageType _damageType = DamageType.PHYSICAL)
    {
        Health.Value -= _damage;
        Debug.Log("Enemy took damage!");
        Debug.Log("Enemy now has " + Health.Value + " health");
    }

    public void CalculateDistanceToTarget(Transform _target)
    {
        float distanceTo = Vector3.Distance(transform.position, _target.position);
        if(distanceTo <= attackRadius)
        {
            timer += Time.deltaTime;
            if(timer > maxTime)
            {

                isInRange = true;
                transform.LookAt(_target);
                Vector3 moveTo = Vector3.MoveTowards(transform.position, _target.position, 100f);
                navMesh.destination = moveTo;

                //modulair?
                //enemyFSM.SwitchState(typeof(EnemyAttackState));
            }
            
        }
        else if (distanceTo > attackRadius)
        {
            isInRange = false;
            BackToPath();
            enemyFSM.SwitchState(typeof(EnemyIdleState));
        }
    }

    public void BackToPath()
    {
        if(!isInRange && navMesh.remainingDistance < 8.5f)
        {
            int i = currentPoint;
            Vector3 moveTo = Vector3.MoveTowards(transform.position, destinations[i].position, 100f);
            navMesh.destination = moveTo;
            if(navMesh.remainingDistance < 1f)
            { 
                UpdateCurrentpoint();
            }
        }
    }

    public void UpdateCurrentpoint()
    {
        if(currentPoint == destinations.Length - 1)
        {
            currentPoint = 0;
        }
        else
        {
             currentPoint++;
        }
    }
}
