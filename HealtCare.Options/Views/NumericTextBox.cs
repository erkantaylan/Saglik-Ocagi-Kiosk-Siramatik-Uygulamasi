using System.Linq;
using System.Windows.Controls;

namespace HealtCare.Options.Views {

    public class NumericTextBox : TextBox {
        protected override void OnTextChanged(TextChangedEventArgs e) {
            base.OnTextChanged(e);
            Text = new string(Text.Where(char.IsDigit).ToArray());
            SelectionStart = Text.Length;
        }
    }

}