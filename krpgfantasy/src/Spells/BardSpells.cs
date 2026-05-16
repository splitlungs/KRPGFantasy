namespace KRPGLib.Fantasy
{
    // ─────────────────────────────────────────────────────────────────────
    //  BARD
    // ─────────────────────────────────────────────────────────────────────

    public class Bardic_Inspiration : Spell
    {
        Bardic_Inspiration() : base()
        {
            Id = "bardic_inspiration";
            Name = "Bardic Inspiration";
            ClassName = "Bard";
            Description = "Grant a nearby ally a Bardic Inspiration die (d6). They may add "
                        + "it to one ability check, attack roll, or saving throw within "
                        + "10 minutes.";
            TargetType            = SpellTargetType.SingleEntity;
            RangeMetres           = 20f;
            ResourceType          = SpellResourceType.BardicInspiration;
            ResourceCost          = 1;
            CooldownSeconds       = 2f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 6f;
            EffectDurationSeconds = 600f;
            RequiresPreparation   = false;
        }
    }

    public class Cutting_Words : Spell
    {
        Cutting_Words() : base()
        {
            Id = "cutting_words";
            Name = "Cutting Words";
            ClassName = "Bard";
            Description = "Subtract a Bardic Inspiration die from a creature's attack roll, "
                        + "ability check, or damage roll as a reaction.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 20f;
            ResourceType        = SpellResourceType.BardicInspiration;
            ResourceCost        = 1;
            CooldownSeconds     = 2f;
            EffectType          = SpellEffectType.Debuff;
            EffectMagnitude     = 6f;
            RequiresPreparation = false;
        }
    }

    public class Vicious_Mockery : Spell
    {
        Vicious_Mockery() : base()
        {
            Id = "vicious_mockery";
            Name = "Vicicous Mockery";
            ClassName = "Bard";
            Description = "Unleash a string of insults laced with enchantments. Target takes "
                        + "psychic damage and has disadvantage on its next attack roll.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 20f;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 4f;
            RequiresPreparation = false;
        }
    }

    public class Healing_Word : Spell
    {
        Healing_Word() : base()
        {
            Id = "healing_word";
            Name = "Healing Word";
            ClassName = "Bard";
            Description = "A creature you can see regains 1d4 + spellcasting modifier HP.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 20f;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 1;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Heal;
            EffectMagnitude     = 4f;
            SpellLevel          = 1;
            RequiresPreparation = true;
        }
    }

    public class Hypnotic_Pattern : Spell
    {
        Hypnotic_Pattern() : base()
        {
            Id = "hypnotic_pattern";
            Name = "Hypnotic Pattern";
            ClassName = "Bard";
            Description = "A twisting pattern of colours appears. Creatures in a 30-ft cube "
                        + "that fail a Wisdom save are charmed and incapacitated.";
            TargetType            = SpellTargetType.AreaOfEffect;
            RangeMetres           = 30f;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 3;
            CooldownSeconds       = 60f;
            EffectType            = SpellEffectType.Debuff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 60f;
            SpellLevel            = 3;
            RequiresPreparation   = true;
        }
    }

    public class Countercharm : Spell
    {
        Countercharm() : base()
        {
            Id = "countercharm";
            Name = "Counter Charm";
            ClassName = "Bard";
            Description = "Begin a performance. Friendly creatures within 30 ft gain "
                        + "advantage on saves against being frightened or charmed.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.None;
            CooldownSeconds       = 30f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 6f;
            RequiresPreparation   = false;
        }
    }

    public class Dissonant_Whispers : Spell
    {
        Dissonant_Whispers() : base()
        {
            Id = "dissonant_whispers";
            Name = "Dissonant Whispers";
            ClassName = "Bard";
            Description = "A discordant melody. Target takes 3d6 psychic damage and must "
                        + "use its reaction to flee from you.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 20f;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 1;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 18f;
            SpellLevel          = 1;
            RequiresPreparation = true;
        }
    }

    public class Song_Of_Rest : Spell
    {
        Song_Of_Rest() : base()
        {
            Id = "song_of_rest";
            Name = "Song of Rest";
            ClassName = "Bard";
            Description = "Play soothing music during a short rest. Allies who spend hit dice "
                        + "regain an extra 1d6 HP.";
            TargetType          = SpellTargetType.AreaOfEffect;
            RangeMetres         = 20f;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 300f;
            EffectType          = SpellEffectType.Heal;
            EffectMagnitude     = 6f;
            RequiresPreparation = false;
        }
    }

    public class Magical_Secrets : Spell
    {
        Magical_Secrets() : base()
        {
            Id = "magical_secrets";
            Name = "Magic Secrets";
            ClassName = "Bard";
            Description = "Draw upon arcane secrets from other traditions, casting a spell "
                        + "learned through Magical Secrets.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 1;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Utility;
            EffectMagnitude     = 1f;
            SpellLevel          = 1;
            RequiresPreparation = true;
        }
    }
}
