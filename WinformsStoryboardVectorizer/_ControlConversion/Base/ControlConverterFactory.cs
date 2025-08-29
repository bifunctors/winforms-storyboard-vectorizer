using WinformsStoryboardVectorizer.ControlConversion;
using WinformsStoryboardVectorizer.ControlConversion.Base;

namespace WinformsStoryboardVectorizer.Factories; 
public abstract class ControlConverterFactory {
    private Dictionary<Type, IControlConverter> _converters = [];

    public IControlConverter GetControlConverter(Control control) {
        Type type = control.GetType();
        while (true) {
            if (_converters.ContainsKey(type)) {
                return _converters[type];
            }
            else if (type.BaseType is null) {
                throw new ControlConversionException(control.GetType());
            }

            type = type.BaseType;
        }
    }

    public void Register(IControlConverter converter) {
        _converters.Add(converter.ConversionType, converter);
    }
}
