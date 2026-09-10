namespace GameAscendNamespace;

public class Regeneration : HealOverTimeEffect
{
    public Regeneration(int duration, int healingPerTurn) : base(duration, healingPerTurn)
    {
        Name = "Regeneration";
    }
}