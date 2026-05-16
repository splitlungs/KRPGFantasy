namespace KRPGLib.Fantasy
{
    // ─────────────────────────────────────────────────────────────────────
    //  ROGUE
    // ─────────────────────────────────────────────────────────────────────

    public class Sneak_Attack : Spell
    {
        Sneak_Attack() : base()
        {
            Id = "rogue_sneak_attack";
            Name = "Sneak";
            ClassName = "Rogue";
            Description = "Toggle Sneak Attack readiness. When conditions are met your next "
                        + "eligible attack deals extra 1d6 damage per two Rogue levels.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 2f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 6f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Dash : Spell
    {
        Dash() : base()
        {
            Id = "rogue_dash";
            Name = "Dash";
            ClassName = "Rogue";
            Description = "Dash as a bonus action, doubling your speed for the turn.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 2f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Hide : Spell
    {
        Hide() : base()
        {
            Id = "rogue_hide";
            Name = "Hide";
            ClassName = "Rogue";
            Description = "Take the Hide action as a bonus action.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Utility;
            EffectMagnitude     = 1f;
            RequiresPreparation = false;
        }
    }

    public class Uncanny_Dodge : Spell
    {
        Uncanny_Dodge() : base()
        {
            Id = "rogue_uncanny_dodge";
            Name = "Dodge";
            ClassName = "Rogue";
            Description = "Use your reaction to halve one attack's damage from an attacker "
                        + "you can see.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Buff;
            EffectMagnitude     = 0.5f;
            RequiresPreparation = false;
        }
    }

    public class Disengage : Spell
    {
        Disengage() : base()
        {
            Id = "rogue_disengage";
            Name = "Disengg";
            ClassName = "Rogue";
            Description = "Disengage as a bonus action, preventing opportunity attacks "
                        + "for the rest of your turn.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Assassinate : Spell
    {
        Assassinate() : base()
        {
            Id = "rogue_assassinate";
            Name = "Asssn";
            ClassName = "Rogue";
            Description = "Prepare a perfect opening strike. Your next attack against a "
                        + "creature that hasn't acted is automatically a critical hit.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 30f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 2f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Death_Strike : Spell
    {
        Death_Strike() : base()
        {
            Id = "rogue_death_strike";
            Name = "DeathS";
            ClassName = "Rogue";
            Description = "Sneak Attack a surprised target. Force a Constitution save "
                        + "(DC 8+Dex+Prof) or the target takes double damage.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 3f;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 60f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 2f;
            RequiresPreparation = false;
        }
    }

    public class Evasion : Spell
    {
        Evasion() : base()
        {
            Id = "rogue_evasion";
            Name = "Evasn";
            ClassName = "Rogue";
            Description = "Activate Evasion. On a successful Dex save for half damage you "
                        + "take none; on a failure you take only half.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 12f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Stroke_Of_Luck : Spell
    {
        Stroke_Of_Luck() : base()
        {
            Id = "rogue_stroke_of_luck";
            Name = "Luck";
            ClassName = "Rogue";
            Description = "Turn a missed attack into a hit, or treat a failed ability check "
                        + "as a 20. Once per short or long rest.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 0f;
            EffectType          = SpellEffectType.Buff;
            EffectMagnitude     = 1f;
            RequiresPreparation = false;
        }
    }
}
