namespace WinformsStoryboardVectorizer.ControlConversion.Converters; 
public class ControlIdGenerator {
    private int _counter = 0;
    public string GetNextId(string controlName) => $"{controlName}-{_counter++}";
}