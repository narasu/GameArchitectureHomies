using System.Collections.Generic;
public interface IDamageable
{
    Dictionary<DamageType, float> Resistances { get; }
    HealthComponent Health { get; }
    void TakeDamage(float _damage, DamageType _damageType = DamageType.PHYSICAL);
}