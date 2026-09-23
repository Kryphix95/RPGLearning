namespace GameAscendNamespace;

public abstract class DamageOverTimeEffect : StatusEffect
{
	public int DamagePerTurn { get; set; }

	protected DamageOverTimeEffect(int duration, int damagePerTurn)
	{
		Duration = duration;
		DamagePerTurn = damagePerTurn;
	}

	public override void OnTurnEnd(Character target)
	{
		target.Health -= DamagePerTurn;

		if (target.Health <= 0)
		{ 
			target.Health = 0;
		}
		
		Console.WriteLine($"{target.Name} takes {DamagePerTurn} damage from {Name}! " + $"HP: {target.Health}/{target.MaxHealth}");
	}
}