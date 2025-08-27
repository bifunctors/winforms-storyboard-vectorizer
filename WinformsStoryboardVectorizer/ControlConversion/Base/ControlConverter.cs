using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Base;
using WinformsStoryboardVectorizer.ControlConversion.Converters;

namespace WinformsStoryboardVectorizer.ControlConversion.Interfaces;
public abstract class ControlConverter<T> : IControlConverter where T : Control {
    protected static readonly XNamespace _svgNamespace = "http://ww.w3.org/2000/svg";

    public Type ConversionType { get; } = typeof(T);

    XElement IControlConverter.Convert(Control control, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        return Convert((T)control, converterFactory, controlIdGenerator);
    }

    protected abstract XElement Convert(T control, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator);
}
