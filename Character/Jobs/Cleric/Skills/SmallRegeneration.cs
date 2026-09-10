namespace GameAscendNamespace;

public class SmallRegeneration : Ability
{
    public SmallRegeneration()
    {
        Name = "Small Regeneration";
        ManaCost = 15;
        Cooldown = 2;
        Description = "Cast a Spell on the Target, that heals it at the start of its Turn. For 4 Turns";
    }
    public override void Execute(Character User, Character Target)
    {
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            this.StartCooldown();

            int healPerTurn = Math.Max(1, (int)Math.Round(Target.MaxHealth * 0.01));

            Target.ApplyStatusEffect(new Regeneration(4, healPerTurn));
        }

    }
}