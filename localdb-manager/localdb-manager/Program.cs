using System;
using System.Collections.Generic;
using System.Data.SqlLocalDb;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace localdb_manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (SqlLocalDbApi.IsLocalDBInstalled())
                Application.Run(new Main());
            else
                Application.Run(new BadForm());
        }
    }
}
