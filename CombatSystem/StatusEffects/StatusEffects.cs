namespace GameAscendNamespace;

public class StatusEffect
{
    public string Name { get; set; } = "";
	public int Duration { get; set; }

    // Called when the status effect is applied to the target
    public virtual void OnApply(Character target) 
	{
	}

    // Called at the start of the target's turn
    public virtual void OnTurnStart(Character target)
	{
	}

    // Allows the status effect to modify incoming damage
    public virtual double ModifyIncomingDamage(double damage, DamageType damageType)
    {
        return damage;
    }


    // Called at the end of the target's turn
    public virtual void OnTurnEnd(Character target)
	{
	}

    // Called when the effect duration reaches 0
    public virtual void OnRemove(Character target)
	{
	}

	public virtual bool PreventsAction => false; 
}	