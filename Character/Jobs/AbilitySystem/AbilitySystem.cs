using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace GameAscendNamespace;

    public class Ability
    {
        public string Name { get; set; } = "";
        public int Damage { get; set; } = 0;
        public int DamageOverTime { get; set; } = 0;
        public int Heal { get; set; } = 0;
        public int HealOverTime { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public int Buff { get; set; } = 0;
        public int Debuff { get; set; } = 0;
        public int Cooldown { get; set; } = 0;
        public int ManaCost { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public virtual void Execute(Character User, Character Target)
        { }
    }
