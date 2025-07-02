namespace WinformsStoryboardVectorizer; 

[Serializable]
public class StoryboardSerializationException : Exception {
    public Type MissingType { get; init; }

    public StoryboardSerializationException(Type missingType) : base($"Missing converter for {missingType} or its children") {
        MissingType = missingType;
    }
}