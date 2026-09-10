namespace GameAscendNamespace;

public class HasteEffect : StatusEffect
{
    int SpeedIncrease { get; set; }

    public HasteEffect(int duration, int speedIncrease)
    {
        Name = "Haste";
        Duration = duration;
        SpeedIncrease = speedIncrease;
    }
    public override void OnApply(Character target)
    {
        target.Speed += SpeedIncrease;
        Console.WriteLine($"{target.Name} Speed: {target.Speed}");
    }
    public override void OnRemove(Character target)
    {
        target.Speed -= SpeedIncrease;
        Console.WriteLine($"{target.Name} Speed: {target.Speed}");
    }

}