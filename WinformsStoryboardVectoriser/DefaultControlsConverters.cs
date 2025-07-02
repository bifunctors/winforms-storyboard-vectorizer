namespace WinformsStoryboardVectorizer;

public class DefaultControlsConverters {
    public DefaultControlsConverters(StoryboardSerializer serializer) {
        serializer.Register<Form>(ConvertForm);
        serializer.Register<TextBox>(ConvertTextBox);
        serializer.Register<Panel>(ConvertPanel);
        serializer.Register<Button>(ConvertButton);
    }

    protected static string ConvertForm(Form form) =>
        $"<rect x=\"0\" y=\"0\" width=\"{form.Width}\" height=\"{form.Height}\"/>";
    protected static string ConvertTextBox(TextBox textbox) => "";
    protected static string ConvertPanel(Panel panel) => 
        $"<rect x=\"{panel.Left}\" y=\"{panel.Top}\" width=\"{panel.Width}\" height=\"{panel.Height}\"/>";
    protected static string ConvertButton(Button button)=> "";
}
