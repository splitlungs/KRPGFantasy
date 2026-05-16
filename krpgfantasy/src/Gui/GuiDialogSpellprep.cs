using System;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;

namespace KRPGLib.Fantasy
{
    /// <summary>
    /// Spell preparation dialog for selecting prepared abilities/spells.
    /// Designed for Vintage Story 1.22.2
    /// </summary>
    public class GuiDialogSpellPrep : GuiDialog
    {
        /// <summary>
        /// Source spell list (left side)
        /// </summary>
        private readonly InventoryGeneric sourceInventory;
        /// <summary>
        /// Prepared spell slots (middle/right)
        /// </summary>
        private readonly InventoryGeneric preparedInventory;
        private readonly Action<ItemSlot[], ItemSlot[]> onApply;
        private GuiComposer composer;
        private const string LeftSlotPrefix = "spellsource-";
        private const string PreparedSlotPrefix = "preparedslot-";
        private readonly int preparedColumns = 4;
        private readonly int preparedRows = 3;
        public override string ToggleKeyCombinationCode => "krpgspellprep";

        public GuiDialogSpellPrep(ICoreClientAPI capi, ItemSlot[] availableSpells, int preparedSlotCount, Action<ItemSlot[], ItemSlot[]> onApply = null) 
            : base(capi)
        {
            this.capi = capi;
            this.onApply = onApply;

            if (preparedSlotCount <= 0)
            {
                preparedSlotCount = 1;
            }

            sourceInventory = new InventoryGeneric(availableSpells.Length, "spellprep-source", null, null);
            preparedInventory = new InventoryGeneric(preparedSlotCount, "spellprep-prepared", null, null);

            for (int i = 0; i < availableSpells.Length; i++)
            {
                if (availableSpells[i]?.Itemstack != null)
                {
                    sourceInventory[i].Itemstack = availableSpells[i].Itemstack.Clone();
                }
            }

            SetupDialog();
        }

        private void SetupDialog()
        {
            ElementBounds bgBounds = ElementStdBounds
                .AutosizedMainDialog
                .WithAlignment(EnumDialogArea.CenterMiddle);

            ElementBounds dialogBounds = bgBounds
                .ForkBoundingParent();

            //
            // LEFT PANEL - SPELL LIST
            //
            ElementBounds leftTitleBounds = ElementBounds
                .Fixed(20, 15, 220, 30);

            ElementBounds leftSlotBounds = ElementBounds
                .Fixed(20, 50, 220, 300);

            //
            // RIGHT PANEL - PREPARED SPELLS
            //
            ElementBounds prepTitleBounds = ElementBounds
                .Fixed(280, 15, 320, 30);

            ElementBounds prepSlotsBounds = ElementBounds
                .Fixed(280, 50, 320, 300);

            //
            // BUTTONS
            //
            ElementBounds applyButtonBounds = ElementBounds
                .Fixed(280, 370, 140, 35);

            ElementBounds cancelButtonBounds = ElementBounds
                .Fixed(440, 370, 140, 35);

            composer = capi.Gui
                .CreateCompo("spelldialogprep", dialogBounds)
                .AddShadedDialogBG(bgBounds)
                .AddDialogTitleBar(
                    Lang.Get("Spell Preparation"),
                    OnTitleBarClose
                )

                //
                // LEFT SIDE LABEL
                //
                .AddStaticText(
                    Lang.Get("Available Spells"),
                    CairoFont.WhiteSmallText(),
                    leftTitleBounds
                )

                //
                // RIGHT SIDE LABEL
                //
                .AddStaticText(
                    Lang.Get("Prepared Spells"),
                    CairoFont.WhiteSmallText(),
                    prepTitleBounds
                );

            //
            // LEFT SIDE SPELL LIST
            //
            double slotY = 50;

            for (int i = 0; i < sourceInventory.Count; i++)
            {
                ElementBounds slotBounds = ElementStdBounds
                    .SlotGrid(EnumDialogArea.None, 20, slotY, 1, 1);

                composer.AddItemSlotGrid(
                    sourceInventory,
                    SendInvPacket,
                    1,
                    new int[] { i },
                    slotBounds,
                    LeftSlotPrefix + i
                );

                slotY += 45;
            }

            //
            // PREPARED SPELL GRID
            //
            int preparedCount = preparedInventory.Count;
            int rows = (int)Math.Ceiling(preparedCount / (float)preparedColumns);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < preparedColumns; col++)
                {
                    int index = row * preparedColumns + col;

                    if (index >= preparedCount)
                    {
                        break;
                    }

                    double x = 280 + (col * 50);
                    double y = 50 + (row * 50);

                    ElementBounds slotBounds = ElementStdBounds
                        .SlotGrid(EnumDialogArea.None, x, y, 1, 1);

                    composer.AddItemSlotGrid(
                        preparedInventory,
                        SendInvPacket,
                        1,
                        new int[] { index },
                        slotBounds,
                        PreparedSlotPrefix + index
                    );
                }
            }

            //
            // BUTTONS
            //
            composer
                .AddSmallButton(
                    Lang.Get("Apply"),
                    OnApplyClicked,
                    applyButtonBounds
                )

                .AddSmallButton(
                    Lang.Get("Cancel"),
                    OnCancelClicked,
                    cancelButtonBounds
                );

            composer.Compose();

            SingleComposer = composer;
        }

        private bool OnApplyClicked()
        {
            onApply?.Invoke(
                sourceInventory.ToArray(),
                preparedInventory.ToArray()
            );

            TryClose();
            return true;
        }

        private bool OnCancelClicked()
        {
            TryClose();
            return true;
        }

        private void OnTitleBarClose()
        {
            TryClose();
        }

        /// <summary>
        /// Required inventory packet sender hook.
        /// Drag/drop works locally without networking here.
        /// </summary>
        private void SendInvPacket(object packet)
        {
            // Intentionally empty for client-side GUI inventory handling.
        }

        public override void OnGuiOpened()
        {
            base.OnGuiOpened();
        }

        public override void OnGuiClosed()
        {
            base.OnGuiClosed();
        }
    }
}