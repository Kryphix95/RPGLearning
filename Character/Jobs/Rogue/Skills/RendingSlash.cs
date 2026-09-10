namespace GameAscendNamespace;

public class RendingSlash : Ability
{
    public RendingSlash()
    { 
        Name = "Rending Slash";
        ManaCost = 25;
        Cooldown = 2;
        Description = "You attack the Target with a Normal Attack, and apply a Bleed Effect with a 75% chance to activate.";
    }

    public override void Execute(Character User, Character Target)
    {
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            this.StartCooldown();

            DamageResult result = User.NormalAttack();
            result = Target.DamageTaken(result);
            Console.WriteLine("Rending Slash hits " + Target.Name + " for " + result.FinalDamage + " Damage.");
            if (!result.IsDodged && !Target.IsDead)
            {
                bool bleedActivated = Random.Shared.NextDouble() * 100 < 75; // 75% chance to apply Bleed
                if (bleedActivated)
                {
                    Console.WriteLine(Target.Name + " is bleeding!");
                    int bleedDamage = Math.Max(1, (int)Math.Round(User.PhysicalDamage * 0.5)); // deals 50% of Physical Damage as Bleed Damage for 2 turns
                    Target.ApplyStatusEffect(new Bleed(2, bleedDamage)); // Apply Bleed for 2 turns
                }
            }
        }
        else
        {
            Console.WriteLine("You don't have enough Mana to use this...");
        }
    }
}