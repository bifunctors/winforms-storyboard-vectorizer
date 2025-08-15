namespace WinformsStoryboardVectorizer.ControlConversion.Interfaces; 
public interface IControlConverterFactory {
    public ControlConverter GetControlConverter(Control control);
}
