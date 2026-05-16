using System;
using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace KRPGLib.Fantasy
{
    /// <summary>
    /// A simple spell‑book dialog. It shows a title, a list of spell icons
    /// (one per slot) and a scrollable area for the spell description / upgrade UI.
    /// </summary>
    public class SpellbookDialog : GuiDialog
    {
        // -----------------------------------------------------------------
        // 1️⃣  Required abstract member – the API asks the dialog to expose a
        //     key‑combination that can be used to toggle it.  For a spell‑book
        //     we don’t need one, so we just return null.
        // -----------------------------------------------------------------
        public override string ToggleKeyCombinationCode => null;

        // -----------------------------------------------------------------
        // 2️⃣  Fields that we need to keep a reference to.
        // -----------------------------------------------------------------
        private readonly List<SpellInfo> spells;   // a simple DTO that holds name, texture, cooldown, etc.

        // -----------------------------------------------------------------
        // 3️⃣  Constructor – the only signature that is valid in 1.22.2.
        // -----------------------------------------------------------------
        public SpellbookDialog(ICoreClientAPI capi, List<SpellInfo> spells)
            : base(capi)               // title is just an internal id
        {
            this.capi = capi;
            this.spells = spells;
        }

        // -----------------------------------------------------------------
        // 4️⃣  Build the UI.  All UI is built in OnGuiOpened() – the
        //     Vintage Story compositor works like a fluent builder.
        // -----------------------------------------------------------------
        public override void OnGuiOpened()
        {
            // ----- overall dialog size (you can tweak these) -----
            const int dlgWidth  = 500;
            const int dlgHeight = 350;

            // ----- start the composer -------------------------------------------------
            var composer = capi.Gui.CreateCompo("spellbook", dlgWidth, dlgHeight)
                // background – use a custom texture that looks like a leather book
                .AddDynamicCustomTexture(
                    "spellbook-bg",
                    ElementBounds.Fill,
                    "mytextures/spellbook_bg.png")   // <-- put your texture in assets/mytextures/
                // title ---------------------------------------------------------------
                .AddStaticText(
                    "Spellbook",
                    CairoFont.WhiteMediumText(),
                    ElementBounds.Fixed(20, 20, dlgWidth - 40, 30));

            // ----- scrollable list of spell slots (grid) ---------------------------------
            const int slotsPerRow = 5;
            const int slotSize    = 48;
            const int slotPadding = 8;

            // calculate the area that will hold the grid
            int gridX = 20;
            int gridY = 70;
            int gridWidth  = slotsPerRow * (slotSize + slotPadding) - slotPadding;
            int gridHeight = ((spells.Count + slotsPerRow - 1) / slotsPerRow) *
                             (slotSize + slotPadding) - slotPadding;

            // make the grid scrollable in case we have many spells
            composer.BeginScrollable(
                    ElementBounds.Fixed(gridX, gridY, gridWidth, gridHeight)
                        .WithFixedPadding(0, 0));

            // add each spell slot
            for (int i = 0; i < spells.Count; i++)
            {
                var spell = spells[i];

                // a dummy ItemSlot that only stores the spell ID – you can replace this
                // with a custom Slot class if you need more data.
                var slot = new ItemSlot(new DummyInventory(spell.Id), 0);

                // calculate position inside the grid
                int col = i % slotsPerRow;
                int row = i / slotsPerRow;
                double slotX = col * (slotSize + slotPadding);
                double slotY = row * (slotSize + slotPadding);

                // add the visual slot (the texture will be drawn by the slot’s
                // custom renderer – see the comment below)
                composer.AddItemSlot(
                    slot,
                    ElementBounds.Fixed(slotX, slotY, slotSize, slotSize))
                    // click on a slot opens the detailed view for that spell
                    .OnMouseUp((e) =>
                    {
                        if (e.Button == EnumMouseButton.Left)
                        {
                            ShowSpellDetails(spell);
                        }
                    });
            }

            // finish the scrollable area
            composer.EndScrollable();

            // ----- close button (bottom‑right) -----------------------------------------
            composer.AddPlainText(
                    "Close",
                    CairoFont.WhiteSmallText(),
                    ElementBounds.Fixed(dlgWidth - 80, dlgHeight - 40, 60, 30))
                .OnMouseUp((e) => TryClose());

            // ----- finally compose the dialog -----------------------------------------
            SingleComposer = composer.Compose();
        }

        // -----------------------------------------------------------------
        // Helper: show a small overlay with the spell’s description, cooldown, etc.
        // -----------------------------------------------------------------
        private void ShowSpellDetails(SpellInfo spell)
        {
            // you could either open a second dialog or simply show a tooltip‑style
            // panel inside the same dialog.  Here we just reuse the existing
            // composer to add a temporary panel.
            var desc = $"{spell.Name}\n\n{spell.Description}\nCooldown: {spell.Cooldown}s";

            var panel = capi.Gui.CreateCompo($"spelldetail-{spell.Id}", 250, 150)
                .AddStaticText(desc, CairoFont.WhiteMediumText())
                .AddPlainText("Close", CairoFont.WhiteSmallText(),
                    ElementBounds.Fixed(200, 110, 40, 30))
                .OnMouseUp(e => panel.TryClose());

            panel.Compose().TryOpen();
        }

        // -----------------------------------------------------------------
        // The dialog’s title‑bar close button simply calls TryClose()
        // -----------------------------------------------------------------
        public override void OnTitleBarClose()
        {
            TryClose();
        }
    }

    // -----------------------------------------------------------------
    // Simple DTO that carries the data you want to show for each spell.
    // -----------------------------------------------------------------
    public class SpellInfo
    {
        public int    Id;
        public string Name;
        public string Description;
        public string IconTexturePath;   // e.g. "mytextures/spells/fire.png"
        public float  Cooldown;
    }

    // -----------------------------------------------------------------
    // Dummy inventory used only to give the ItemSlot a container.
    // The slot itself does not store any items – we only use it to draw the
    // spell icon via a custom renderer (see comment below).
    // -----------------------------------------------------------------
    public class DummyInventory : InventoryBase
    {
        private readonly int spellId;

        public DummyInventory(int spellId) : base(null)
        {
            this.spellId = spellId;
        }

        public override ItemSlot this[int index] => new ItemSlot(this, index);
    }
}