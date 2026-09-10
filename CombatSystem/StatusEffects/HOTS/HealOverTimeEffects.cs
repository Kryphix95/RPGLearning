namespace GameAscendNamespace;

public abstract class HealOverTimeEffect : StatusEffect
{
    public int HealPerTurn { get; set; } = 0;

    protected HealOverTimeEffect(int duration, int healPerTurn)
    {
        Duration = duration;
        HealPerTurn = healPerTurn;
    }
    public override void OnTurnStart(Character target)
    {
        target.Health += HealPerTurn;
        if (target.Health > target.MaxHealth)
        {
            target.Health = target.MaxHealth;
        }
        Console.WriteLine($"{target.Name} heals {HealPerTurn} HP from {Name}! " + $"HP: {target.Health}/{target.MaxHealth}");
    }
}

