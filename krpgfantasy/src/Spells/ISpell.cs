namespace KRPGLib.Fantasy
{
    public interface ISpell
    {

        string Id { get; set; }
        /// <summary>Display name shown on the hotbar slot and preparation grid.</summary>
        string Name { get; set; }
        /// <summary>Tooltip / detail panel description.</summary>
        string Description { get; set; }
        /// <summary>The FantasyClass this ability belongs to.</summary>
        string ClassName { get; set; }

        // ─── Targeting ────────────────────────────────────────────────────────

        SpellTargetType TargetType { get; set; }
        /// <summary>Range in metres. Ignored for <see cref="SpellTargetType.Self"/>.</summary>
        float RangeMetres { get; set; }

        // ─── Resource cost ────────────────────────────────────────────────────

        SpellResourceType ResourceType { get; set; }
        /// <summary>
        /// Amount of the resource consumed per cast.
        /// For <see cref="SpellResourceType.SpellSlot"/> this is the slot level (1–9).
        /// </summary>
        int ResourceCost { get; set; }

        // ─── Preparation ──────────────────────────────────────────────────────

        /// <summary>
        /// When true, this spell must be loaded into a preparation slot before
        /// it can be cast.  Only classes with preparation mechanics (Wizard,
        /// Sorcerer, Cleric, Druid, etc.) will typically have preparable spells.
        ///
        /// Non-preparable spells (cantrips, class features like Rage) are always
        /// available and are not shown in <see cref="GuiDialogSpellPreparation"/>.
        /// </summary>
        bool RequiresPreparation { get; set; }

        /// <summary>
        /// Spell level (1–9).  Determines which preparation slot tiers can hold it.
        /// 0 = cantrip (never requires preparation).
        /// </summary>
        int SpellLevel { get; set; }

        // ─── Timing ───────────────────────────────────────────────────────────

        /// <summary>Cooldown in seconds before this ability can be used again.</summary>
        float CooldownSeconds { get; set; }

        // ─── Effect ───────────────────────────────────────────────────────────

        SpellEffectType EffectType { get; set; }
        /// <summary>
        /// Magnitude of the primary effect (damage, heal amount, buff value, etc.).
        /// Interpreted by the server's effect dispatcher.
        /// </summary>
        float EffectMagnitude { get; set; }

        /// <summary>Duration in seconds for buff/debuff effects. 0 = instant.</summary>
        float EffectDurationSeconds { get; set; }

        // ─── Display ─────────────────────────────────────────────────────────

        /// <summary>
        /// Short label shown inside the hotbar and preparation slots (≤ 5 chars).
        /// Defaults to the first five characters of Name.
        /// </summary>
        string SlotLabel => Name?.Length > 5 ? Name.Substring(0, 5) : Name;

        /// <summary>Kept for hotbar backward-compatibility.</summary>
        string HotbarLabel => SlotLabel;
    }
}