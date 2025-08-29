using static System.Windows.Forms.Control;

namespace WinformsStoryboardVectorizer.ControlConversion.Helpers; 
internal static class ControlHelpers {
    public static IEnumerable<Control> Reverse(ControlCollection controlCollection) {
        for (int i = controlCollection.Count - 1; i >= 0; i--) {
            yield return controlCollection[i];
        }
    }

    public static string ToRgbaString(this Color color) {
        return $"rgba({color.R}, {color.G}, {color.B}, {color.A})";
    }

    public static string ToRgbString(this Color color) {
        return $"rgb({color.R}, {color.G}, {color.B})";
    }
}
