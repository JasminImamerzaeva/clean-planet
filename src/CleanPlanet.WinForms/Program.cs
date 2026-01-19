using System;
using System.Windows.Forms;

namespace CleanPlanet.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show("Необработанное исключение:\r\n" + e.Exception.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            ApplicationConfiguration.Initialize();

            using (var login = new FormLogin())
            {
                var result = login.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
            }

            Application.Run(new FormPartners());
        }
    }
}
