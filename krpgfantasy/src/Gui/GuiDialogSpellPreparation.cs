/*
using System;
using System.Collections.Generic;
using Cairo;
using Vintagestory.API.Client;
using Vintagestory.API.MathTools;

namespace KRPGLib.Fantasy.Gui
{
    public class GuiDialogSpellPreparation : GuiDialog
    {
        private readonly ICoreClientAPI capi;
        private readonly List<AbilityDefinition> availableAbilities;
        private readonly List<AbilityDefinition> preparedAbilities;
        private readonly Action<List<AbilityDefinition>> onApply;

        private GuiComposer composer;

        private AbilityDefinition draggedAbility;

        private const int SlotSize = 48;
        private const int PreparedColumns = 4;
        private const int PreparedRows = 3;

        public override string ToggleKeyCombinationCode => "krpgspellprep";

        public GuiDialogSpellPreparation(ICoreClientAPI capi) : base(capi)
        {
            this.capi = capi;
            availableAbilities = new List<AbilityDefinition>();
            preparedAbilities = new List<AbilityDefinition>();
            onApply = new Action<List<AbilityDefinition>>();
            this.availableAbilities = availableAbilities;
            this.preparedAbilities = preparedAbilities;
            this.onApply = onApply;

            while (this.preparedAbilities.Count < PreparedColumns * PreparedRows)
            {
                this.preparedAbilities.Add(null);
            }

            SetupDialog();
        }

        private void SetupDialog()
        {
            ElementBounds dialogBounds = ElementStdBounds.AutosizedMainDialog
                .WithAlignment(EnumDialogArea.CenterMiddle)
                .WithFixedSize(700, 540);

            composer = capi.Gui
                .CreateCompo("krpgfantasy_spellprep", dialogBounds)
                .AddShadedDialogBG(ElementBounds.Fill)
                .AddDialogTitleBar("Spell Preparation", OnTitleBarClose)
                .AddDynamicCustomDraw(ElementBounds.Fixed(20, 40, 280, 420), DrawAvailableAbilities)
                .AddDynamicCustomDraw(ElementBounds.Fixed(340, 40, 240, 240), DrawPreparedAbilities)
                .AddSmallButton("Apply", OnApplyClicked, ElementBounds.Fixed(340, 470, 100, 35))
                .AddSmallButton("Cancel", OnCancelClicked, ElementBounds.Fixed(460, 470, 100, 35))
                .Compose();
        }

        public override void OnMouseDown(MouseEvent args)
        {
            base.OnMouseDown(args);

            draggedAbility = GetAbilityAtPosition(args.X, args.Y);

            if (draggedAbility != null)
            {
                args.Handled = true;
            }
        }

        public override void OnMouseUp(MouseEvent args)
        {
            base.OnMouseUp(args);

            if (draggedAbility == null) return;

            int slotIndex = GetPreparedSlotAtPosition(args.X, args.Y);

            if (slotIndex >= 0)
            {
                preparedAbilities[slotIndex] = draggedAbility;
            }

            draggedAbility = null;
        }

        private void DrawAvailableAbilities(Context ctx, ImageSurface surface, ElementBounds bounds)
        {
            double y = bounds.drawY;

            DrawText(ctx, "Available Abilities", bounds.drawX, y);

            y += 30;

            foreach (AbilityDefinition ability in availableAbilities)
            {
                DrawAbilityBox(ctx, bounds.drawX, y, 240, 36, ability.Name);
                y += 42;
            }
        }

        private void DrawPreparedAbilities(Context ctx, ImageSurface surface, ElementBounds bounds)
        {
            DrawText(ctx, "Prepared", bounds.drawX, bounds.drawY - 8);

            for (int i = 0; i < preparedAbilities.Count; i++)
            {
                int col = i % PreparedColumns;
                int row = i / PreparedColumns;

                double x = bounds.drawX + col * (SlotSize + 8);
                double y = bounds.drawY + row * (SlotSize + 8);

                GuiElement.RoundRectangle(ctx, x, y, SlotSize, SlotSize, 4);

                ctx.SetSourceRGBA(0.2, 0.2, 0.2, 1);
                ctx.FillPreserve();

                ctx.SetSourceRGBA(0.5, 0.5, 0.5, 1);
                ctx.LineWidth = 1;
                ctx.Stroke();

                AbilityDefinition ability = preparedAbilities[i];

                if (ability != null)
                {
                    DrawText(ctx, ability.Name, x + 4, y + 28, 12);
                }
            }
        }

        private void DrawAbilityBox(
            Context ctx,
            double x,
            double y,
            double width,
            double height,
            string text
        )
        {
            GuiElement.RoundRectangle(ctx, x, y, width, height, 4);

            ctx.SetSourceRGBA(0.15, 0.15, 0.15, 1);
            ctx.FillPreserve();

            ctx.SetSourceRGBA(0.4, 0.4, 0.4, 1);
            ctx.LineWidth = 1;
            ctx.Stroke();

            DrawText(ctx, text, x + 8, y + 24);
        }

        private void DrawText(
            Context ctx,
            string text,
            double x,
            double y,
            double size = 16
        )
        {
            ctx.Save();

            ctx.SelectFontFace(
                "Sans",
                FontSlant.Normal,
                FontWeight.Normal
            );

            ctx.SetFontSize(size);
            ctx.SetSourceRGBA(1, 1, 1, 1);

            ctx.MoveTo(x, y);
            ctx.ShowText(text);

            ctx.Restore();
        }

        private AbilityDefinition GetAbilityAtPosition(double mouseX, double mouseY)
        {
            double startX = 20;
            double startY = 70;

            for (int i = 0; i < availableAbilities.Count; i++)
            {
                double y = startY + i * 42;

                if (mouseX >= startX &&
                    mouseX <= startX + 240 &&
                    mouseY >= y &&
                    mouseY <= y + 36)
                {
                    return availableAbilities[i];
                }
            }

            return null;
        }

        private int GetPreparedSlotAtPosition(double mouseX, double mouseY)
        {
            double startX = 340;
            double startY = 40;

            for (int i = 0; i < preparedAbilities.Count; i++)
            {
                int col = i % PreparedColumns;
                int row = i / PreparedColumns;

                double x = startX + col * (SlotSize + 8);
                double y = startY + row * (SlotSize + 8);

                if (mouseX >= x &&
                    mouseX <= x + SlotSize &&
                    mouseY >= y &&
                    mouseY <= y + SlotSize)
                {
                    return i;
                }
            }

            return -1;
        }

        private bool OnApplyClicked()
        {
            onApply?.Invoke(preparedAbilities);
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
    }

    public class AbilityDefinition
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
*/