namespace KRPGLib.Fantasy
{
    // ─────────────────────────────────────────────────────────────────────
    //  MONK
    // ─────────────────────────────────────────────────────────────────────

    public class Flurry_Of_Blows : Spell
    {
        Flurry_Of_Blows() : base()
        {
            Id = "flurry_of_blows";
            Name = "Flurry Of Blows";
            ClassName = "Monk";
            Description = "Spend 1 ki point to make two unarmed strikes as a bonus action "
                        + "after the Attack action.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 2f;
            ResourceType        = SpellResourceType.KiPoint;
            ResourceCost        = 1;
            CooldownSeconds     = 2f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 6f;
            RequiresPreparation = false;
        }
    }

    public class Stunning_Strike : Spell
    {
        Stunning_Strike() : base()
        {
            Id = "stunning_strike";
            Name = "Stunning Strike";
            ClassName = "Monk";
            Description = "Spend 1 ki point after a hit to force a Constitution save. "
                        + "On failure the target is stunned until the end of your next turn.";
            TargetType            = SpellTargetType.SingleEntity;
            RangeMetres           = 2f;
            ResourceType          = SpellResourceType.KiPoint;
            ResourceCost          = 1;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Debuff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Step_Of_The_Wind : Spell
    {
        Step_Of_The_Wind() : base()
        {
            Id = "step_of_the_wind";
            Name = "Step of the Wind";
            ClassName = "Monk";
            Description = "Spend 1 ki point to Disengage or Dash as a bonus action. "
                        + "Jump distance is doubled this turn.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.KiPoint;
            ResourceCost          = 1;
            CooldownSeconds       = 2f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 2f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Patient_Defense : Spell
    {
        Patient_Defense() : base()
        {
            Id = "patient_defense";
            Name = "Patient Defense";
            ClassName = "Monk";
            Description = "Spend 1 ki point to Dodge as a bonus action. Attacks against "
                        + "you have disadvantage until your next turn.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.KiPoint;
            ResourceCost          = 1;
            CooldownSeconds       = 2f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Deflect_Missiles : Spell
    {
        Deflect_Missiles() : base()
        {
            Id = "deflect_missiles";
            Name = "Deflct Missiles";
            ClassName = "Monk";
            Description = "Reduce ranged weapon damage by 1d10 + Dex + Monk level as a "
                        + "reaction. If reduced to 0, spend 1 ki to throw it back.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Buff;
            EffectMagnitude     = 10f;
            RequiresPreparation = false;
        }
    }

    public class Wholeness_Of_Body : Spell
    {
        Wholeness_Of_Body() : base()
        {
            Id = "wholeness_of_body";
            Name = "Wholeness Of Body";
            ClassName = "Monk";
            Description = "Cure yourself of one disease or neutralise one poison. Costs 3 ki.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.KiPoint;
            ResourceCost        = 3;
            CooldownSeconds     = 60f;
            EffectType          = SpellEffectType.Heal;
            EffectMagnitude     = 1f;
            RequiresPreparation = false;
        }
    }

    public class Empty_Body : Spell
    {
        Empty_Body() : base()
        {
            Id = "empty_body";
            Name = "Empty Body";
            ClassName = "Monk";
            Description = "Spend 4 ki to become invisible for 1 minute and gain resistance "
                        + "to all damage except force.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.KiPoint;
            ResourceCost          = 4;
            CooldownSeconds       = 120f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 60f;
            RequiresPreparation   = false;
        }
    }

    public class Quivering_Palm : Spell
    {
        Quivering_Palm() : base()
        {
            Id = "quivering_palm";
            Name = "Quivering Palm";
            ClassName = "Monk";
            Description = "Spend 3 ki on a hit to set up lethal vibrations. Use an action "
                        + "later to potentially reduce the target to 0 HP.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 2f;
            ResourceType        = SpellResourceType.KiPoint;
            ResourceCost        = 3;
            CooldownSeconds     = 60f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 10f;
            RequiresPreparation = false;
        }
    }

    public class Tranquility : Spell
    {
        Tranquility() : base()
        {
            Id = "tranquility";
            Name = "Tranqility";
            ClassName = "Monk";
            Description = "Enter a meditative state. Gain the effect of Sanctuary until "
                        + "disturbed, and recover extra ki on rest.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 300f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 2f;
            EffectDurationSeconds = 480f;
            RequiresPreparation   = false;
        }
    }
}
