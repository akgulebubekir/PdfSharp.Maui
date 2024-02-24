namespace PdfSharp.Maui.Renderers.Editors;

[PdfRenderer(ViewType = typeof(Slider))]
public class PdfSliderRenderer : PdfRendererBase<Slider>
{
    protected override void CreateLayout(XGraphics page, Slider view, XRect bounds, double scaleFactor)
    {
        var valueRatio = view.Value / (view.Maximum - view.Minimum);
        var thumbWith = GetProperty<double>(SliderViewProperties.ThumbWith);
        var trackHeight = HasProperty(SliderViewProperties.TrackHeight)
            ? GetProperty<double>(SliderViewProperties.TrackHeight)
            : 5 * scaleFactor;

        if (HasProperty(ViewProperties.BackgroundColor))
        {
            page.DrawRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds.X,
                bounds.Center.Y - (trackHeight / 2), bounds.Width, trackHeight);
        }

        page.DrawRectangle(GetProperty<XBrush>(SliderViewProperties.MinTrackColor), bounds.X,
            bounds.Center.Y - (trackHeight / 2), bounds.Width * valueRatio, trackHeight);

        page.DrawRectangle(GetProperty<XBrush>(SliderViewProperties.MaxTrackColor),
            bounds.X + (bounds.Width * valueRatio),
            bounds.Center.Y - (trackHeight / 2), bounds.Width - (bounds.Width * valueRatio), trackHeight);

        if (HasProperty(SliderViewProperties.ThumbRingColor))
        {
            var thumbRingWith = GetProperty<double>(SliderViewProperties.ThumbRingWidth);
            page.DrawEllipse(GetProperty<XBrush>(SliderViewProperties.ThumbRingColor),
                bounds.X + (bounds.Width * valueRatio) - (thumbRingWith / 2), bounds.Center.Y - (thumbRingWith / 2),
                thumbRingWith,
                thumbRingWith);
        }

        page.DrawEllipse(GetProperty<XBrush>(SliderViewProperties.ThumbColor),
            bounds.X + (bounds.Width * valueRatio) - (thumbWith / 2), bounds.Center.Y - (thumbWith / 2), thumbWith,
            thumbWith);
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, Slider view, XRect bounds, double scaleFactor)
    {
        Properties.Add(SliderViewProperties.ThumbColor, XBrushes.DarkGray);
        Properties.Add(SliderViewProperties.ThumbWith, bounds.Height * 0.7);
        Properties.Add(SliderViewProperties.MinTrackColor, XBrushes.Gray);
        Properties.Add(SliderViewProperties.MaxTrackColor, XBrushes.LightGray);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, Slider view, XRect bounds, double scaleFactor)
    {
        Properties.Add(SliderViewProperties.ThumbColor,
            view.ThumbColor.IsNotDefault() ? view.ThumbColor.ToXBrush() : new XSolidBrush(XColor.FromArgb(50, 84, 85)));
        Properties.Add(SliderViewProperties.ThumbWith, bounds.Height * 0.8);
        Properties.Add(SliderViewProperties.TrackHeight, bounds.Height * 0.2);
        Properties.Add(SliderViewProperties.MinTrackColor,
            view.MinimumTrackColor.IsNotDefault()
                ? view.MinimumTrackColor.ToXBrush()
                : new XSolidBrush(XColor.FromArgb(50, 84, 85)));
        Properties.Add(SliderViewProperties.MaxTrackColor,
            view.MaximumTrackColor.IsNotDefault() ? view.MaximumTrackColor.ToXBrush() : XBrushes.LightGray);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, Slider view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(SliderViewProperties.ThumbColor,
            view.ThumbColor.IsNotDefault() ? view.ThumbColor.ToXBrush() : new XSolidBrush(XColor.FromArgb(50, 84, 85)));
        Properties.Add(SliderViewProperties.ThumbWith, bounds.Height * 0.9);
        Properties.Add(SliderViewProperties.TrackHeight, bounds.Height * 0.12);
        Properties.Add(SliderViewProperties.MinTrackColor,
            view.MinimumTrackColor.IsNotDefault()
                ? view.MinimumTrackColor.ToXBrush()
                : new XSolidBrush(XColor.FromArgb(50, 84, 85)));
        Properties.Add(SliderViewProperties.MaxTrackColor,
            view.MaximumTrackColor.IsNotDefault() ? view.MaximumTrackColor.ToXBrush() : XBrushes.LightGray);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, Slider view, XRect bounds, double scaleFactor)
    {
        Properties.Add(SliderViewProperties.ThumbColor,
            view.ThumbColor.IsNotDefault() ? view.ThumbColor.ToXBrush() : XBrushes.White);
        Properties.Add(SliderViewProperties.ThumbWith, bounds.Height * 0.4);
        Properties.Add(SliderViewProperties.MinTrackColor, view.MinimumTrackColor.IsNotDefault()
            ? view.MinimumTrackColor.ToXBrush()
            : XBrushes.White);
        Properties.Add(SliderViewProperties.MaxTrackColor,
            view.MaximumTrackColor.IsNotDefault() ? view.MaximumTrackColor.ToXBrush() : XBrushes.DarkGray);

        Properties.Add(SliderViewProperties.ThumbRingColor, XBrushes.DarkGray);
        Properties.Add(SliderViewProperties.ThumbRingWidth, bounds.Height * 0.8);
    }

    private static class SliderViewProperties
    {
        public const string ThumbColor = nameof(ThumbColor);
        public const string ThumbWith = nameof(ThumbWith);
        public const string ThumbRingColor = nameof(ThumbRingColor);
        public const string ThumbRingWidth = nameof(ThumbRingWidth);
        public const string TrackHeight = nameof(TrackHeight);
        public const string MinTrackColor = nameof(MinTrackColor);
        public const string MaxTrackColor = nameof(MaxTrackColor);
    }
}
