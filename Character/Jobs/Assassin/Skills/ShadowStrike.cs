namespace GameAscendNamespace;

public class ShadowStrike : Ability
{
    public ShadowStrike()
    {
        Name = "Shadow Strike";
        ManaCost = 15;
        Cooldown = 2;
        Description = "Deals dark Damage to the Target. Has an increased Crit Damage. If the Target is Poisoned, it hits a second time.";
    }

        public override void Execute(Character User, List <Character> Targets)
        {
            Character Target = Targets[0];

            if (User.Mana >= ManaCost)
            {
                User.Mana -= ManaCost;
                this.StartCooldown();

                Poison? activePoison = Target.ActiveEffects.OfType<Poison>().FirstOrDefault();

                if (activePoison == null)
                {
                     DamageResult result = User.NormalAttack(0, 0.15);
                     result.DamageType = DamageType.Dark;
                     result = Target.DamageTaken(result);

                }
                else
                {
                    
                    DamageResult result1 = User.NormalAttack(0, 0.15);
                    result1.DamageType = DamageType.Dark;
                    result1 = Target.DamageTaken(result1);

                    if (!Target.IsDead)
                    {
                        DamageResult result2 = User.NormalAttack(0, 0.15);
                        result2.DamageType = DamageType.Dark;
                        result2 = Target.DamageTaken(result2);
                    }

                }
            }
        }
}