using System.Xml.Linq;

namespace WinformsStoryboardSeriaizer;

internal static class SvgManager {
    public static readonly XNamespace SvgNamespace = "http://ww.w3.org/2000/svg";

    public XDocument Document { get; init; }
    public XElement Root { get; init; }
    public XElement Defs { get; init; }

    public SvgManager() {
        Root = new XElement(SvgNamespace + "svg");
        Defs = new XElement("defs");
        Root.Add(Defs);

        Document = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                Root
                );
    }

    public void AddToDefs(XElement element) => Defs.Add(element);
    public void AddToRoot(XElement element) => Root.Add(element);
}
