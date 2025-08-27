using WinformsStoryboardVectorizer.ControlConversion.Base;

namespace WinformsStoryboardVectorizer.ControlConversion.Interfaces; 
public abstract class ControlConverterFactory {
    private Dictionary<Type, IControlConverter> _converters = new();

    public IControlConverter GetControlConverter(Control control) {
        if (_converters.ContainsKey(control.GetType())) {
            return _converters[control.GetType()];
        }
        else throw new ControlConversionException(control.GetType());
    }

    public void Register(IControlConverter converter) {
        _converters.Add(converter.ConversionType, converter);
    }
}
