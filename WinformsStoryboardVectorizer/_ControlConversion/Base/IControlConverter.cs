using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.Factories;

namespace WinformsStoryboardVectorizer.ControlConversion.Base; 
public interface IControlConverter {
    public Type ConversionType { get; }

    public XElement Convert(Control control, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator);
}
