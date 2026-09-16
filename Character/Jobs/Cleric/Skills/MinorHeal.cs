namespace GameAscendNamespace;

public class  MinorHeal : Ability
{
    public MinorHeal()
    {
        Name = "Minor Heal";
        ManaCost = 10;
        Cooldown = 1;
        Description = "Heals the Target for a minor Amount of their HP.";
    }
    public override void Execute(Character User, List<Character> Targets)
    {
        Character Target = Targets[0];

        if(User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            this.StartCooldown();
            int healTarget = Math.Max(1, (int)Math.Round(Target.MaxHealth * 0.1));

            int healthBefore = Target.Health;
            
            Target.Health += healTarget;
            if(Target.Health > Target.MaxHealth)
            {
                Target.Health = Target.MaxHealth;
            }

            int actualHeal = Target.Health - healthBefore;

            Console.WriteLine($"{Target.Name} is healed for {actualHeal} HP! " + $"HP: {Target.Health}/{Target.MaxHealth}");
        }
    }
}