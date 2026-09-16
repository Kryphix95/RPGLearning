namespace GameAscendNamespace
{
    public class SmallFireball : Ability
    {
        public SmallFireball()
        {
            Name = "Small Fireball";
            ManaCost = 15;
            Cooldown = 2;
            Description = "A small fireball that deals fire damage to a single target.";
        }
        public override void Execute(Character user, List<Character> Targets)
        {
            Character Target = Targets[0];
            if (user.Mana >= ManaCost)
            {
                user.Mana -= ManaCost;
                this.StartCooldown();
                DamageResult result = user.MagicAttack(DamageType.Fire);
                result = Target.DamageTaken(result);
                Console.WriteLine($"Small Fireball hits {Target.Name} for {result.FinalDamage} Damage. " + $"Crit: {result.IsCrit}, Block: {result.IsBlocked}, Dodge: {result.IsDodged}, Type: {result.DamageType}");
            }
            else
            {
                Console.WriteLine("You don't have enough Mana to use this...");
            }
        }
    }
}