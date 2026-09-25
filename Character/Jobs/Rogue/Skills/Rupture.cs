namespace GameAscendNamespace;

public class Rupture : Ability
{
    public Rupture()
    {
        Name = "Rupture";
        ManaCost = 30;
        Cooldown = 5; // Cooldown in turns
        Description = "Performs a Normal Attack. If the Target is Bleeding, the attack is guaranteed to Critical Strike and deals increased Damage and refreshes Bleed.";
    }
    public override void Execute(Character User, List <Character> Targets)
    {
        Character Target = Targets[0];
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            this.StartCooldown();


            Bleed? activeBleed = Target.ActiveEffects.OfType<Bleed>().FirstOrDefault();

            DamageResult result;

            if (activeBleed != null)
            {
                result = User.NormalAttack(100);
                result.RawDamage *= 2;
            }
            else
            {
                result = User.NormalAttack();
            }

            result = Target.DamageTaken(result);

            Console.WriteLine($"Rupture hits {Target.Name} for {result.FinalDamage} Damage. " + $"Crit: {result.IsCrit}, Block: {result.IsBlocked}, Dodge: {result.IsDodged}");

            if (activeBleed != null && !result.IsDodged && !result.TargetDied)
            {
                Target.ApplyStatusEffect(new Bleed(2, activeBleed.DamagePerTurn));
            }
        }
    }
}