using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using System.Collections.Generic;
using Cairo;

namespace KRPGLib.Fantasy.Feats
{
    public class GuiDialogFeatTree : GuiDialog
    {
        public override string ToggleKeyCombinationCode => "krpgfeats";

        const int WIDTH = 800;
        const int HEIGHT = 600;

        const int NODE_W = 110;
        const int NODE_H = 40;

        double[] rowY = { 80, 180, 300, 420 };

        private Dictionary<string, FeatNode> feats = new();
        private Dictionary<string, ElementBounds> nodeBounds = new();

        private ElementBounds dialogBounds;
        private ElementBounds contentBounds;

        private string hoveredFeatCode = null;

        public GuiDialogFeatTree(ICoreClientAPI capi) : base(capi)
        {
            SetupFeats();
            SetupDialog();
        }

        private void SetupFeats()
        {
            feats["A"] = new FeatNode("A", "Rage", "Starting feat") { Unlocked = true };

            feats["B"] = new FeatNode("B", "Strength I", "+Damage", "A");
            feats["C"] = new FeatNode("C", "Agility I", "+Speed", "A");

            feats["D"] = new FeatNode("D", "Power Strike", "Heavy attack", "B");
            feats["E"] = new FeatNode("E", "Endurance", "+HP", "B");
            feats["F"] = new FeatNode("F", "Quick Step", "Dodge", "C");

            feats["G"] = new FeatNode("G", "Berserk", "Rage mode", "D");
            feats["H"] = new FeatNode("H", "Shadowstep", "Teleport", "F");
        }

        private void SetupDialog()
        {
            dialogBounds = ElementBounds.Fixed(0, 0, WIDTH, HEIGHT)
                .WithAlignment(EnumDialogArea.CenterMiddle);

            contentBounds = ElementBounds.Fill;

            var composer = capi.Gui
                .CreateCompo("feattree", dialogBounds)
                .AddDialogBG(ElementBounds.Fill)
                .AddDialogTitleBar("Feat Tree", OnTitleBarClose)
                .BeginChildElements(contentBounds);

            // BACK LAYER (connections)
            composer.AddDynamicCustomDraw(
                dialogBounds,
                (ctx, surface, bounds) => DrawConnections(ctx)
            );

            foreach (KeyValuePair<string, FeatNode> pair in feats)
            {
                
            }
            // Nodes
            AddFeat(composer, "A", WIDTH * 0.5, rowY[0]);

            AddFeat(composer, "B", WIDTH * 0.3, rowY[1]);
            AddFeat(composer, "C", WIDTH * 0.7, rowY[1]);

            AddFeat(composer, "D", WIDTH * 0.2, rowY[2]);
            AddFeat(composer, "E", WIDTH * 0.4, rowY[2]);
            AddFeat(composer, "F", WIDTH * 0.7, rowY[2]);

            AddFeat(composer, "G", WIDTH * 0.25, rowY[3]);
            AddFeat(composer, "H", WIDTH * 0.75, rowY[3]);

            // FRONT LAYER (overlays)
            composer.AddDynamicCustomDraw(
                dialogBounds,
                (ctx, surface, bounds) => DrawNodeOverlays(ctx)
            );

            composer.EndChildElements();
            SingleComposer = composer.Compose();
        }

        private ElementBounds NodeBounds(double centerX, double centerY)
        {
            return ElementBounds.Fixed(
                centerX - NODE_W / 2,
                centerY - NODE_H / 2,
                NODE_W,
                NODE_H
            ).WithParent(contentBounds);
        }

        private void AddFeat(GuiComposer composer, string code, double x, double y)
        {
            var feat = feats[code];

            var bounds = NodeBounds(x, y);
            nodeBounds[code] = bounds;

            composer.AddSmallButton(
                feat.Name,
                () => OnFeatClicked(feat),
                bounds,
                EnumButtonStyle.Normal
            );
        }

        public override void OnRenderGUI(float deltaTime)
        {
            base.OnRenderGUI(deltaTime);

            hoveredFeatCode = null;

            double mouseX = capi.Input.MouseX;
            double mouseY = capi.Input.MouseY;

            foreach (var kvp in nodeBounds)
            {
                if (kvp.Value.PointInside(mouseX, mouseY))
                {
                    hoveredFeatCode = kvp.Key;
                    break;
                }
            }
        }

        private bool CanUnlock(FeatNode feat)
        {
            foreach (var parent in feat.Parents)
            {
                if (!feats[parent].Unlocked) return false;
            }
            return true;
        }

        private bool IsConnectedToHovered(string code)
        {
            if (hoveredFeatCode == null) return false;

            if (code == hoveredFeatCode) return true;

            var hovered = feats[hoveredFeatCode];

            if (hovered.Parents.Contains(code)) return true;

            foreach (var f in feats.Values)
            {
                if (f.Parents.Contains(hoveredFeatCode) && f.Code == code)
                    return true;
            }

            return false;
        }
        bool ShouldHighlightConnection(string parent, string child)
        {
            if (hoveredFeatCode != null)
            {
                return parent == hoveredFeatCode || child == hoveredFeatCode;
            }

            return feats[parent].Unlocked && CanUnlock(feats[child]);
        }
        private void DrawConnections(Context ctx)
        {
            double offsetX = dialogBounds.renderX;
            double offsetY = dialogBounds.renderY;

            foreach (var feat in feats.Values)
            {
                foreach (var parentCode in feat.Parents)
                {
                    if (!nodeBounds.ContainsKey(parentCode)) continue;

                    var fromB = nodeBounds[parentCode];
                    var toB = nodeBounds[feat.Code];

                    double startX = fromB.renderX + fromB.OuterWidth / 2 - offsetX;
                    double startY = fromB.renderY + fromB.OuterHeight - offsetY;

                    double endX = toB.renderX + toB.OuterWidth / 2 - offsetX;
                    double endY = toB.renderY - offsetY;

                    bool highlight = ShouldHighlightConnection(parentCode, feat.Code);

                    if (highlight)
                    {
                        ctx.SetSourceRGBA(1.0, 0.85, 0.3, 1);
                        ctx.LineWidth = 4;
                    }
                    else if (feat.Unlocked)
                    {
                        ctx.SetSourceRGBA(0.2, 1, 0.2, 1);
                        ctx.LineWidth = 3;
                    }
                    else
                    {
                        ctx.SetSourceRGBA(0.6, 0.6, 0.6, 0.6);
                        ctx.LineWidth = 2;
                    }

                    ctx.LineCap = LineCap.Round;

                    double midY = (startY + endY) / 2;

                    ctx.MoveTo(startX, startY);
                    ctx.LineTo(startX, midY);
                    ctx.LineTo(endX, midY);
                    ctx.LineTo(endX, endY);

                    ctx.Stroke();
                }
            }
        }

        private void DrawNodeOverlays(Context ctx)
        {
            double offsetX = dialogBounds.renderX;
            double offsetY = dialogBounds.renderY;

            foreach (var kvp in nodeBounds)
            {
                var code = kvp.Key;
                var bounds = kvp.Value;
                var feat = feats[code];

                double x = bounds.renderX - offsetX;
                double y = bounds.renderY - offsetY;

                // LOCKED overlay
                if (!feat.Unlocked && !CanUnlock(feat))
                {
                    ctx.SetSourceRGBA(0, 0, 0, 0.6);
                    ctx.Rectangle(x, y, bounds.OuterWidth, bounds.OuterHeight);
                    ctx.Fill();
                }
                // AVAILABLE glow
                else if (!feat.Unlocked && CanUnlock(feat))
                {
                    ctx.SetSourceRGBA(1, 0.85, 0.2, 0.25);
                    ctx.Rectangle(x, y, bounds.OuterWidth, bounds.OuterHeight);
                    ctx.Fill();
                }
            }
        }
        private bool OnFeatClicked(FeatNode feat)
        {
            if (feat.Unlocked) return true;

            if (CanUnlock(feat))
            {
                feat.Unlocked = true;

                capi.TriggerIngameError(this, "feat", $"Unlocked: {feat.Name}");

                SingleComposer?.ReCompose();
            }
            else
            {
                capi.TriggerIngameError(this, "feat", $"Requires prerequisites!");
            }

            return true;
        }
        private void OnTitleBarClose()
        {
            TryClose();
        }
    }
}