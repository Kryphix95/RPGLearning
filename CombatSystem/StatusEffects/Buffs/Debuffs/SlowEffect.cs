namespace GameAscendNamespace
{
    public class SlowEffect : StatusEffect
    {
        int SpeedDecrease { get; set; }

        public SlowEffect(int duration, int speedDecrease)
        {
            Name = "Slow";
            Duration = duration;
            SpeedDecrease = speedDecrease;
        }

        public override void OnApply(Character target)
        {
            target.Speed -= SpeedDecrease;
            Console.WriteLine($"{target.Name} Speed: {target.Speed}");
        }

        public override void OnRemove(Character target)
        {
            target.Speed += SpeedDecrease;
            Console.WriteLine($"{target.Name} Speed: {target.Speed}");
        }

    }

}