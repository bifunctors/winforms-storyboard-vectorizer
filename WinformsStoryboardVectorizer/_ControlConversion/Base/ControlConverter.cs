using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Base;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.Factories;
using WinformsStoryboardVectorizer.ControlConversion.Helpers;

namespace WinformsStoryboardVectorizer.ControlConversion.Interfaces;
public abstract class ControlConverter<T> : IControlConverter where T : Control {
    public Type ConversionType { get; } = typeof(T);

    XElement IControlConverter.Convert(Control control, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        return Convert((T)control, converterFactory, controlIdGenerator);
    }

    protected abstract XElement Convert(T control, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator);

    protected static IEnumerable<XElement> GetConvertedChildren(Control control, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        foreach (Control childControl in ControlHelpers.Reverse(control.Controls)) {
            yield return converterFactory.GetControlConverter(childControl).Convert(childControl, converterFactory, controlIdGenerator);
        }
    }
}
