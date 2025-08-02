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
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", button.Width),
            new XAttribute("height", button.Height),
            new XAttribute("fill", $"rgb({button.BackColor.R},{button.BackColor.G},{button.BackColor.B})"));

        throw new NotImplementedException("Text position not implemented");

        XElement buttonTextSvg = new(SvgInformation.SvgNamespace + "text",
            new XAttribute("font-size", $"{button.Font.SizeInPoints}"));

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