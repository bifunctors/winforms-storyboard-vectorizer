using System.Xml.Linq;

namespace WinformsStoryboardVectorizer;

public class DefaultControlsConverters {
    public DefaultControlsConverters(StoryboardSerializer serializer) {
        serializer.Register<Form>(ConvertForm);
        serializer.Register<TextBox>(ConvertTextBox);
        serializer.Register<Panel>(ConvertPanel);
        serializer.Register<Button>(ConvertButton);
    }

    protected static XElement ConvertForm(Form form) {
        return new XElement(SvgInformation.SvgNamespace + "rect",
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", form.Width),
            new XAttribute("height", form.Height),
            new XAttribute("fill", $"rgb({form.BackColor.R},{form.BackColor.G},{form.BackColor.B})"));
    }

    protected static XElement ConvertButton(Button button) {
        XElement buttonSvg = new(SvgInformation.SvgNamespace + "rect",
            new XAttribute("x", button.Location.X),
            new XAttribute("y", button.Location.Y),
            new XAttribute("width", button.Width),
            new XAttribute("height", button.Height),
            new XAttribute("fill", $"rgb({button.BackColor.R},{button.BackColor.G},{button.BackColor.B})"));

        SizeF textSize;
        using (var g = Graphics.FromHwnd(IntPtr.Zero)) {
           textSize = g.MeasureString(button.Text, button.Font);
        }

        double textX;
        string textAnchor;

        // Horizontal alignment
        switch (button.TextAlign) {
            case ContentAlignment.TopLeft:
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.BottomLeft:
                textX = 5; // Small padding from the left edge
                textAnchor = "start";
                break;
            case ContentAlignment.TopCenter:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.BottomCenter:
                textX = button.Width / 2.0;
                textAnchor = "middle";
                break;
            case ContentAlignment.TopRight:
            case ContentAlignment.MiddleRight:
            case ContentAlignment.BottomRight:
                textX = button.Width - 5; // Small padding from the right edge
                textAnchor = "end";
                break;
            default: // Default to center if not specified
                textX = button.Width / 2.0;
                textAnchor = "middle";
                break;
        }

        double textY;

        // Vertical alignment
        switch (button.TextAlign) {
            case ContentAlignment.TopLeft:
            case ContentAlignment.TopCenter:
            case ContentAlignment.TopRight:
                // SVG's y is the baseline, so we add font ascent to get to the top of the text
                textY = textSize.Height / 2.0;
                break;
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.MiddleRight:
                // Middle is centered on the button height, then adjusted for baseline
                textY = (button.Height - textSize.Height) / 2.0 + textSize.Height - (textSize.Height * 0.2); // Heuristic baseline adjustment
                break;
            case ContentAlignment.BottomLeft:
            case ContentAlignment.BottomCenter:
            case ContentAlignment.BottomRight:
                // Bottom is near the bottom of the button
                textY = button.Height - 5; // Small padding from the bottom edge
                break;
            default: // Default to middle
                textY = (button.Height - textSize.Height) / 2.0 + textSize.Height - (textSize.Height * 0.2); // Heuristic baseline adjustment
                break;
        }

        XElement buttonTextSvg = new(SvgInformation.SvgNamespace + "text",
            new XAttribute("x", textX + button.Location.X),
            new XAttribute("y", textY + button.Location.Y),
            new XAttribute("text-anchor", textAnchor),
            new XAttribute("font-size", $"{button.Font.SizeInPoints}pt"),
            new XAttribute("fill", $"rgb({button.ForeColor.R},{button.ForeColor.G},{button.ForeColor.B})"),
            button.Text);

        return new XElement(SvgInformation.SvgNamespace + "g", buttonSvg, buttonTextSvg);

    }

    protected static XElement ConvertCheckBox(CheckBox checkBox) {
        throw new NotImplementedException();
    }

    protected static XElement ConvertCheckedListBox(CheckedListBox checkedListBox) {
        throw new NotImplementedException();
    }

    protected static XElement ConvertComboBox(ComboBox comboBox) {
        throw new NotImplementedException();
    }

    protected static XElement ConvertContextMenuStrip(ContextMenuStrip contextMenuStrip) {
        throw new NotImplementedException("This will not be implemented");
    }

    protected static XElement ConvertDataGridView(DataGridView dataGridView) {
        throw new NotImplementedException();
    }

    protected static XElement ConvertDateTimePicker(DateTimePicker dateTimePicker) {
        throw new NotImplementedException();
    }

    protected static XElement ConvertDomainUpDown(DomainUpDown domainUpDown) {
        throw new NotImplementedException();
    }

    protected static XElement ConvertFlowLayoutPanel(FlowLayoutPanel flowLayoutPanel) {
        throw new NotImplementedException();
    }

    protected static XElement ConvertGroupBox(GroupBox groupBox) {
        throw new NotImplementedException();
    }


    protected static string ConvertTextBox(TextBox textbox) => "";
    protected static string ConvertPanel(Panel panel) =>
        $"<rect x=\"{panel.Location.X}\" y=\"{panel.Location.Y}\" width=\"{panel.Width}\" height=\"{panel.Height}\" fill=\"rgb({panel.BackColor.R},{panel.BackColor.G},{panel.BackColor.B})\"/>";
}