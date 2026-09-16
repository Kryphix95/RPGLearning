namespace GameAscendNamespace;

public class IronGuard : Ability
{
    public IronGuard()
    {
        Name = "Iron Guard";
        Description = "Reducing incoming Physical damage for 3 turns.";
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
            Console.WriteLine($"{User.Name} uses {Name}, reducing incoming Physical damage by 30%!");
            User.ApplyStatusEffect(new IronGuardEffect(3)); // Apply the Iron Guard effect for 3 turns
        }
        else
        {
            Console.WriteLine("You don't have enough Mana to use this ability.");
        }
    }
}