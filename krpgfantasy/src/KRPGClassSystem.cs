using System;
using System.IO;
using Vintagestory.API.Common;
using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Net;
using Vintagestory.API.Common.Entities;
using ProtoBuf;
using KRPGLib.Fantasy.Stats;
using KRPGLib.Net;
using System.Linq;
using KRPGLib.Fantasy.Feats;

namespace KRPGLib.Fantasy.Classes
{
    /// <summary>
    /// Server‑authoritative system that stores DnD‑style attributes
    /// for each player after they pick a class.
    /// </summary>
    public class KRPGClassSystem : ModSystem
    {
        internal ICoreAPI Api;
        internal ICoreServerAPI sApi;
        internal ICoreClientAPI cApi;
        // private IServerNetworkChannel channel;
        private GuiDialogPlayerStats StatsDialog;
        private GuiDialogFeatTree FeatsDialog;
        private GuiDialogSpellPrep SpellsDialog;
        private KRPGFantasyNetSystem cNetManager;
        private KRPGFantasyNetSystem sNetManager;
        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            Api = api;
            Api.Logger.StoryEvent("Remembering powers...");
        }
        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);
            cApi = api;
            cNetManager = api.ModLoader.GetModSystem<KRPGFantasyNetSystem>();
            // ── Dialogs ───────────────────────────────────────────────────────
            StatsDialog = new GuiDialogPlayerStats(api);
            FeatsDialog = new GuiDialogFeatTree(api);
            // SpellsDialog = new GuiDialogSpellPrep(api);
            // ── Dialog events ─────────────────────────────────────────────────
            // StatsDialog.OnStatsApplied += OnClientStatsApplied;
            // ── Hotkeys ───────────────────────────────────────────────────────
            api.Input.RegisterHotKey("krpgstats", "Open KRPG Stats", GlKeys.O, HotkeyType.GUIOrOtherControls);
            api.Input.SetHotKeyHandler("krpgstats", _ => ToggleStatsDialog());
            api.Input.RegisterHotKey("krpgfeats", "Open KRPG Feats", GlKeys.O, HotkeyType.GUIOrOtherControls, false, true);
            api.Input.SetHotKeyHandler("krpgfeats", _ => ToggleFeatsDialog());
            api.Input.RegisterHotKey("krpgspellprep", "Open KRPG Spells", GlKeys.O, HotkeyType.GUIOrOtherControls, false, true, true);
            api.Input.SetHotKeyHandler("krpgspellprep", _ => ToggleSpellsDialog());

            cApi.Logger.Event("[KRPGClasses] Successfully loaded.");
        }
        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            sApi = api;
            sNetManager = api.ModLoader.GetModSystem<KRPGFantasyNetSystem>();
            sNetManager.C2S_OnStatsPacket += StatsPacketHandler;
            api.Event.PlayerReady += OnPlayerReady;
            sApi.Logger.Event("[KRPGFantasy] Successfully loaded.");
        }
        public override void Dispose()
        {
            if (Api is ICoreClientAPI capi)
            {
                StatsDialog?.TryClose();
            }
            else if (Api is ICoreServerAPI sapi)
            {
                sNetManager.C2S_OnStatsPacket -= StatsPacketHandler;
                sapi.Event.PlayerReady -= OnPlayerReady;
            }
        }
        private void OnClientStatsApplied(long eID, int[] stats)
        {
            // Entity entity = sApi.World.GetEntityById(eID);
            // ITreeAttribute attrs = entity?.WatchedAttributes?.GetOrAddTreeAttribute(RootAttribute);
            // if (attrs == null || stats.Length != PlayerDataKeys.StatKeys.Length) return;
            // foreach (int i in stats)
            // {
            //     SetPlayerStats(attrs, PlayerDataKeys.StatKeys[i], i);
            // }
        }
        private bool ToggleStatsDialog()
        {
            if (StatsDialog.IsOpened())
                StatsDialog.TryClose();
            else
            {
                var attrs = cApi.World.Player?.Entity?.WatchedAttributes?.GetTreeAttribute(PlayerDataKeys.RootAttribute);
                if (attrs == null) return true;
                int[] current = new int[PlayerDataKeys.StatCount];
                for (int i = 0; i < PlayerDataKeys.StatCount; i++)
                {
                    current[i] = attrs.GetInt(PlayerDataKeys.StatKeys[i], PlayerDataKeys.StatDefault);
                }
                StatsDialog.SetStats(current);
                StatsDialog.TryOpen();
            }
            return true; // true = input consumed, don't pass it further

            // if (StatsDialog.IsOpened()) { StatsDialog.TryClose(); return true; }
            // 
            // // Load current values from WatchedAttributes so the dialog reflects
            // // what the server has stored (and what was last auto-synced to us).
            // var attrs = cApi.World.Player?.Entity?.WatchedAttributes?.GetTreeAttribute(RootAttribute);
            // if (attrs == null) return false;
            // 
            // int[] current = new int[PlayerDataKeys.StatCount];
            // for (int i = 0; i < PlayerDataKeys.StatCount; i++)
            //     current[i] = attrs.GetInt(PlayerDataKeys.StatKeys[i], PlayerDataKeys.StatDefault);
            // 
            // StatsDialog.SetStats(current);
            // StatsDialog.TotalPoints = attrs.GetInt(
            //     PlayerDataKeys.StatPointsTotalKey, PlayerDataKeys.StatPointsTotal);
            // 
            // StatsDialog.TryOpen();
            // return true;
        }
        private bool ToggleFeatsDialog()
        {
            if (FeatsDialog.IsOpened())
                FeatsDialog.TryClose();
            else
                FeatsDialog.TryOpen();
            return true; // true = input consumed, don't pass it further
        }
        private bool ToggleSpellsDialog()
        {
            if (SpellsDialog.IsOpened())
                SpellsDialog.TryClose();
            else
                SpellsDialog.TryOpen();
            return true; // true = input consumed, don't pass it further
        }
        private void OnPlayerReady(IServerPlayer player)
        {
            ValidatePlayerStats(player);

            // ClassSelectPacket pkt = new ClassSelectPacket() {ClassName = player.Entity.WatchedAttributes.GetString("characterclass")};
            // channel.SendPacket(pkt, player);
            // sApi.Network.SendEntityPacket(player, player.Entity.EntityId, pkt);
        }
        #region Stats
        /// <summary>
        /// Checks all Player Stat keys, and assigns default if they are not found.
        /// </summary>
        /// <param name="player"></param>
        private void ValidatePlayerStats(IServerPlayer player)
        {
            ITreeAttribute attrs = player?.Entity?.WatchedAttributes?.GetOrAddTreeAttribute(PlayerDataKeys.RootAttribute);
            if (attrs == null)
            {
                Api.Logger.Error("[KRPGClasses] Failed to get or create root attribute tree for {0}.", player.PlayerName);
                return;
            }
            foreach (string s in PlayerDataKeys.StatKeys)
                SetPlayerStat(attrs, s, PlayerDataKeys.StatDefault, false);
            SetPlayerStat(attrs, PlayerDataKeys.StatPointsTotalKey, PlayerDataKeys.StatPointsTotal, false);
            player.Entity.WatchedAttributes.MergeTree(attrs);
        }
        /// <summary>
        /// Helper that writes a default value only when the key does not yet exist. Enable overwrite 
        /// </summary>
        /// <param name="attrs"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="overwrite"></param>
        private void SetPlayerStat(ITreeAttribute attrs, string key, int value, bool overwrite)
        {
            if (!attrs.HasAttribute(key) || overwrite == true)
            {
                attrs.SetInt(key, value);
            }
        }
        public void StatsPacketHandler(IPlayer player, StatsPacket pkt)
        {
            // Prevent players editing each other
            if (player.PlayerUID != pkt.PlayerUID) return;

            ITreeAttribute attrs = player.Entity?.WatchedAttributes?.GetOrAddTreeAttribute(PlayerDataKeys.RootAttribute);
            if (attrs == null || pkt.Values.Length != PlayerDataKeys.StatKeys.Length) return;
            foreach (int i in pkt.Values)
            {
                SetPlayerStat(attrs, PlayerDataKeys.StatKeys[i], i, true);
            }
            int total = attrs.GetInt(PlayerDataKeys.StatPointsTotalKey, 0);
            total = Math.Max(total - pkt.PointsSpent, 0);
            attrs.SetInt(PlayerDataKeys.StatPointsTotalKey, total);
            player.Entity?.WatchedAttributes?.MergeTree(attrs);
        }
        #endregion
        /// <summary>
        /// Handles the packet that arrives from the client after the player selects a class.
        /// </summary>
        private void OnReceiveClassPkt(IServerPlayer fromPlayer, ClassSelectPacket packet)
        {

        }

        // private void OnStatsPacket(IServerPlayer player, StatsPacket packet)
        // {
        //     // ── Basic structural validation ───────────────────────────────────
        //     if (packet?.Values == null || packet.Values.Length != PlayerDataKeys.StatCount)
        //     {
        //         Reject(player, ResponsePacket.RequestType.Stats,
        //             "Malformed stats packet.");
        //         return;
        //     }
// 
        //     // ── Range validation ──────────────────────────────────────────────
        //     foreach (int v in packet.Values)
        //     {
        //         if (v < PlayerDataKeys.StatMin || v > PlayerDataKeys.StatMax)
        //         {
        //             Reject(player, ResponsePacket.RequestType.Stats,
        //                 $"Stat value {v} is outside the allowed range "
        //                 + $"({PlayerDataKeys.StatMin}–{PlayerDataKeys.StatMax}).");
        //             return;
        //         }
        //     }
// 
        //     // ── Point-budget validation ───────────────────────────────────────
        //     var attrs = player.Entity.WatchedAttributes.GetTreeAttribute(RootAttribute);
// 
        //     // Existing committed total (sum of current attribute values)
        //     int committedSum = 0;
        //     foreach (string key in PlayerDataKeys.StatKeys)
        //         committedSum += attrs.GetInt(key, PlayerDataKeys.StatDefault);
// 
        //     int proposedSum = packet.Values.Sum();
        //     int totalBudget = attrs.GetInt(PlayerDataKeys.StatPointsTotalKey, PlayerDataKeys.StatPointsTotal);
// 
        //     // The player may only spend up to totalBudget points above the
        //     // baseline (6 stats × StatDefault) in aggregate.
        //     int baseline   = PlayerDataKeys.StatCount * PlayerDataKeys.StatDefault;
        //     int pointsUsed = proposedSum - baseline;
// 
        //     if (pointsUsed > totalBudget)
        //     {
        //         Reject(player, ResponsePacket.RequestType.Stats,
        //             $"Not enough stat points. Tried to use {pointsUsed} "
        //             + $"but only {totalBudget} are available.");
        //         return;
        //     }
// 
        //     // ── Write ─────────────────────────────────────────────────────────
        //     for (int i = 0; i < PlayerDataKeys.StatCount; i++)
        //         attrs.SetInt(PlayerDataKeys.StatKeys[i], packet.Values[i]);
// 
        //     // WatchedAttributes auto-syncs to the client; no manual send needed.
        //     Accept(player, ResponsePacket.RequestType.Stats);
// 
        //     sApi.Logger.Debug(
        //         "[KRPGClasses] {0}: stats updated ({1})",
        //         player.PlayerName,
        //         string.Join(", ", packet.Values));
        // }
        // private void Accept(IServerPlayer player, ResponsePacket.RequestType kind, string message = "OK")
        // {
        //     sNetManager.SendResponse(player, new ResponsePacket
        //     {
        //         Request = (int)kind,
        //         Success = true,
        //         Message = message
        //     });
        // }
        // 
        // private void Reject(IServerPlayer player, ResponsePacket.RequestType kind, string reason)
        // {
        //     sNetManager.SendResponse(player, new ResponsePacket
        //     {
        //         Request = (int)kind,
        //         Success = false,
        //         Message = reason
        //     });
        // 
        //     sApi.Logger.Warning(
        //         "[KRPGClasses] {0}: rejected {1} — {2}",
        //         player.PlayerName, kind, reason);
        // }
    }
}