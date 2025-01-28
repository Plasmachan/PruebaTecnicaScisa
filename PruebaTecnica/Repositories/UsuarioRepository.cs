using PruebaTecnica.Helpers;
using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnica.Repositories
{
    public class UsuarioRepository
    {
        public readonly Model1 context;

        public UsuarioRepository(Model1 context)
        {
            this.context  = context;
        }


        public void InsertarUsuario(Usuario u)
        {
            context.Usuarios.Add(u);
            context.SaveChanges();
        }

        public bool ExisteUsuario(string name)
        {
            var usuario = context.Usuarios.FirstOrDefault(x=>x.NombreUsuario == name) ;

            if (usuario != null)
            {
                return true;
            }

            return false;
        }


        public Usuario GetUsuarioByName(string Name)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.NombreUsuario == Name);

            if(usuario != null)
            {
                return usuario;
            }
            return null;
        }


        public Usuario ValidarUsuario(string username, string password)
        {
            var usuario = context.Usuarios.SingleOrDefault(u => u.NombreUsuario == username);
            if (usuario != null)
            {
                // Comparar contraseñas encriptadas

                if (usuario.Contraseña == Encriptacion.GetMD5(password)) 
                {
                    return usuario;
                }
            }
            return null;
        }
    }
}