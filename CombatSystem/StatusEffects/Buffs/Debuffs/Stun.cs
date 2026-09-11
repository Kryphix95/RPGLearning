namespace GameAscendNamespace;

public class Stun : StatusEffect
{
    public Stun(int duration)
    {
        Name = "Stun";
        Duration = duration;
    }
   public override bool PreventsAction => true;
}