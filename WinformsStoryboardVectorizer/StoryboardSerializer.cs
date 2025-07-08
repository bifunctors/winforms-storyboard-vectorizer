using System.Xml.Linq;

namespace WinformsStoryboardVectorizer;

public class StoryboardSerializer {
    private readonly Dictionary<Type, Func<Control, SvgInformation, XElement>> _converters = [];
    public void Register<T>(Func<T, XElement> converter) where T : Control {
        _converters.Add(typeof(T), (control, svgInformation) => converter((T)control));
    }

    public void Register<T>(Func<T, string> converter) where T : Control {
        _converters.Add(typeof(T), (control, svgInformation) => XElement.Parse(converter((T)control)));
    }

    public void Register<T>(Func<T, SvgInformation, XElement> converter) where T : Control {
        _converters.Add(typeof(T), (control, svgInformation) => converter((T)control, svgInformation));
    }

    private SvgInformation? _svgInformation;
    private int _svgIdIndex;

    public SvgInformation Serialize(Control control) {
        _svgInformation = new();
        _svgIdIndex = 0;

        Serialize(control, _svgInformation.Root);

        return _svgInformation;
    }

    public void Serialize(Control control, XElement root) {
        Func<Control, SvgInformation, XElement> converter = GetConverter(control.GetType());

        root.Add(converter(control, _svgInformation));

        if (control.Controls.Count == 0) return;

        string clipPathId = $"{control.Name}-{_svgIdIndex++}";
        _svgInformation!.AddToDefs(
            new XElement(SvgInformation.SvgNamespace + "clipPath",
                new XAttribute("id", clipPathId),
                new XElement(SvgInformation.SvgNamespace + "rect",
                            new XAttribute("x", 0),
                            new XAttribute("y", 0),
                            new XAttribute("width", control.Width),
                            new XAttribute("height", control.Height))));

        XElement newRoot = new(SvgInformation.SvgNamespace + "g",
            new XAttribute("transform", $"translate({control.Location.X},{control.Location.Y})"),
            new XAttribute("clip-path", $"url(#{clipPathId})")
            );

        root.Add(newRoot);

        foreach (Control childControl in control.Controls) {
            Serialize(childControl, newRoot);
        }
    }

    private Func<Control, SvgInformation, XElement> GetConverter(Type controlType) {
        while (true) {
            if (_converters.TryGetValue(controlType, out Func<Control, SvgInformation, XElement>? converter)) return converter;
            if (controlType.BaseType is null) throw new StoryboardSerializationException(controlType);
            controlType = controlType.BaseType;
        }
    }
}
