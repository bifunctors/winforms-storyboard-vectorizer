using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;

namespace WinformsStoryboardVectorizer.ControlConversion.Converters;
public class PanelConverter : ControlConverter {
    public bool CanConvert(Control control) {
        return control is Panel;
    }

    public XElement Convert(Control control) {
        throw new NotImplementedException();
    }
}
