namespace KRPGLib.Fantasy
{
    // ─────────────────────────────────────────────────────────────────────
    //  WIZARD
    // ─────────────────────────────────────────────────────────────────────

    public class Magic_Missile : Spell
    {
        Magic_Missile() : base()
        {
            Id = "magic_missile";
            Name = "Magic Missile";
            ClassName = "Wizard";
            Description = "Three darts of magical force each automatically hit for 1d4+1 "
                        + "force damage. Extra darts per slot level above 1st.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 25f;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 1;
            CooldownSeconds     = 2f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 15f;
            SpellLevel          = 1;
            RequiresPreparation = true;
        }
    }

    public class Shield : Spell
    {
        Shield() : base()
        {
            Id = "shield";
            Name = "Shield";
            ClassName = "Wizard";
            Description = "React to an attack to gain +5 AC and immunity to Magic Missile "
                        + "until your next turn.";
            TargetType            = SpellTargetType.Self;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 1;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Buff;
            EffectMagnitude       = 5f;
            EffectDurationSeconds = 6f;
            SpellLevel            = 1;
            RequiresPreparation   = true;
        }
    }

    public class Misty_Step : Spell
    {
        Misty_Step() : base()
        {
            Id = "misty_step";
            Name = "Misty Step";
            ClassName = "Wizard";
            Description = "Teleport up to 30 ft to an unoccupied space you can see as a "
                        + "bonus action.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 2;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Utility;
            EffectMagnitude     = 30f;
            SpellLevel          = 2;
            RequiresPreparation = true;
        }
    }

    public class Fireball : Spell
    {
        Fireball() : base()
        {
            Id = "fireball";
            Name = "Fireball";
            ClassName = "Wizard";
            Description = "A roaring blast in a 20-ft radius. Creatures make a Dex save or "
                        + "take 8d6 fire damage (half on success).";
            TargetType          = SpellTargetType.AreaOfEffect;
            RangeMetres         = 30f;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 3;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Damage;
            EffectMagnitude     = 48f;
            SpellLevel          = 3;
            RequiresPreparation = true;
        }
    }

    public class Counterspell : Spell
    {
        Counterspell() : base()
        {
            Id = "counterspell";
            Name = "Counterspell";
            ClassName = "Wizard";
            Description = "Interrupt a spell within 60 ft. Level 3 and below fail "
                        + "automatically; higher requires a check.";
            TargetType          = SpellTargetType.SingleEntity;
            RangeMetres         = 20f;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 3;
            CooldownSeconds     = 6f;
            EffectType          = SpellEffectType.Utility;
            EffectMagnitude     = 1f;
            SpellLevel          = 3;
            RequiresPreparation = true;
        }
    }

    public class Arcane_Recovery : Spell
    {
        Arcane_Recovery() : base()
        {
            Id = "arcane_recovery";
            Name = "Arcane Recovery";
            ClassName = "Wizard";
            Description = "After a short rest, recover expended spell slots whose total level "
                        + "is ≤ half Wizard level (max 5th level slots).";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.None;
            CooldownSeconds     = 300f;
            EffectType          = SpellEffectType.Utility;
            EffectMagnitude     = 1f;
            RequiresPreparation = false;
        }
    }

    public class Polymorph : Spell
    {
        Polymorph() : base()
        {
            Id = "polymorph";
            Name = "Polymorph";
            ClassName = "Wizard";
            Description = "Transform a creature within 60 ft into a new form for 1 hour "
                        + "(Wisdom save negates).";
            TargetType            = SpellTargetType.SingleEntity;
            RangeMetres           = 20f;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 4;
            CooldownSeconds       = 6f;
            EffectType            = SpellEffectType.Utility;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 3600f;
            SpellLevel            = 4;
            RequiresPreparation   = true;
        }
    }

    public class Wall_Of_Force : Spell
    {
        Wall_Of_Force() : base()
        {
            Id = "wall_of_force";
            Name = "Wall of Force";
            ClassName = "Wizard";
            Description = "An invisible wall of force appears. Nothing physically passes "
                        + "through it for 10 minutes.";
            TargetType            = SpellTargetType.AreaOfEffect;
            RangeMetres           = 20f;
            ResourceType          = SpellResourceType.SpellSlot;
            ResourceCost          = 5;
            CooldownSeconds       = 60f;
            EffectType            = SpellEffectType.Utility;
            EffectMagnitude       = 1f;
            EffectDurationSeconds = 600f;
            SpellLevel            = 5;
            RequiresPreparation   = true;
        }
    }

    public class Wish : Spell
    {
        Wish() : base()
        {
            Id = "wish";
            Name = "Wish";
            ClassName = "Wizard";
            Description = "The mightiest mortal spell. Describe your desired outcome; the "
                        + "server determines the most faithful interpretation.";
            TargetType          = SpellTargetType.Self;
            ResourceType        = SpellResourceType.SpellSlot;
            ResourceCost        = 9;
            CooldownSeconds     = 0f;
            EffectType          = SpellEffectType.Utility;
            EffectMagnitude     = 1f;
            SpellLevel          = 9;
            RequiresPreparation = true;
        }
    }
}
