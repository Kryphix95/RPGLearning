namespace GameAscendNamespace;

public class Meditation : Ability
{
    public Meditation()
    {
        Name = "Meditation";
        ManaCost = 0;
        Cooldown = 5;
        Description = "Meditate to restore a portion of your Mana.";
    }
    public override void Execute(Character user, Character target)
    {
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