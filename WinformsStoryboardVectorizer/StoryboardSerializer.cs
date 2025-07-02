namespace WinformsStoryboardVectorizer;

public class StoryboardSerializer {
    private readonly Dictionary<Type, Func<Control, string>> _converters = [];
    public void Register<T>(Func<T, string> converter) where T : Control =>
        _converters.Add(typeof(T), control => converter((T)control));

    public string Serialize(Control control) {
        Func<Control, string>? converter = null;

        // Find the type to serialize with base type fall back
        Type? type = control.GetType();
        while (type is not null) {
            if (_converters.TryGetValue(type, out converter)) break;
            type = type.BaseType;
        }

        if (type is null) throw new StoryboardSerializationException(control.GetType());

        if (control.Controls.Count == 0) return converter!(control);
        
        string childControlStrings = "";
        foreach (Control childControl in control.Controls) {
            childControlStrings += Serialize(childControl);
        }

        return $"{converter!(control)}<g transform=\"translate({control.Location.X},{control.Location.Y})\">{childControlStrings}</g>";
    }
}
