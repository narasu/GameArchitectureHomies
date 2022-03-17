using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IControllable
{
    [SerializeField] [Min(0f)] private float maxHealth;
    [SerializeField] [Min(0f)] private float speed;
    private CharacterController charCtrl;
    private Inventory inventory;

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

    public InputManager pInputManager 
    { 
        get; private set; 
    }

    void Awake()
    {
        Health = new HealthComponent(maxHealth);
        charCtrl = GetComponent<CharacterController>();
        FindInputManager();
        pInputManager.BindVector2("Horizontal", "Vertical", new CommandPlayerMove(charCtrl, speed));
        inventory = GetComponent<Inventory>();
        IEquipable[] weapons = GetComponentsInChildren<IEquipable>(true);
        inventory.AddItems(weapons);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    IEquipable equipable = collision.gameObject.GetComponent<IEquipable>();
    //    if (equipable != null)
    //    {
    //        inventory.AddItems(equipable);
    //    }
    //}

    public void TakeDamage(float _damage, DamageType _damageType = DamageType.PHYSICAL)
    {
        Health.Value -= _damage;
        EventManager.Invoke(EventType.PLAYER_DAMAGED);
    }

    public void FindInputManager()
    {
        pInputManager = FindObjectOfType<InputManager>();
    }
}
