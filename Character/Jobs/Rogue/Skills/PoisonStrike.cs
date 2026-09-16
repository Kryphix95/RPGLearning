namespace GameAscendNamespace;

public class PoisonStrike : Ability
{
    public double PoisonChance { get; set; } = 50; // Generell Chance to hit Poison with this skill.

    public PoisonStrike()
    {
        Name = "PoisonStrike";
        ManaCost = 25;
        Description = "Strikes the Target and has a 50% Chance to Poison the Target for 5 Turns.";
        Cooldown = 3;
    }

    public override void Execute(Character User, List <Character> Targets)
    {
        Character Target = Targets[0];
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            this.StartCooldown();

            DamageResult result = User.NormalAttack();
            result = Target.DamageTaken(result);
            Console.WriteLine("Poison Strike hits " + Target.Name + " for " + result.FinalDamage + " Damage.");

            if (!result.IsDodged && !Target.IsDead)
            {
                bool poisonActivated = Random.Shared.NextDouble() * 100 < Math.Clamp(PoisonChance, 0, 100);

                if (poisonActivated)
                {
                    Console.WriteLine(Target.Name + " has been poisoned!");
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
