using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocioMatrix {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            try {
                SocioMatrix socioMatrix = new SocioMatrix();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new GUIMain(socioMatrix));
            } catch (Exception ex) {
                Program.HandleException(ex);
            }
        }

        public static void HandleException(Exception ex) {
            MessageBox.Show("Exception: " + ex.ToString());
        }
    }
}
