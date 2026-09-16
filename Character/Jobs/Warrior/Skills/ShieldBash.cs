namespace GameAscendNamespace;

public class ShieldBash : Ability
{
    public ShieldBash()
    {
        Name = "Shield Bash";
        Description = "Bashes the enemy with your shield, dealing damage and stunning them for 1 turn.";
        ManaCost = 15;
        Cooldown = 4;
    }
    public override void Execute(Character User, List <Character> Targets)
    {
        Character Target = Targets[0];
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            this.StartCooldown();
            DamageResult result = User.NormalAttack();
            result.RawDamage = (int)Math.Round(result.RawDamage * 0.8); // Shield Bash deals 80% of normal attack damage
            result = Target.DamageTaken(result);
            Console.WriteLine($"{User.Name} uses {Name} on {Target.Name}, dealing {result.FinalDamage} damage!");
            if (!result.IsDodged && !Target.IsDead)
            {
                Console.WriteLine($"{Target.Name} is stunned for 1 turn!");
                Target.ApplyStatusEffect(new Stun(1));
            }
        }
        else
        {
            Console.WriteLine("You don't have enough Mana to use this ability.");
        }
    }
}