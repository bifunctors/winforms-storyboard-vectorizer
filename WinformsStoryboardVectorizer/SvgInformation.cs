using System.Xml.Linq;

namespace WinformsStoryboardVectorizer;

public class SvgInformation {
    public static readonly XNamespace SvgNamespace =  "http://www.w3.org/2000/svg";

    public XDocument Document { get; init; }

    private readonly XElement _svgRoot;
    private readonly XElement _defs;

    public SvgInformation(double width, double height) {
        Document = new();
        _svgRoot = new(SvgNamespace + "svg");
        _defs = new(SvgNamespace + "def");
        _svgRoot.Add(_defs);
    }

    public void AddDef(XElement element) => _defs.Add(element);

    public void Add(XElement element) => _svgRoot.Add(element);
}
