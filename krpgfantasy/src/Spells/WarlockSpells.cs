namespace KRPGLib.Fantasy
{
    // ─────────────────────────────────────────────────────────────────────
    //  WARLOCK
    // ─────────────────────────────────────────────────────────────────────

    public class Eldritch_Blast : Spell
    {
        Eldritch_Blast() : base()
        {
            Id = "eldritch_blast";
            Name = "Eldritch Blast";
            ClassName = "Warlock";
            Description = "Hurl a beam of crackling energy. On a hit deals 1d10 force damage. "
                        + "Fire additional beams at higher levels (up to 4 at level 17).";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 24f;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 2f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 10f;
            SpellLevel          = 0;
            RequiresPreparation = false;
        }
    }

    public class Hex : Spell
    {
        Hex() : base()
        {
            Id = "hex";
            Name = "Hex";
            ClassName = "Warlock";
            Description = "Curse a creature you can see. Deal an extra 1d6 necrotic damage "
                        + "whenever you hit it, and impose disadvantage on one ability check "
                        + "of your choice. Move the curse if the target dies.";
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

    public class Armor_Of_Agathys : Spell
    {
        Armor_Of_Agathys() : base()
        {
            Id = "armor_of_agathys";
            Name = "Armor of Agathys";
            ClassName = "Warlock";
            Description = "A protective magical force surrounds you. Gain 5 temporary HP "
                        + "per slot level. While the temp HP lasts, any creature that hits "
                        + "you with a melee attack takes 5 cold damage per slot level.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 1;
            CooldownSeconds       = 0f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 5f;
            EffectDurationSeconds = 3600f;
            SpellLevel            = 1;
            RequiresPreparation   = true;
        }
    }

    public class Hellish_Rebuke : Spell
    {
        Hellish_Rebuke() : base()
        {
            Id = "hellish_rebuke";
            Name = "Hellish Rebuke";
            ClassName = "Warlock";
            Description = "React to being damaged by pointing your finger. The attacker is "
                        + "momentarily surrounded by hellish flames, taking 2d10 fire damage "
                        + "(Dex save for half).";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 18f;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 1;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 20f;
            SpellLevel          = 1;
            RequiresPreparation = true;
        }
    }

    public class Darkness : Spell
    {
        Darkness() : base()
        {
            Id = "darkness";
            Name = "Darkness";
            ClassName = "Warlock";
            Description = "Magical darkness spreads from a point you choose, filling a "
                        + "15-ft radius sphere. Darkvision can't penetrate it and light "
                        + "spells of 2nd level or lower cannot illuminate it.";
            TargetType            = SpellTargetType.AreaOfEffect;
            RangeMetres           = 20f;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 2;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Utility;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 60f;
            SpellLevel            = 2;
            RequiresPreparation   = true;
        }
    }

    public class Hunger_Of_Hadar : Spell
    {
        Hunger_Of_Hadar() : base()
        {
            Id = "hunger_of_hadar";
            Name = "Hunger of Hadar";
            ClassName = "Warlock";
            Description = "Open a gateway to the void. A 20-ft radius sphere of blackness "
                        + "and bitter cold appears. Creatures inside take 2d6 cold at the "
                        + "start of their turn and 2d6 acid at the end (Con save negates cold).";
            TargetType            = SpellTargetType.AreaOfEffect;
            RangeMetres           = 30f;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 3;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Damage;
            EffectMagnitude       = 24f;
            EffectDurationSeconds = 60f;
            SpellLevel            = 3;
            RequiresPreparation   = true;
        }
    }

    public class Hold_Monster : Spell
    {
        Hold_Monster() : base()
        {
            Id = "hold_monster";
            Name = "Hold Monster";
            ClassName = "Warlock";
            Description = "Target a creature you can see. It must succeed on a Wisdom save "
                        + "or be paralysed for 1 minute. The target may repeat the save at "
                        + "the end of each of its turns.";
            TargetType            = SpellTargetType.SingleEntity;
            RangeMetres           = 18f;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 5;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Debuff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 60f;
            SpellLevel            = 5;
            RequiresPreparation   = true;
        }
    }

    public class Dark_Ones_Blessing : Spell
    {
        Dark_Ones_Blessing() : base()
        {
            Id = "dark_ones_blessing";
            Name = "Dark Ones' Blessing";
            ClassName = "Warlock";
            Description = "Fiend Patron: When you reduce a hostile creature to 0 HP, gain "
                        + "temporary HP equal to your Charisma modifier + Warlock level.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 2f;
            EffectType          = SpellEffectType.Heal;
            EffectMagnitude     = 5f;
            RequiresPreparation = false;
        }
    }

    public class Dark_Ones_Own_Luck : Spell
    {
        Dark_Ones_Own_Luck() : base()
        {
            Id = "dark_ones_own_luck";
            Name = "Dark Ones'Luck";
            ClassName = "Warlock";
            Description = "Fiend Patron: Call on your patron to alter fate in your favour. "
                        + "Add a d10 to one ability check or saving throw. "
                        + "Recharges on a short or long rest.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 0f;
            EffectType          = SpellEffectType.Buff;
            EffectMagnitude     = 10f;
            RequiresPreparation = false;
        }
    }

    public class Mystic_Arcanum : Spell
    {
        Mystic_Arcanum() : base()
        {
            Id = "mystic_arcanum";
            Name = "Mysic Arcanum";
            ClassName = "Warlock";
            Description = "Cast a powerful spell bestowed by your patron without expending "
                        + "a spell slot (6th level or higher). Usable once per long rest "
                        + "per arcanum level.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 0f;
            EffectType          = SpellEffectType.Utility;
            EffectMagnitude     = 1f;
            SpellLevel          = 6;
            RequiresPreparation = false;
        }
    }
}