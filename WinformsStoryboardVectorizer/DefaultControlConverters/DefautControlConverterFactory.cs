using WinformsStoryboardVectorizer.DefaultControlConverters.Converters;

namespace WinformsStoryboardVectorizer.Factories; 
public class DefautControlConverterFactory : ControlConverterFactory {
    public DefautControlConverterFactory() {
        Register(new FormControlConverter());
        Register(new PanelConverter());
        Register(new ButtonConverter());
    }
}
