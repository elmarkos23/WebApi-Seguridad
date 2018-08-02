using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Seguridad.Security
{
    public class clsAcceso
    {
        public static bool VaidateUser(string username, string password)
        {
            //autenticacion registrado en codigo, se puede remplazar por otros modos
            if (username.Equals("admin") && password.Equals("admin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }
}