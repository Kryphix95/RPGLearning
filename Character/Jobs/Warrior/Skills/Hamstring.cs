namespace GameAscendNamespace;

public class Hamstring : Ability
{
    public Hamstring()
    {
        Name = "Hamstring";
        Description = "A Normal attack that deals damage and reduces the target's speed for 2 turns.";
        ManaCost = 20;
        Cooldown = 5;
    }
    public override void Execute(Character User, List <Character> Targets)
    {
        Character Target = Targets[0];
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            this.StartCooldown();
            DamageResult result = User.NormalAttack();
            result.RawDamage = (int)Math.Round(result.RawDamage * 0.75); // Hamstring deals 75% of normal attack damage
            result = Target.DamageTaken(result);
            Console.WriteLine($"{User.Name} uses {Name} on {Target.Name}, dealing {result.FinalDamage} damage!");
            if (!result.IsDodged && !Target.IsDead)
            {
                Console.WriteLine($"{Target.Name}'s speed is reduced for 2 turns!");
                Target.ApplyStatusEffect(new SlowEffect(2, 3));
            }
        }
        else
        {
            Console.WriteLine("You don't have enough Mana to use this ability.");
        }
    }
}