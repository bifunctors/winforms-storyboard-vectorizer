namespace WinformsStoryboardVectorizer.ControlConversion; 

[Serializable]
public class ControlConversionException : Exception {
    public Type MissingType { get; init; }

    public ControlConversionException(Type missingType) : base($"Missing converter for {missingType}") {
        MissingType = missingType;
    }
}