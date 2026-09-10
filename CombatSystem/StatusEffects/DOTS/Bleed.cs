namespace GameAscendNamespace;

public class Bleed : DamageOverTimeEffect
{
    public Bleed(int duration, int damagePerTurn) : base(duration, damagePerTurn)
    {
        Name = "Bleed";
    }
}