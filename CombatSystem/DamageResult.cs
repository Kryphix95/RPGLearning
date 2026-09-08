namespace GameAscendNamespace;

public class DamageResult
{
    public int RawDamage { get; set; }
    public int FinalDamage { get; set; }

    public bool IsCrit { get; set; }
    public bool IsDodged { get; set; }
    public bool IsBlocked { get; set; }
    public bool TargetDied { get; set; }

    public DamageResult(int rawDamage, bool isCrit)
    {
        RawDamage = rawDamage;
        IsCrit = isCrit;
    }
}