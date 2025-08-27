using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;

namespace WinformsStoryboardVectorizer.Factories; 
public class DefautControlConverterFactory : ControlConverterFactory {
    public DefautControlConverterFactory() {
        Register(new FormConverter());
        Register(new PanelConverter());
    }
}
