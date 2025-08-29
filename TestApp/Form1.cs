namespace TestApp {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            button1.Text = Location.ToString();
            base.OnMouseMove(e);
        }
    }
}
