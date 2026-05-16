namespace KRPGLib.Fantasy
{
    // ─────────────────────────────────────────────────────────────────────
    //  FIGHTER
    // ─────────────────────────────────────────────────────────────────────

    public class Second_Wind : Spell
    {
        Second_Wind() : base()
        {
            Id = "second_wind";
            Name = "Second Wind";
            ClassName = "Fighter";
            Description = "As a bonus action regain 1d10 + Fighter level HP. "
                        + "Recharges on a short or long rest.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.FighterResource;
            ResourceCost        = 1;
            CooldownSeconds     = 0f;
            EffectType          = SpellEffectType.Heal;
            EffectMagnitude     = 10f;
            RequiresPreparation = false;
        }
    }

    public class Action_Surge : Spell
    {
        Action_Surge() : base()
        {
            Id = "action_surge";
            Name = "Action Surge";
            ClassName = "Fighter";
            Description = "Take one additional action this turn. Recharges on a short or "
                        + "long rest.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.FighterResource;
            ResourceCost          = 1;
            CooldownSeconds       = 0f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Trip_Attack : Spell
    {
        Trip_Attack() : base()
        {
            Id = "trip_attack";
            Name = "Trip";
            ClassName = "Fighter";
            Description = "Expend a Superiority Die to add to damage and attempt to knock "
                        + "a target prone. Target makes a Strength save.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 3f;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Debuff;
            EffectMagnitude     = 8f;
            RequiresPreparation = false;
        }
    }

    public class Menacing_Attack : Spell
    {
        Menacing_Attack() : base()
        {
            Id = "menacing_attack";
            Name = "Menacing Attack";
            ClassName = "Fighter";
            Description = "Add a Superiority Die to damage and force a Wisdom save or "
                        + "the target is frightened until the end of your next turn.";
            TargetType            = SpellTargetType.SingleEntity;
            RangeMetres           = 3f;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Debuff;
            EffectMagnitude       = 8f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Disarming_Attack : Spell
    {
        Disarming_Attack() : base()
        {
            Id = "disarming_attack";
            Name = "Disarm";
            ClassName = "Fighter";
            Description = "Add a Superiority Die to damage. Target makes a Strength save "
                        + "or drops the object it holds.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 3f;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 8f;
            EffectType          = SpellEffectType.Debuff;
            EffectMagnitude     = 8f;
            RequiresPreparation = false;
        }
    }

    public class Indomitable : Spell
    {
        Indomitable() : base()
        {
            Id = "indomitable";
            Name = "Indomitable";
            ClassName = "Fighter";
            Description = "Reroll a failed saving throw, keeping the new result. "
                        + "Once per long rest.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.FighterResource;
            ResourceCost        = 1;
            CooldownSeconds     = 0f;
            EffectType          = SpellEffectType.Buff;
            EffectMagnitude     = 1f;
            RequiresPreparation = false;
        }
    }

    public class Rally : Spell
    {
        Rally() : base()
        {
            Id = "rally";
            Name = "Rally";
            ClassName = "Fighter";
            Description = "Bolster a friendly creature with a Superiority Die, granting "
                        + "temporary HP equal to the roll + Charisma modifier.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 20f;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 12f;
            EffectType          = SpellEffectType.Heal;
            EffectMagnitude     = 8f;
            RequiresPreparation = false;
        }
    }

    public class Parry : Spell
    {
        Parry() : base()
        {
            Id = "parry";
            Name = "Parry";
            ClassName = "Fighter";
            Description = "Reduce melee damage by a Superiority Die roll + Dexterity "
                        + "modifier as a reaction.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Buff;
            EffectMagnitude     = 8f;
            RequiresPreparation = false;
        }
    }

    public class Commanders_Strike : Spell
    {
        Commanders_Strike() : base()
        {
            Id = "commanders_strike";
            Name = "Commander's Strike";
            ClassName = "Fighter";
            Description = "Forgo one attack to direct an ally to strike. They use their "
                        + "reaction to attack and add a Superiority Die to damage.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 20f;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Buff;
            EffectMagnitude     = 8f;
            RequiresPreparation = false;
        }
    }
}
