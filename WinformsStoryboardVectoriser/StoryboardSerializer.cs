using System.Windows.Forms;

namespace WinformsStoryboardVectorizer;

public class StoryboardSerializer {
    private Dictionary<Type, Func<Control, string>> converters = [];

    public void Register<T>(Func<T, string> converter) where T : Control {
        converters.Add(typeof(T), converter);
    }

    public string Serialize(Form form) {

    }
}
