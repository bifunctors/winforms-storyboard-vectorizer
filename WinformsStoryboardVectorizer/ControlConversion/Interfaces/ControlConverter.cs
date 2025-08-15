using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Converters;

namespace WinformsStoryboardVectorizer.ControlConversion.Interfaces;
public abstract class ControlConverter(IControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
    protected readonly IControlConverterFactory _converterFactory = converterFactory;
    protected readonly ControlIdGenerator _controlIdGenerator = controlIdGenerator;

    public abstract bool CanConvert(Control control);
    public abstract XElement Convert(Control control);
}
