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
    public override void Execute(Character User, Character Target)
    {
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            this.StartCooldown();
            Console.WriteLine($"{User.Name} uses {Name}, reducing incoming Physical damage by 30%!");
            User.ApplyStatusEffect(new IronGuardEffect(4)); // Stored as 4 to Apply an Iron Guard effect that lasts for 3 turns and reduces damage by 30%
        }
        else
        {
            Console.WriteLine("You don't have enough Mana to use this ability.");
        }
    }
}