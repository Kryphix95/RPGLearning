namespace GameAscendNamespace;

public class PoisonStrike : Ability
{
    public double PoisonChance { get; set; } = 50; // Generell Chance to hit Poison with this skill.

    public PoisonStrike()
    {
        Name = "PoisonStrike";
        ManaCost = 25;
        Description = "Strikes the Target and has a 50% Chance to Poison the Target for 5 Turns.";
    }

    public override void Execute(Character User, Character Target)
    {
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            DamageResult result = User.NormalAttack();
            result = Target.DamageTaken(result);
            if (!result.IsDodged && !Target.IsDead)
            {
                bool poisonActivated = Random.Shared.NextDouble() * 100 < Math.Clamp(PoisonChance, 0, 100);

                if (poisonActivated)
                {
                    int poisonDamage = Math.Max(1,(int)Math.Round(Target.MaxHealth * 0.02));
                    Target.ApplyStatusEffect(new Poison(5, poisonDamage));
                }
            }
        }
        else
        {
            Console.WriteLine("You dont have enough Mana to use this...");
        }

    }
}
