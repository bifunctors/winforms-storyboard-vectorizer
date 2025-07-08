def get_controls():
    file = open("controls.txt")
    text = file.read()
    lines = text.split("\n")
    controls = [control for control in lines if len(control) > 0 and control[0] != "#"]
    return controls


def main():
    controls = get_controls()

    for control in controls:
        print(gen_method(control))


def gen_method(control):
    control_var = control[0].lower() + control[1:]

    print(control_var)
    method =  """
    protected static XElement Convert{0}({0} {1}) {{
        throw new NotImplementedException();
    }}
    """.format(control, control_var)

    return method


if __name__ == "__main__":
    main()
