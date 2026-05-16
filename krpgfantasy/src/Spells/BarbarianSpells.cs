namespace KRPGLib.Fantasy
{
    // ─────────────────────────────────────────────────────────────────────
    //  BARBARIAN
    // ─────────────────────────────────────────────────────────────────────

    public class Rage : Spell
    {
        Rage() : base()
        {
            Id = "rage";
            Name = "Rage";
            ClassName = "Barbarian";
            Description = "Enter a battle fury for 1 minute. Gain bonus damage on Strength "
                         + "attacks, resistance to physical damage, and advantage on Strength "
                         + "checks and saving throws.";
            TargetType      = SpellTargetType.Self;
            ResourceType    = SpellResourceType.RageUse;
            ResourceCost    = 1;
            CooldownSeconds = 6f;
            EffectType = SpellEffectType.Buff;
            EffectMagnitude = 2f;
            EffectDurationSeconds = 60f;
            RequiresPreparation = false;
        }
    }
    public class Reckless : Spell
    {
        Reckless() : base()
        {
            Id = "reckless"; 
            Name = "Reckless"; 
            ClassName = "Barbarian";
            Description = "Attack recklessly this turn, gaining advantage on Strength attack "
                        + "rolls but granting attackers advantage against you until your "
                        + "next turn.";
            TargetType      = SpellTargetType.Self;
            ResourceType    = SpellResourceType.None;
            CooldownSeconds = 6f;
            EffectType      = SpellEffectType.Buff;
            EffectMagnitude = 1f;
            EffectDurationSeconds = 6f;
            RequiresPreparation = false;
            }
    }
    public class Intimidate : Spell
    {
        Intimidate() : base ()
        {
            Id = "intimidate"; 
            Name = "Intimidate"; 
            ClassName = "Barbarian";
            Description = "Let out a battle cry. Nearby enemies that fail a Wisdom save "
                        + "become frightened for 10 seconds.";
            TargetType      = SpellTargetType.AreaOfEffect;
            RangeMetres     = 10f;
            ResourceType    = SpellResourceType.None;
            CooldownSeconds = 30f;
            EffectType      = SpellEffectType.Debuff;
            EffectMagnitude = 1f;
            EffectDurationSeconds = 10f;
            RequiresPreparation = false;
        }
    }
    public class Danger_Sense : Spell
    {
        Danger_Sense() : base()
        {
            Id = "danger_sense";
            Name = "Danger Sense";
            ClassName = "Barbarian";
            Description = "Activate primal awareness. Gain advantage on Dexterity saving "
                        + "throws against effects you can see until your next turn.";
            TargetType      = SpellTargetType.Self;
            ResourceType    = SpellResourceType.None;
            CooldownSeconds = 12f;
            EffectType      = SpellEffectType.Buff;
            EffectMagnitude = 1f;
            EffectDurationSeconds = 6f;
            RequiresPreparation = false;
        }
    }
    public class Relentless : Spell
    {
        Relentless() : base()
        {
            Id = "barbarian_relentless"; 
            Name = "Relentless"; 
            ClassName = "Barbarian";
            Description = "When you would be reduced to 0 HP while raging, make a "
                        + "Constitution save (DC 10, +5 per use) to drop to 1 HP instead.";
            TargetType      = SpellTargetType.Self;
            ResourceType    = SpellResourceType.None;
            CooldownSeconds = 60f;
            EffectType      = SpellEffectType.Buff;
            EffectMagnitude = 1f;
            RequiresPreparation = false;
        }
    }
    public class Brutal_Strike : Spell
    {
        Brutal_Strike() : base()
        {
            Id = "barbarian_brutal_strike"; 
            Name = "Brutal"; 
            ClassName = "Barbarian";
            Description = "Channel rage into your next hit, dealing bonus damage equal "
                        + "to your Rage bonus on top of the weapon's normal damage.";
            TargetType      = SpellTargetType.SingleEntity;
            RangeMetres     = 3f;
            ResourceType    = SpellResourceType.None;
            CooldownSeconds = 8f;
            EffectType      = SpellEffectType.Damage;
            EffectMagnitude = 6f;
            RequiresPreparation = false;
        }
    }
    public class Frenzy : Spell
    {
        Frenzy() : base()
        {
            Id = "barbarian_frenzy"; 
            Name = "Frenzy"; 
            ClassName = "Barbarian";
            Description = "While raging, make one additional melee weapon attack as a "
                        + "bonus action each turn. Gain one level of exhaustion when the "
                        + "rage ends.";
            TargetType      = SpellTargetType.SingleEntity;
            RangeMetres     = 3f;
            ResourceType    = SpellResourceType.None;
            CooldownSeconds = 2f;
            EffectType      = SpellEffectType.Damage;
            EffectMagnitude = 1f;
            RequiresPreparation = false;
        }
    }
    public class Totem_Bear : Spell
    {
        Totem_Bear() : base()
        {
            Id = "barbarian_totem_bear"; 
            Name = "Bear Totem"; 
            ClassName = "Barbarian";
            Description = "Call upon the Bear Totem Spirit. While raging, gain resistance "
                        + "to all damage except psychic.";
            TargetType      = SpellTargetType.Self;
            ResourceType    = SpellResourceType.RageUse;
            ResourceCost    = 1;
            CooldownSeconds = 60f;
            EffectType      = SpellEffectType.Buff;
            EffectMagnitude = 2f;
            EffectDurationSeconds = 60f;
            RequiresPreparation = false;
        }
    }
    public class Indomitable_Might : Spell
    {
        Indomitable_Might() : base()
        {
            Id = "barbarian_indomitable_might"; 
            Name = "Indomitable Might"; 
            ClassName = "Barbarian";
            Description = "For 10 seconds any Strength check total lower than your Strength "
                        + "score is replaced by your Strength score.";
            TargetType      = SpellTargetType.Self;
            ResourceType    = SpellResourceType.None;
            CooldownSeconds = 20f;
            EffectType      = SpellEffectType.Buff;
            EffectMagnitude = 1f;
            EffectDurationSeconds = 10f;
            RequiresPreparation = false;
        }
    }
}