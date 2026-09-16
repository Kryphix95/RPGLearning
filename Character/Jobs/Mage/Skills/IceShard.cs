namespace GameAscendNamespace;


public class  IceShard : Ability
{
    public IceShard()
    {
        Name = "Ice Shard";
        ManaCost = 10;
        Cooldown = 2;
        Description = "A shard of ice that deals damage and slows the target.";
    }
    public override void Execute(Character user, List<Character> Targets)
    {
        Character Target = Targets[0];
        if (user.Mana >= ManaCost)
        {
            user.Mana -= ManaCost;
            this.StartCooldown();
            DamageResult result = user.MagicAttack(DamageType.Ice);
            result = Target.DamageTaken(result);
            Console.WriteLine($"Ice Shard hits {Target.Name} for {result.FinalDamage} Damage. " + $"Crit: {result.IsCrit}, Block: {result.IsBlocked}, Dodge: {result.IsDodged}, Type: {result.DamageType}");
            if (!result.IsDodged && !Target.IsDead)
            {
                Target.ApplyStatusEffect(new SlowEffect(2, 3)); // Apply Slow effect for 2 turns, reducing speed by 3
                Console.WriteLine($"{Target.Name} is slowed!");
            }
        }
        else
        {
            Console.WriteLine("You don't have enough Mana to use this...");
        }
    }
}
