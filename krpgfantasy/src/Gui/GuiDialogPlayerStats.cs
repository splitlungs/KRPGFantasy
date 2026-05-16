using System;
using Vintagestory.API.Client;
using KRPGLib.Net;

namespace KRPGLib.Fantasy.Stats
{
    /// <summary>
    /// Standalone dialog that lets the player view and assign DnD-style ability score points.
    /// </summary>
    public class GuiDialogPlayerStats : GuiDialog
    {
        // ─── Hotkey ───────────────────────────────────────────────────────────
        public override string ToggleKeyCombinationCode => "krpgstats";

        // ─── Stat definitions ─────────────────────────────────────────────────
        public readonly string[] StatNames = PlayerDataKeys.StatKeys;

        // ─── State ────────────────────────────────────────────────────────────

        /// <summary>Last committed (Applied) values. Updated on Apply.</summary>
        private int[] committedStats = new int[PlayerDataKeys.StatCount];

        /// <summary>Working copy shown in the dialog. Discarded on Cancel.</summary>
        private int[] pendingStats = new int[PlayerDataKeys.StatCount];

        /// <summary>
        /// Total point budget available to the player.
        /// Set this from WatchedAttributes before calling TryOpen().
        /// Default: 27 (standard 5e point-buy pool).
        /// </summary>
        public int TotalPoints = 27;

        /// <summary>
        /// Sum of committedStats at the moment TryOpen() was called.
        /// Used as the baseline for delta-based budget calculation.
        /// </summary>
        private int pointsBaselineSum;

        // ─── Events ───────────────────────────────────────────────────────────

        /// <summary>
        /// Raised when the player clicks Apply.
        /// The int[] argument contains all six confirmed stat values in
        /// StatNames order (Str, Dex, Con, Int, Wis, Cha).
        /// Hook this to send a C2S_StatsPacket to the server.
        /// </summary>
        public event Action<string, int[]> OnStatsApplied;

        // ─── Layout constants (virtual pixels) ───────────────────────────────

        private const double PadOuter = 10;
        private const double LabelW = 140;
        private const double ValueW = 46;
        private const double BtnW = 28;
        private const double BtnH = 20;    // button visual height — centred in RowH
        private const double BtnOffY = 2.0;   // vertical nudge to align with text baseline
        private const double BtnGap = 4;
        private const double RowH = 30;
        private const double HeaderH = 28;
        private const double FooterH = 40;

        // Total dialog content width
        private const double DialogW  = LabelW + ValueW + BtnW * 2 + BtnGap + PadOuter * 2;

        // ─── Constructor ─────────────────────────────────────────────────────

        public GuiDialogPlayerStats(ICoreClientAPI capi) : base(capi)
        {
            for (int i = 0; i < PlayerDataKeys.StatCount; i++)
                committedStats[i] = PlayerDataKeys.StatDefault;

            Array.Copy(committedStats, pendingStats, PlayerDataKeys.StatCount);
            this.capi = capi;
            SetupDialog();
        }

        // ─── Public API ───────────────────────────────────────────────────────

        /// <summary>
        /// Push a fresh set of stat values from the server into the dialog.
        /// Safe to call whether the dialog is open or closed.
        /// </summary>
        public void SetStats(int[] values)
        {
            if (values == null || values.Length != PlayerDataKeys.StatCount)
                throw new ArgumentException(
                    $"Expected exactly {PlayerDataKeys.StatCount} stat values.", nameof(values));

            Array.Copy(values, committedStats, PlayerDataKeys.StatCount);
            Array.Copy(values, pendingStats, PlayerDataKeys.StatCount);

            if (IsOpened())
                RefreshAllDisplays();
        }

        // ─── GuiDialog overrides ─────────────────────────────────────────────

        public override bool DisableMouseGrab => true;

        // public override bool TryOpen()
        // {
        //     // Reset pending to committed so the player always starts fresh. - Why?
        //     // Array.Copy(committedStats, pendingStats, PlayerDataKeys.StatCount);
// 
        //     // Capture the committed sum as the spending baseline.
        //     // Budget is measured as net increases above this value.
        //     // pointsBaselineSum = SumStats(committedStats);
// 
        //     ComposeDialog();
        //     return base.TryOpen();
        // }

        // ─── Composition ─────────────────────────────────────────────────────

        public void SetupDialog()
        {
            // ── Column X origins ──────────────────────────────────────────────
            double xLabel = PadOuter;
            double xValue = xLabel + LabelW;
            double xMinus = xValue + ValueW;
            double xPlus = xMinus + BtnW + BtnGap;

            // ── Vertical rhythm ───────────────────────────────────────────────
            double titleH = GuiStyle.TitleBarHeight;
            double topPad = 6.0;
            double sectionGap = 10.0;
            double footerGap = 8.0;

            double contentStartY = titleH + topPad;
            double statsStartY = contentStartY + HeaderH;
            double pointsStartY = statsStartY + RowH * PlayerDataKeys.StatCount + sectionGap;
            double footerY = pointsStartY + RowH * 2 + footerGap;
            double innerH = footerY + FooterH;

            // ── Bounds ────────────────────────────────────────────────────────
            ElementBounds dialogBounds = ElementStdBounds
                .AutosizedMainDialog
                .WithAlignment(EnumDialogArea.CenterMiddle);

            ElementBounds bgBounds = ElementBounds.Fill
                .WithFixedPadding(GuiStyle.ElementToDialogPadding);
            bgBounds.BothSizing = ElementSizing.FitToChildren;

            ElementBounds innerBounds = ElementBounds.Fixed(0, 0, DialogW, innerH);

            // ── Fonts ─────────────────────────────────────────────────────────
            CairoFont headerFont = CairoFont.WhiteSmallText();
            CairoFont labelFont = CairoFont.WhiteSmallText();
            CairoFont valueFont = CairoFont.WhiteSmallText();
            CairoFont detailFont = CairoFont.WhiteDetailText();

            // ── Composer ──────────────────────────────────────────────────────
            GuiComposer composer = capi.Gui
                .CreateCompo("playerstats", dialogBounds)
                .AddShadedDialogBG(bgBounds, withTitleBar: true)
                .AddDialogTitleBar("Player Stats", OnTitleBarClose)
                .BeginChildElements(bgBounds)
                    .BeginChildElements(innerBounds);

            // ── Column headers ────────────────────────────────────────────────
            composer
                .AddStaticText(
                    "Ability Score",
                    headerFont,
                    ElementBounds.Fixed(xLabel, contentStartY, LabelW, HeaderH))
                .AddStaticText(
                    "Value",
                    headerFont,
                    ElementBounds.Fixed(xValue, contentStartY, ValueW, HeaderH));

            // ── Stat rows ─────────────────────────────────────────────────────
            for (int i = 0; i < PlayerDataKeys.StatCount; i++)
            {
                int    ci   = i;   // captured for lambda
                double rowY = statsStartY + RowH * i;

                composer
                    .AddStaticText(
                        StatNames[i],
                        labelFont,
                        ElementBounds.Fixed(xLabel, rowY, LabelW, RowH))
                    .AddDynamicText(
                        pendingStats[i].ToString(),
                        valueFont,
                        ElementBounds.Fixed(xValue, rowY, ValueW, RowH),
                        key: "statval-" + i)
                    .AddSmallButton(
                        "−",
                        () => OnClickMinus(ci),
                        ElementBounds.Fixed(xMinus, rowY + BtnOffY, BtnW, BtnH),
                        EnumButtonStyle.Normal,
                        key: "btnminus-" + i)
                    .AddSmallButton(
                        "+",
                        () => OnClickPlus(ci),
                        ElementBounds.Fixed(xPlus, rowY + BtnOffY, BtnW, BtnH),
                        EnumButtonStyle.Normal,
                        key: "btnplus-" + i);
            }

            // ── Points summary ────────────────────────────────────────────────
            // Values are placed in the '+' button column so they stay aligned
            // with the rest of the numeric column.
            double pointsLabelW = xPlus - xLabel - 4;

            composer
                .AddStaticText(
                    "Points Available:",
                    detailFont,
                    ElementBounds.Fixed(xLabel, pointsStartY, pointsLabelW, RowH))
                .AddDynamicText(
                    TotalPoints.ToString(),
                    valueFont,
                    ElementBounds.Fixed(xPlus, pointsStartY, BtnW, RowH),
                    key: "pointsAvailable")
                .AddStaticText(
                    "Points Remaining:",
                    detailFont,
                    ElementBounds.Fixed(xLabel, pointsStartY + RowH, pointsLabelW, RowH))
                .AddDynamicText(
                    PointsRemaining.ToString(),
                    valueFont,
                    ElementBounds.Fixed(xPlus, pointsStartY + RowH, BtnW, RowH),
                    key: "pointsRemaining");

            // ── Apply / Cancel ────────────────────────────────────────────────
            double applyW = 80;
            double cancelW = 80;
            double totalBtnW = applyW + 8 + cancelW;
            double btnStartX = (DialogW - totalBtnW) / 2.0;

            composer
                .AddSmallButton(
                    "Apply",
                    OnClickApply,
                    ElementBounds.Fixed(btnStartX, footerY, applyW, 28),
                    EnumButtonStyle.Normal,
                    key: "btnApply")
                .AddSmallButton(
                    "Cancel",
                    OnClickCancel,
                    ElementBounds.Fixed(btnStartX + applyW + 8, footerY, cancelW, 28),
                    EnumButtonStyle.Normal,
                    key: "btnCancel");

            SingleComposer = composer
                .EndChildElements()
                .EndChildElements()
                .Compose();
        }

        // ─── Button handlers ──────────────────────────────────────────────────

        private bool OnClickMinus(int index)
        {
            if (pendingStats[index] <= PlayerDataKeys.StatMin) return true;

            pendingStats[index]--;
            UpdateStatDisplay(index);
            UpdatePointsDisplay();
            return true;
        }

        private bool OnClickPlus(int index)
        {
            if (pendingStats[index] >= PlayerDataKeys.StatMax) return true;
            if (PointsRemaining <= 0)           return true;   // budget exhausted

            pendingStats[index]++;
            UpdateStatDisplay(index);
            UpdatePointsDisplay();
            return true;
        }
        private bool OnClickApply()
        {
            string pID = capi.World.Player.PlayerUID;
            Array.Copy(pendingStats, committedStats, PlayerDataKeys.StatCount);
            StatsPacket pkt = new StatsPacket() { PlayerUID = pID, Values = committedStats, PointsSpent = this.PointsSpent};
            KRPGFantasyNetSystem.cChannel.SendPacket(pkt);
            TryClose();
            return true;
        }

        private bool OnClickCancel()
        {
            Array.Copy(committedStats, pendingStats, PlayerDataKeys.StatCount);
            TryClose();
            return true;
        }

        private void OnTitleBarClose() => OnClickCancel();

        // ─── Display helpers ──────────────────────────────────────────────────

        /// <summary>Net points spent since the dialog was opened.</summary>
        private int PointsSpent => SumStats(pendingStats) - pointsBaselineSum;

        /// <summary>Points the player can still spend.</summary>
        private int PointsRemaining => TotalPoints - PointsSpent;

        private static int SumStats(int[] stats)
        {
            int total = 0;
            foreach (int v in stats) total += v;
            return total;
        }

        private void UpdateStatDisplay(int index)
        {
            SingleComposer
                .GetDynamicText("statval-" + index)
                .SetNewText(pendingStats[index].ToString());
        }

        private void UpdatePointsDisplay()
        {
            SingleComposer.GetDynamicText("pointsAvailable")
                .SetNewText(TotalPoints.ToString());
            SingleComposer.GetDynamicText("pointsRemaining")
                .SetNewText(PointsRemaining.ToString());
        }

        private void RefreshAllDisplays()
        {
            for (int i = 0; i < PlayerDataKeys.StatCount; i++)
                UpdateStatDisplay(i);
            UpdatePointsDisplay();
        }
    }
}