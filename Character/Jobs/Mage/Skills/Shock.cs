namespace GameAscendNamespace;


public class Shock : Ability
{ 
    public Shock()
    {
        Name = "Shock";
        ManaCost = 15;
        Cooldown = 2;
        Description = "A bolt of lightning that deals damage and has a chance to stun the target.";
    }
    public override void Execute(Character user, List<Character> Targets)
    {
        Character Target = Targets[0];
        if (user.Mana >= ManaCost)
        {
            user.Mana -= ManaCost;
            this.StartCooldown();
            DamageResult result = user.MagicAttack(DamageType.Lightning);
            result = Target.DamageTaken(result);
            Console.WriteLine($"Shock hits {Target.Name} for {result.FinalDamage} Damage. " + $"Crit: {result.IsCrit}, Block: {result.IsBlocked}, Dodge: {result.IsDodged}, Type: {result.DamageType}");
            if (!result.IsDodged && !Target.IsDead)
            {
                
                if (Random.Shared.Next(0, 100) < 30) // 30% chance to stun
                {
                    Target.ApplyStatusEffect(new Stun(1)); // Apply Stun effect for 1 turn
                    Console.WriteLine($"{Target.Name} is stunned!");
                }
            }
        }
        else
        {
            Console.WriteLine("You don't have enough Mana to use this...");
        }
    }
}