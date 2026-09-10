namespace GameAscendNamespace;

public class  Haste : Ability
{
    public Haste()
    {
        Name = "Haste";
        ManaCost = 30;
        Cooldown = 3;
        Description = "Cast a Spell on the Target, that increases its Speed for 3 Turns";
    }

    public override void Execute(Character User, Character Target)
    {
        if (User.Mana >= ManaCost)
        {
            User.Mana -= ManaCost;
            this.StartCooldown();
            int speedIncrease = 10; // Increase Dexterity/speed by 10 for 3 turns
            Target.ApplyStatusEffect(new HasteEffect(3, speedIncrease));
        }
    }
}