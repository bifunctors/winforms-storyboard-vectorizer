using System.Xml.Linq;

namespace WinformsStoryboardVectorizer;

public class SvgInformation {
    public static readonly XNamespace SvgNamespace = "http://ww.w3.org/2000/svg";

    public XDocument Document { get; init; }
    public XElement Root { get; init; }
    public XElement Defs { get; init; }

    public SvgInformation() {
        Root = new XElement(SvgNamespace + "svg");
        Defs = new XElement(SvgNamespace + "defs");
        Root.Add(Defs);

        Document = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                Root
                );
    }

    public void AddToDefs(XElement element) => Defs.Add(element);
}
