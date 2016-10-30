﻿using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Artemis.Profiles.Lua.Brushes;
using MoonSharp.Interpreter;

namespace Artemis.Profiles.Lua
{
    [MoonSharpUserData]
    public class LuaDrawWrapper
    {
        private readonly DrawingContext _ctx;

        public LuaDrawWrapper(DrawingContext ctx)
        {
            _ctx = ctx;
        }

        public void DrawEllipse(LuaBrush luaBrush, double x, double y, double height, double width)
        {
            x *= 4;
            y *= 4;
            height *= 4;
            width *= 4;

            var center = new Point(x + width/2, y + height/2);
            _ctx.DrawEllipse(luaBrush.Brush, new Pen(), center, width, height);
        }

        public void DrawLine(LuaBrush luaBrush, double startX, double startY, double endX, double endY,
            double thickness)
        {
            startX *= 4;
            startY *= 4;
            endX *= 4;
            endY *= 4;
            thickness *= 4;

            _ctx.DrawLine(new Pen(luaBrush.Brush, thickness), new Point(startX, startY), new Point(endX, endY));
        }

        public void DrawRectangle(LuaBrush luaBrush, double x, double y, double height, double width)
        {
            x *= 4;
            y *= 4;
            height *= 4;
            width *= 4;

            _ctx.DrawRectangle(luaBrush.Brush, new Pen(), new Rect(x, y, width, height));
        }

        public void DrawText(string text, string fontName, int fontSize, LuaBrush luaBrush, double x, double y)
        {
            x *= 4;
            y *= 4;

            var font = Fonts.SystemTypefaces.FirstOrDefault(f => f.FontFamily.ToString() == fontName);
            if (font == null)
                throw new ScriptRuntimeException($"Font '{fontName}' not found");

            var formatted = new FormattedText(text, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, font,
                fontSize, luaBrush.Brush);
            _ctx.DrawText(formatted, new Point(x, y));
        }
    }
}