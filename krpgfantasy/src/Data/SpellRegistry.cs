using System.Collections.Generic;

namespace KRPGLib.Fantasy
{
    // =========================================================================
    //  Resource pool WatchedAttribute keys and default values.
    //  Centralised here so SpellcastingServer, SpellcastingClient, and the
    //  hotbar HUD all reference the same strings.
    // =========================================================================

    public static class SpellResources
    {
        // ── WatchedAttribute keys ─────────────────────────────────────────────
        public const string RageUses = "rage_uses";
        public const string KiPoints = "ki_points";
        public const string SpellSlots = "spell_slots";
        public const string BardicInspiration = "bardic_inspiration";
        public const string LayOnHandsPool = "lay_on_hands";
        public const string FighterSurge = "action_surge";
        public const string FighterWind = "second_wind";

        // ── Defaults ─────────────────────────────────────────────────────────
        public const int DefaultRageUses = 2;
        public const int DefaultKiPoints = 2;
        public const int DefaultSpellSlots = 4;
        public const int DefaultBardicInspiration = 3;
        public const int DefaultLayOnHandsPool = 5;
        public const int DefaultFighterSurge = 1;
        public const int DefaultFighterWind = 1;

        // ── Spell hotbar slot key prefix ──────────────────────────────────────
        public const string HotbarSlotPrefix = "spellslot.";
        public static string HotbarSlotKey(int i) => HotbarSlotPrefix + i;

        // ── Spell preparation slot key prefix ─────────────────────────────────
        public const string PrepSlotPrefix = "spellprep.";
        public static string PrepSlotKey(int i) => PrepSlotPrefix + i;

        /// <summary>
        /// Returns the WatchedAttribute key for the given resource type,
        /// or empty string for None.
        /// </summary>
        public static string KeyFor(SpellResourceType type)
        {
            switch (type)
            {
                case SpellResourceType.RageUse:           return RageUses;
                case SpellResourceType.KiPoint:           return KiPoints;
                case SpellResourceType.SpellSlot:         return SpellSlots;
                case SpellResourceType.BardicInspiration: return BardicInspiration;
                case SpellResourceType.LayOnHandsPool:    return LayOnHandsPool;
                case SpellResourceType.FighterResource:   return FighterSurge;
                default:                                  return string.Empty;
            }
        }

        /// <summary>Returns the default pool value for the given resource type.</summary>
        public static int DefaultFor(SpellResourceType type)
        {
            switch (type)
            {
                case SpellResourceType.RageUse:           return DefaultRageUses;
                case SpellResourceType.KiPoint:           return DefaultKiPoints;
                case SpellResourceType.SpellSlot:         return DefaultSpellSlots;
                case SpellResourceType.BardicInspiration: return DefaultBardicInspiration;
                case SpellResourceType.LayOnHandsPool:    return DefaultLayOnHandsPool;
                case SpellResourceType.FighterResource:   return DefaultFighterSurge;
                default:                                  return 0;
            }
        }
    }

    // =========================================================================
    //  SpellRegistry — static factory for all class spell arrays.
    //
    //  Naming convention for Spell.Id:
    //    {classname_lowercase}_{ability_snake_case}
    //
    //  RequiresPreparation is true for all levelled spells (SpellLevel > 0)
    //  that consume a SpellSlot.  Martial class features always have it false.
    // =========================================================================

    public static class SpellRegistry
    {
        public static Dictionary<string, Spell[]> Registry;
        public static Spell[] BarbarianSpells;
        public static Spell[] BardSpells;
        public static Spell[] FighterSpells;
        public static Spell[] MonkSpells;
        public static Spell[] PaladinSpells;
        public static Spell[] RangerSpells;
        public static Spell[] RogueSpells;
        public static Spell[] SorcererSpells;
        public static Spell[] WizardSpells;
        
        // ─────────────────────────────────────────────────────────────────────
        //  Lookup by class name (case-insensitive)
        // ─────────────────────────────────────────────────────────────────────

        public static Spell[] ForClass(string className)
        {
            switch (className?.ToLowerInvariant())
            {
                case "barbarian": return BarbarianSpells;
                case "bard":      return BardSpells;
                case "fighter":   return FighterSpells;
                case "monk":      return MonkSpells;
                case "paladin":   return PaladinSpells;
                case "ranger":    return RangerSpells;
                case "rogue":     return RogueSpells;
                case "sorcerer":  return SorcererSpells;
                case "wizard":    return WizardSpells;
                default:          return null;
            }
        }

        /// <summary>Returns true if the named class uses spell preparation.</summary>
        public static bool ClassUsesPreparation(string className)
        {
            Spell[] spells = ForClass(className);
            if (spells == null) return false;
            foreach (var s in spells)
                if (s.RequiresPreparation) return true;
            return false;
        }
    }
}
