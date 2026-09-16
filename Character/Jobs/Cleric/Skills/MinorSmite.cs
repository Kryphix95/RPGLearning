namespace GameAscendNamespace;

public class MinorSmite : Ability
{
    public MinorSmite()
    {
        Name = "Minor Smite";
        Description = "A basic smite that deals holy damage to a single target.";
        ManaCost = 15;
        Cooldown = 0;
    }
    public override void Execute(Character user, List<Character> Targets)
    {
        Character Target = Targets[0];
        if(user.Mana >= ManaCost)
        {
            user.Mana -= ManaCost;
            this.StartCooldown();
            DamageResult result = user.MagicAttack(DamageType.Holy);
            result = Target.DamageTaken(result);
            // Console.WriteLine("Minor Smite hits " + Target.Name + " for " + result.FinalDamage + " Damage.");
            Console.WriteLine($"Minor Smite hits {Target.Name} for {result.FinalDamage} Damage. " + $"Crit: {result.IsCrit}, Block: {result.IsBlocked}, Dodge: {result.IsDodged}, Type: {result.DamageType}");
        }
        else
        {
            Console.WriteLine("You don't have enough Mana to use this...");
        }
    }
}