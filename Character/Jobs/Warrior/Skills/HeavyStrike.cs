namespace GameAscendNamespace;

public class HeavyStrike : Ability
{
    public HeavyStrike()
    {
        Name = "Heavy Strike";
        Description = "A powerful strike that deals heavy damage to a single target.";
        ManaCost = 20;
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
            result.RawDamage = (int)Math.Round(result.RawDamage * 1.5); // Heavy Strike deals 150% of normal attack damage
            result = Target.DamageTaken(result);
            Console.WriteLine($"{User.Name} uses {Name} on {Target.Name}, dealing {result.FinalDamage} damage!");
        }
        else
        {
            Console.WriteLine("You don't have enough Mana to use this ability.");
        }
    }
}