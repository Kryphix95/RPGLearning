namespace GameAscendNamespace;

public class IronGuardEffect : StatusEffect
{

    public IronGuardEffect(int duration)
    {
        Name = "Iron Guard";
        Duration = duration;
    }

    // Reduces incoming Physical damage by 30%
    public override double ModifyIncomingDamage(double damage, DamageType damageType)
    {
       if (damageType == DamageType.Physical)
       {
          return damage * 0.7;
       }

        return damage;
    }
}
