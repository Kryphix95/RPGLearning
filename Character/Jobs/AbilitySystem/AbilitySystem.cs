namespace GameAscendNamespace;

    public class Ability
    {
        public string Name { get; set; } = "";
        public int ManaCost { get; set; } = 0;
        public int Damage { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public int Cooldown { get; set; } = 0;
        public int RemainingCooldown { get; set; } = 0;
        
        public TargetType TargetType { get; set; } = TargetType.Any;
        public int TargetCount { get; set; } = 1;


        public void ReduceCooldown()
        { 
            if(RemainingCooldown > 0)
        {
            RemainingCooldown--;
        }
    }       
        public void StartCooldown()
        {
            RemainingCooldown = Cooldown + 1; // +1 because the cooldown is reduced at the start of the turn, so if the ability has a cooldown of 2, it will be available again after 2 turns.
    }
        public virtual void Execute(Character User, List<Character> Targets)
        { }


        public string Description { get; set; } = string.Empty;
}
