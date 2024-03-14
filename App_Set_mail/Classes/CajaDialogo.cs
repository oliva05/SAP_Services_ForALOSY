using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace App_Set_mail
{
    public sealed class CajaDialogo
    {
        public static DialogResult Error(string Mensaje)
        {
            return MessageBox.Show(Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static DialogResult Error(string Mensaje, Exception ec1)
        {
            return MessageBox.Show(Mensaje + ec1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Information(string msj)
        {
            return MessageBox.Show(msj, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Pregunta(string msj)
        {
            return MessageBox.Show(msj, "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }

}
