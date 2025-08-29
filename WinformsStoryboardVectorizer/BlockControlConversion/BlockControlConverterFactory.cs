using WinformsStoryboardVectorizer.BlockControlConversion.Converters;
using WinformsStoryboardVectorizer.Factories;

namespace WinformsStoryboardVectorizer.BlockControlConversion;
public class BlockControlConverterFactory : ControlConverterFactory {
    public BlockControlConverterFactory() {
        Register(new BlockFormConverter());
        Register(new BlockControlConverter());
    }
}
