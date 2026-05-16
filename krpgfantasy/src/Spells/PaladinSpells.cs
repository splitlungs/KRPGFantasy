namespace KRPGLib.Fantasy
{
    // ─────────────────────────────────────────────────────────────────────
    //  PALADIN
    // ─────────────────────────────────────────────────────────────────────

    public class Divine_Smite : Spell
    {
        Divine_Smite() : base()
        {
            Id = "divine_smite";
            Name = "Divine Smite";
            ClassName = "Paladin";
            Description = "Expend a spell slot to deal radiant damage on a melee hit. "
                        + "2d8 base (+1d8 per slot level above 1st), extra vs undead/fiends.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 2f;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 1;
            CooldownSeconds     = 0f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 16f;
            SpellLevel          = 1;
            RequiresPreparation = true;
        }
    }

    public class Lay_On_Hands : Spell
    {
        Lay_On_Hands() : base()
        {
            Id = "lay_on_hands";
            Name = "Lay on Hands";
            ClassName = "Paladin";
            Description = "Restore HP from your healing pool (5 x Paladin level). "
                        + "Spend 5 points to cure a disease or neutralise a poison.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 2f;
            ResourceType        = SpellResourceType.LayOnHandsPool;
            ResourceCost        = 5;
            CooldownSeconds     = 0f;
            EffectType          = SpellEffectType.Heal;
            EffectMagnitude     = 5f;
            RequiresPreparation = false;
        }
    }

    public class Sacred_Weapon : Spell
    {
        Sacred_Weapon() : base()
        {
            Id = "sacred_weapon";
            Name = "Sacred Weapon";
            ClassName = "Paladin";
            Description = "Imbue your weapon with holy power for 1 minute. Add Charisma "
                        + "modifier to attack rolls; weapon sheds bright light.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 60f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 60f;
            RequiresPreparation   = false;
        }
    }

    public class Turn_The_Unholy : Spell
    {
        Turn_The_Unholy() : base()
        {
            Id = "turn_the_unholy";
            Name = "Turn Unholy";
            ClassName = "Paladin";
            Description = "Fiends and undead within 30 ft must make a Wisdom save or be "
                        + "turned for 1 minute.";
            TargetType            = SpellTargetType.AreaOfEffect;
            RangeMetres           = 15f;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 120f;
            EffectType            = SpellEffectType.Debuff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 60f;
            RequiresPreparation   = false;
        }
    }

    public class Bless : Spell
    {
        Bless() : base()
        {
            Id = "bless";
            Name = "Bless";
            ClassName = "Paladin";
            Description = "Up to three creatures add 1d4 to attack rolls and saving throws "
                        + "for 1 minute.";
            TargetType            = SpellTargetType.AreaOfEffect;
            RangeMetres           = 10f;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 1;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 4f;
            EffectDurationSeconds = 60f;
            SpellLevel            = 1;
            RequiresPreparation   = true;
        }
    }

    public class Shield_Of_Faith : Spell
    {
        Shield_Of_Faith() : base()
        {
            Id = "shield_of_faith";
            Name = "Shield of Faith";
            ClassName = "Paladin";
            Description = "A shimmering field surrounds a creature you choose, granting "
                        + "+2 AC for 10 minutes.";
            TargetType            = SpellTargetType.SingleEntity;
            RangeMetres           = 20f;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 1;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 2f;
            EffectDurationSeconds = 600f;
            SpellLevel            = 1;
            RequiresPreparation   = true;
        }
    }

    public class Holy_Nimbus : Spell
    {
        Holy_Nimbus() : base()
        {
            Id = "holy_nimbus";
            Name = "Holy Nimbus";
            ClassName = "Paladin";
            Description = "Emit sunlight for 1 minute. Hostile creatures in bright light "
                        + "take 10 radiant damage at the start of their turn.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 5;
            CooldownSeconds       = 300f;
            EffectType            = SpellEffectType.Damage;
            EffectMagnitude       = 10f;
            EffectDurationSeconds = 60f;
            SpellLevel            = 5;
            RequiresPreparation   = true;
        }
    }

    public class Aura_Of_Courage : Spell
    {
        Aura_Of_Courage() : base()
        {
            Id = "aura_of_courage";
            Name = "Aura of Courage";
            ClassName = "Paladin";
            Description = "Toggle Aura of Courage. While active, friendly creatures within "
                        + "10 ft cannot be frightened.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 2f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = -1f;
            RequiresPreparation   = false;
        }
    }

    public class Cleansing_Touch : Spell
    {
        Cleansing_Touch() : base()
        {
            Id = "cleansing_touch";
            Name = "Cleansing Touch";
            ClassName = "Paladin";
            Description = "End one spell on yourself or a willing creature you touch. "
                        + "Uses = Charisma modifier per long rest.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 2f;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 60f;
            EffectType          = SpellEffectType.Utility;
            EffectMagnitude     = 1f;
            RequiresPreparation = false;
        }
    }
}
