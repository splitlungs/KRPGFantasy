namespace KRPGLib.Fantasy
{
    // ─────────────────────────────────────────────────────────────────────
    //  RANGER
    // ─────────────────────────────────────────────────────────────────────

    public class Hunters_Mark : Spell
    {
        Hunters_Mark() : base()
        {
            Id = "hunters_mark";
            Name = "Hunter's Mark";
            ClassName = "Ranger";
            Description = "Mark a creature as your quarry. Deal extra 1d6 damage on weapon "
                        + "hits and gain advantage on Perception/Survival to find it.";
            TargetType            = SpellTargetType.SingleEntity;
            RangeMetres           = 30f;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 1;
            CooldownSeconds       = 0f;
            EffectType            = SpellEffectType.Debuff;
            EffectMagnitude       = 6f;
            EffectDurationSeconds = 3600f;
            SpellLevel            = 1;
            RequiresPreparation   = true;
        }
    }

    public class Colossus_Slayer : Spell
    {
        Colossus_Slayer() : base()
        {
            Id = "colossus_slayer";
            Name = "Colossus Slayer";
            ClassName = "Ranger";
            Description = "Once per turn deal an extra 1d8 damage when hitting a creature "
                        + "below its maximum HP.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 2f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 8f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Volley : Spell
    {
        Volley() : base()
        {
            Id = "volley";
            Name = "Volley";
            ClassName = "Ranger";
            Description = "Make a ranged attack against any number of creatures within 10 ft "
                        + "of a chosen point within normal range.";
            TargetType          = SpellTargetType.AreaOfEffect;
            RangeMetres         = 30f;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 1f;
            RequiresPreparation = false;
        }
    }

    public class Ensnaring_Strike : Spell
    {
        Ensnaring_Strike() : base()
        {
            Id = "ensnaring_strike";
            Name = "Ensnaring Strike";
            ClassName = "Ranger";
            Description = "Your next hit releases restraining vines. Target makes a Strength "
                        + "save or is restrained for up to 1 minute.";
            TargetType            = SpellTargetType.SingleEntity;
            RangeMetres           = 20f;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 1;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Debuff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 60f;
            SpellLevel            = 1;
            RequiresPreparation   = true;
        }
    }

    public class Vanish : Spell
    {
        Vanish() : base()
        {
            Id = "vanish";
            Name = "Vanish";
            ClassName = "Ranger";
            Description = "Use the Hide action as a bonus action. You are impossible to "
                        + "track by non-magical means until your next turn.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Lightning_Arrow : Spell
    {
        Lightning_Arrow() : base()
        {
            Id = "lightning_arrow";
            Name = "Lightning Arrow";
            ClassName = "Ranger";
            Description = "Your next ranged attack deals 4d8 lightning damage plus 2d8 "
                        + "to creatures within 10 ft of the impact.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 30f;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 3;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 32f;
            SpellLevel          = 3;
            RequiresPreparation = true;
        }
    }

    public class Conjure_Barrage : Spell
    {
        Conjure_Barrage() : base()
        {
            Id = "conjure_barrage";
            Name = "Barrage";
            ClassName = "Ranger";
            Description = "Throw a weapon or fire a piece of ammunition. Identical projectiles "
                        + "fill a 60-ft cone for 3d8 damage.";
            TargetType          = SpellTargetType.AreaOfEffect;
            RangeMetres         = 30f;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 3;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 24f;
            SpellLevel          = 3;
            RequiresPreparation = true;
        }
    }

    public class Feral_Senses : Spell
    {
        Feral_Senses() : base()
        {
            Id = "feral_senses";
            Name = "Feral Senses";
            ClassName = "Ranger";
            Description = "Preternatural senses activate. You don't have disadvantage "
                        + "attacking invisible creatures you can hear or smell.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Swift_Quiver : Spell
    {
        Swift_Quiver() : base()
        {
            Id = "swift_quiver";
            Name = "Swift Quiver";
            ClassName = "Ranger";
            Description = "Transform your quiver. As a bonus action each turn, make two "
                        + "ranged weapon attacks for 1 minute.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 5;
            CooldownSeconds       = 60f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 2f;
            EffectDurationSeconds = 60f;
            SpellLevel            = 5;
            RequiresPreparation   = true;
        }
    }
}
