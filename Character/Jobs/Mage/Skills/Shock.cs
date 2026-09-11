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
    public override void Execute(Character user, Character target)
    {
        if (user.Mana >= ManaCost)
        {
            user.Mana -= ManaCost;
            this.StartCooldown();
            DamageResult result = user.MagicAttack(DamageType.Lightning);
            result = target.DamageTaken(result);
            Console.WriteLine($"Shock hits {target.Name} for {result.FinalDamage} Damage. " + $"Crit: {result.IsCrit}, Block: {result.IsBlocked}, Dodge: {result.IsDodged}, Type: {result.DamageType}");
            if (!result.IsDodged && !target.IsDead)
            {
                
                if (Random.Shared.Next(0, 100) < 30) // 30% chance to stun
                {
                    target.ApplyStatusEffect(new Stun(1)); // Apply Stun effect for 1 turn
                    Console.WriteLine($"{target.Name} is stunned!");
                }
            }
        }
        else
        {
            Console.WriteLine("You don't have enough Mana to use this...");
        }
    }
}