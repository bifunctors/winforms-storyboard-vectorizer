using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;
using WinformsStoryboardVectorizer.Factories;
using static WinformsStoryboardVectorizer.ControlConversion.Helpers.SvgConstants;

public class ButtonConverter : ControlConverter<Button> {
    protected override XElement Convert(Button button, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        XElement buttonRect = new(SvgNamespace + "rect",
            new XAttribute("id", controlIdGenerator.GetNextId(button.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", button.Width),
            new XAttribute("height", button.Height),
            new XAttribute("fill", $"rgb({button.BackColor.R},{button.BackColor.G},{button.BackColor.B})"),
            new XAttribute("stroke", GetBorderColor(button)),
            new XAttribute("stroke-width", GetBorderWidth(button)),
            new XAttribute("rx", 3), // Rounded corners typical for buttons
            new XAttribute("ry", 3));

        // Button text
        XElement buttonText = null;
        if (!string.IsNullOrEmpty(button.Text)) {
            var textPosition = CalculateTextPosition(button);
            var textAnchor = GetTextAnchor(button.TextAlign);
            var dominantBaseline = GetDominantBaseline(button.TextAlign);

            buttonText = new XElement(SvgNamespace + "text",
                new XAttribute("id", controlIdGenerator.GetNextId(button.Name + "-text")),
                new XAttribute("x", textPosition.X),
                new XAttribute("y", textPosition.Y),
                new XAttribute("text-anchor", textAnchor),
                new XAttribute("dominant-baseline", dominantBaseline),
                new XAttribute("font-family", button.Font.FontFamily.Name),
                new XAttribute("font-size", TextRenderer.MeasureText(button.Text, button.Font).Height),
                new XAttribute("font-weight", button.Font.Bold ? "bold" : "normal"),
                new XAttribute("font-style", button.Font.Italic ? "italic" : "normal"),
                new XAttribute("fill", $"rgb({button.ForeColor.R},{button.ForeColor.G},{button.ForeColor.B})"),
                button.Text);

            // Add text decoration for underline
            if (button.Font.Underline) {
                buttonText.Add(new XAttribute("text-decoration", "underline"));
            }
        }

        // Create the main group with transform
        XElement buttonGroup = new(SvgNamespace + "g",
            new XAttribute("transform", $"translate({button.Location.X}, {button.Location.Y})"),
            buttonRect);

        // Add text if it exists
        if (buttonText != null) {
            buttonGroup.Add(buttonText);
        }

        // Add cursor pointer style for interactivity indication
        buttonGroup.Add(new XAttribute("style", "cursor: pointer;"));

        return buttonGroup;
    }

    private (float X, float Y) CalculateTextPosition(Button button) {
        const float padding = 4; // Standard button text padding
        float x, y;

        // Horizontal positioning
        switch (button.TextAlign) {
            case ContentAlignment.TopLeft:
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.BottomLeft:
                x = padding;
                break;
            case ContentAlignment.TopCenter:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.BottomCenter:
                x = button.Width / 2.0f;
                break;
            case ContentAlignment.TopRight:
            case ContentAlignment.MiddleRight:
            case ContentAlignment.BottomRight:
                x = button.Width - padding;
                break;
            default:
                x = button.Width / 2.0f; // Default to center
                break;
        }

        // Vertical positioning
        switch (button.TextAlign) {
            case ContentAlignment.TopLeft:
            case ContentAlignment.TopCenter:
            case ContentAlignment.TopRight:
                y = padding + button.Font.Size * 0.8f; // Account for font baseline
                break;
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.MiddleRight:
                y = button.Height / 2.0f;
                break;
            case ContentAlignment.BottomLeft:
            case ContentAlignment.BottomCenter:
            case ContentAlignment.BottomRight:
                y = button.Height - padding;
                break;
            default:
                y = button.Height / 2.0f; // Default to middle
                break;
        }

        return (x, y);
    }

    private string GetTextAnchor(ContentAlignment textAlign) {
        switch (textAlign) {
            case ContentAlignment.TopLeft:
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.BottomLeft:
                return "start";
            case ContentAlignment.TopCenter:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.BottomCenter:
                return "middle";
            case ContentAlignment.TopRight:
            case ContentAlignment.MiddleRight:
            case ContentAlignment.BottomRight:
                return "end";
            default:
                return "middle";
        }
    }

    private string GetDominantBaseline(ContentAlignment textAlign) {
        switch (textAlign) {
            case ContentAlignment.TopLeft:
            case ContentAlignment.TopCenter:
            case ContentAlignment.TopRight:
                return "text-before-edge"; // Top alignment
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.MiddleRight:
                return "middle"; // Middle alignment
            case ContentAlignment.BottomLeft:
            case ContentAlignment.BottomCenter:
            case ContentAlignment.BottomRight:
                return "text-after-edge"; // Bottom alignment
            default:
                return "middle";
        }
    }

    private string GetBorderColor(Button button) {
        // WinForms buttons typically have a border
        // You might want to customize this based on button.FlatStyle
        switch (button.FlatStyle) {
            case FlatStyle.Flat:
                return $"rgb({button.FlatAppearance.BorderColor.R},{button.FlatAppearance.BorderColor.G},{button.FlatAppearance.BorderColor.B})";
            case FlatStyle.Popup:
            case FlatStyle.Standard:
            case FlatStyle.System:
            default:
                return "rgb(128,128,128)"; // Default gray border
        }
    }

    private int GetBorderWidth(Button button) {
        switch (button.FlatStyle) {
            case FlatStyle.Flat:
                return button.FlatAppearance.BorderSize;
            case FlatStyle.Popup:
                return 1;
            case FlatStyle.Standard:
            case FlatStyle.System:
            default:
                return 1;
        }
    }
}
