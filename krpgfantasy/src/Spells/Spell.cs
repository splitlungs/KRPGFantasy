namespace KRPGLib.Fantasy
{
    /// <summary>
    /// The type of resource consumed when a spell or ability is used.
    /// Every FantasyClass maps to one or more of these.
    /// </summary>
    public enum SpellResourceType
    {
        /// <summary>No resource cost — usable freely (subject to cooldown only).</summary>
        None = 0,

        /// <summary>Barbarian rage uses (tracked in WatchedAttributes).</summary>
        RageUse = 1,

        /// <summary>Monk ki points.</summary>
        KiPoint = 2,

        /// <summary>
        /// Generic spell slot (Bard, Paladin, Ranger, Sorcerer, Wizard, Cleric).
        /// Spells that consume a slot AND require preparation use this type.
        /// </summary>
        SpellSlot = 3,

        /// <summary>Bard Bardic Inspiration uses.</summary>
        BardicInspiration = 4,

        /// <summary>Paladin Lay on Hands pool (HP value, not a count).</summary>
        LayOnHandsPool = 5,

        /// <summary>Fighter Second Wind / Action Surge uses.</summary>
        FighterResource = 6,
    }

    /// <summary>What the spell targets.</summary>
    public enum SpellTargetType
    {
        /// <summary>Affects only the caster.</summary>
        Self = 0,

        /// <summary>Targets a single entity the player is looking at.</summary>
        SingleEntity = 1,

        /// <summary>Affects all entities within <see cref="Spell.RangeMetres"/>.</summary>
        AreaOfEffect = 2,
    }

    /// <summary>Broad category used by the server's effect dispatcher.</summary>
    public enum SpellEffectType
    {
        Buff    = 0,
        Debuff  = 1,
        Damage  = 2,
        Heal    = 3,
        Utility = 4,
    }

    /// <summary>
    /// Data model for a single spell or class ability.
    ///
    /// Instances are immutable descriptions of what the ability does.
    /// Runtime state (cooldowns, resource counts, preparation) lives server-side
    /// in <see cref="SpellcastingServer"/> and in WatchedAttributes.
    /// </summary>
    public abstract class Spell : ISpell
    {
        // ─── Identity ─────────────────────────────────────────────────────────

        /// <summary>
        /// Unique internal identifier used as the network key and WatchedAttribute
        /// key suffix. Must be lowercase with no spaces. Example: "wizard_fireball"
        /// </summary>
        public string Id { get; set; }

        /// <summary>Display name shown on the hotbar slot and preparation grid.</summary>
        public string Name { get; set; }

        /// <summary>Tooltip / detail panel description.</summary>
        public string Description { get; set; }

        /// <summary>The FantasyClass this ability belongs to.</summary>
        public string ClassName { get; set; }

        // ─── Targeting ────────────────────────────────────────────────────────

        public SpellTargetType TargetType { get; set; } = SpellTargetType.Self;

        /// <summary>Range in metres. Ignored for <see cref="SpellTargetType.Self"/>.</summary>
        public float RangeMetres { get; set; } = 5f;

        // ─── Resource cost ────────────────────────────────────────────────────

        public SpellResourceType ResourceType { get; set; } = SpellResourceType.None;

        /// <summary>
        /// Amount of the resource consumed per cast.
        /// For <see cref="SpellResourceType.SpellSlot"/> this is the slot level (1–9).
        /// </summary>
        public int ResourceCost { get; set; } = 0;

        // ─── Preparation ──────────────────────────────────────────────────────

        /// <summary>
        /// When true, this spell must be loaded into a preparation slot before
        /// it can be cast.  Only classes with preparation mechanics (Wizard,
        /// Sorcerer, Cleric, Druid, etc.) will typically have preparable spells.
        ///
        /// Non-preparable spells (cantrips, class features like Rage) are always
        /// available and are not shown in <see cref="GuiDialogSpellPreparation"/>.
        /// </summary>
        public bool RequiresPreparation { get; set; } = false;

        /// <summary>
        /// Spell level (1–9).  Determines which preparation slot tiers can hold it.
        /// 0 = cantrip (never requires preparation).
        /// </summary>
        public int SpellLevel { get; set; } = 0;

        // ─── Timing ───────────────────────────────────────────────────────────

        /// <summary>Cooldown in seconds before this ability can be used again.</summary>
        public float CooldownSeconds { get; set; } = 0f;

        // ─── Effect ───────────────────────────────────────────────────────────

        public SpellEffectType EffectType { get; set; } = SpellEffectType.Utility;

        /// <summary>
        /// Magnitude of the primary effect (damage, heal amount, buff value, etc.).
        /// Interpreted by the server's effect dispatcher.
        /// </summary>
        public float EffectMagnitude { get; set; } = 0f;

        /// <summary>Duration in seconds for buff/debuff effects. 0 = instant.</summary>
        public float EffectDurationSeconds { get; set; } = 0f;

        // ─── Display ─────────────────────────────────────────────────────────

        /// <summary>
        /// Short label shown inside the hotbar and preparation slots (≤ 5 chars).
        /// Defaults to the first five characters of Name.
        /// </summary>
        public string SlotLabel => Name?.Length > 5 ? Name.Substring(0, 5) : Name;

        /// <summary>Kept for hotbar backward-compatibility.</summary>
        public string HotbarLabel => SlotLabel;
    }
}