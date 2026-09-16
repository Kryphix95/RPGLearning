namespace GameAscendNamespace;

public class StatusEffect
{
    public string Name { get; set; } = "";
	public int Duration { get; set; }

	// Wird ausgeführt, wenn der Statuseffekt auf das Ziel angewendet wird
	public virtual void OnApply(Character target) 
	{
	}

	// Wird am Anfang jedes eigenen Turns ausgeführt
	public virtual void OnTurnStart(Character target)
	{
	}

    // Allows the status effect to modify incoming damage.
    public virtual double ModifyIncomingDamage(double damage,DamageType damageType)
    {
        return damage;
    }


    // Wird am Ende jedes eigenen Turns ausgeführt
    public virtual void OnTurnEnd(Character target)
	{
	}

	// Wird ausgeführt, wenn Duration 0 erreicht
	public virtual void OnRemove(Character target)
	{
	}

	public virtual bool PreventsAction => false; 
}	