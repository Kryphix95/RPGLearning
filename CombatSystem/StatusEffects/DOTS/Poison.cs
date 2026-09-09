namespace GameAscendNamespace;

public class Poison : DamageOverTimeEffect
{
    public Poison(int duration, int damagePerTurn) : base(duration, damagePerTurn)
    {
        Name = "Poison";
    }
}