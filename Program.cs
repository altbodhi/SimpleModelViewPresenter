using System;
using System.Windows.Forms;
namespace SimpleModelViewPresenter
{
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new ViewNoteForm());
        }
    }
}
