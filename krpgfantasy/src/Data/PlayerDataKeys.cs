namespace KRPGLib.Fantasy.Stats
{
    /// <summary>
    /// Centralised string keys used when reading and writing player data to
    /// <c>IPlayer.Entity.WatchedAttributes</c>.
    ///
    /// WatchedAttributes are automatically synchronised from the server to the
    /// owning client, so the client can read these values for display without
    /// an explicit sync packet.
    ///
    /// All keys are namespaced under "dndmod" to avoid collisions with other mods.
    /// </summary>
    public static class PlayerDataKeys
    {
        public const string RootAttribute = "krpgfantasy";
        // ── Stats ─────────────────────────────────────────────────────────────
        public const string StatStr = "strength";
        public const string StatDex = "dexterity";
        public const string StatCon = "constitution";
        public const string StatInt = "intelligence";
        public const string StatWis = "wisdom";
        public const string StatCha = "charisma";

        /// <summary>
        /// Ordered array matching <c>GuiDialogPlayerStats.StatNames</c>.
        /// Index 0 = Strength … Index 5 = Charisma.
        /// </summary>
        public static readonly string[] StatKeys =
        {
            StatStr,
            StatDex,
            StatCon,
            StatInt,
            StatWis,
            StatCha
        };

        public const int StatCount = 6;
        public const int StatDefault = 10;
        public const int StatMin = 1;
        public const int StatMax = 20;

        // ── Stat points ───────────────────────────────────────────────────────
        /// <summary>Total stat points the player is allowed to distribute.</summary>
        public const int StatPointsTotal = 27;
        public const string StatPointsTotalKey = "sptotal";

        // ── Class ─────────────────────────────────────────────────────────────
        /// <summary>Chosen DnD class name, or empty string if none selected.</summary>
        public const string ClassName = "charclass";

        // ── Feat points ───────────────────────────────────────────────────────
        /// <summary>Total feat points the player has earned.</summary>
        public const string FeatPointsTotal = "fptotal";

        // ── Feats ─────────────────────────────────────────────────────────────
        /// <summary>
        /// Prefix for feat unlock flags.
        /// Full key = <c>FeatPrefix + featName</c>.
        /// Value is stored as int (0 = locked, 1 = unlocked).
        /// </summary>
        public const string FeatPrefix = "feat-";

        // ── Server → client response channel ──────────────────────────────────
        public const string NetworkChannel = "krpgstats";
    }
}