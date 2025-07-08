namespace WinformsStoryboardVectorizer;

public class DefaultControlsConverters {
    public DefaultControlsConverters(StoryboardSerializer serializer) {
        serializer.Register<Form>(ConvertForm);
        serializer.Register<TextBox>(ConvertTextBox);
        serializer.Register<Panel>(ConvertPanel);
        serializer.Register<Button>(ConvertButton);
    }

    protected static string ConvertForm(Form form) =>
        $"<rect x=\"0\" y=\"0\" width=\"{form.Width}\" height=\"{form.Height}\" fill=\"rgb({form.BackColor.R},{form.BackColor.G},{form.BackColor.B})\"/>";
    protected static string ConvertTextBox(TextBox textbox) => "";
    protected static string ConvertPanel(Panel panel) => 
        $"<rect x=\"{panel.Location.X}\" y=\"{panel.Location.Y}\" width=\"{panel.Width}\" height=\"{panel.Height}\" fill=\"rgb({panel.BackColor.R},{panel.BackColor.G},{panel.BackColor.B})\"/>";    protected static string ConvertButton(Button button)=> "";
}
