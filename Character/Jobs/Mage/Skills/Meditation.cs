namespace GameAscendNamespace;

public class Meditation : Ability
{
    public Meditation()
    {
        Name = "Meditation";
        ManaCost = 0;
        Cooldown = 5;
        Description = "Meditate to restore a portion of your Mana.";
        TargetType = TargetType.Self;
    }
    public override void Execute(Character user, List<Character> targets)
    {
        Character target = targets[0];
        if (user.Mana < user.MaxMana)
        {
            this.StartCooldown();
            int manaRestored = (int)Math.Min(50, user.MaxMana - user.Mana); // Restore 50 Mana
            user.Mana += manaRestored;
            Console.WriteLine($"{user.Name} meditates and restores {manaRestored} Mana. Current Mana: {user.Mana}/{user.MaxMana}");
        }
        else
        {
            Console.WriteLine("Your Mana is already full.");
        }
    }
}