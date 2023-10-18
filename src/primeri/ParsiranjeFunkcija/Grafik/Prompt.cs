using System.Windows.Forms;

namespace Grafik
{
    // pomoćna klasa koja prikazuje dijalog za unos teksta
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption, string initialValue)
        {
            // kreiraj malu formu kao dijaloški prozor, dodaj kontrole na formu
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() 
            { 
                Text = "Ok", Left = 350, Width = 100, Top = 80, 
                DialogResult = DialogResult.OK 
            };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            textBox.Text = initialValue;

            // prikaži djalog i ako je unos potvrđen - vrati uneti tekst
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
