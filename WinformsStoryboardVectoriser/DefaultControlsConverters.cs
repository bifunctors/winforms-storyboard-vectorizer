using System.Windows.Forms;

namespace WinformsStoryboardVectorizer;

public class DefaultControlsConverters {
    public DefaultControlsConverters(StoryboardSerializer serializer) {
        serializer.Register<Form>(ConvertForm);
        serializer.Register<Textbox>(ConvertTextbox);
        serializer.Register<Panel>(ConvertPanel);
        serializer.Register<Button>(ConvertButton);
    }

    protected string ConvertForm(Form form) => "";
    protected string ConvertTextbox(Textbox textbox);
    protected string ConvertPanel(Panel panel);
    protected string ConvertButton(Button button);
}
