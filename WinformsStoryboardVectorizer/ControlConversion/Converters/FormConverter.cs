using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;

namespace WinformsStoryboardVectorizer.ControlConversion.Converters;
public class FormConverter(IControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) : ControlConverter(converterFactory, controlIdGenerator) {
    public override bool CanConvert(Control control) => control is Form;

    public override XElement Convert(Control control) {
        Form form = (Form)control;

        XElement formSvg = new(SvgInformation.SvgNamespace + "rect",
            new XAttribute("id", _controlIdGenerator.GetNextId(form.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", form.Width),
            new XAttribute("height", form.Height),
            new XAttribute("fill", $"rgb({form.BackColor.R},{form.BackColor.G},{form.BackColor.B})"));

        XElement childSvgs = new(SvgInformation.SvgNamespace + "g",
            new XAttribute("x", 0),
            new XAttribute("y", 0));

        foreach (Control childControl in form.Controls) {
            childSvgs.Add(_converterFactory.GetControlConverter(childControl).Convert(childControl));
        }

        return new XElement(SvgInformation.SvgNamespace + "g", formSvg, formSvg);
    }
}
