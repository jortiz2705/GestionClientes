using System;
using System.Net.Mail;

namespace GestionClientes
{
    internal class Cliente
    {
        public string Id {  get;private set; }
        public string Nombre { get; private set; }
        public string Email { get; private set; }
        public decimal Credito { get;private set; }
        public bool Estado { get; private set; }

        public Cliente(string id,string nombre,string correo,decimal credito,bool estado)
        {           
            if (credito<0)
            {
                throw new ArgumentException("El monto ingresado no puede ser negativo");
            }
                
            Id = id;
            Nombre = nombre;
            Email = correo;
            Credito = credito;
            Estado = estado;
        }
        public bool ActualizarNombre(string nombre)
        {
            if (!Estado)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Length<3 || nombre.Length > 100)
            {
                return false;
            }
            Nombre = nombre;
            return true;
        }
        public bool ActualizarCorreo(string correo)
        {
            if (!Estado)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(correo))
            {
                return false;
            }
            try
            {
                var correoValido = new MailAddress(correo);
                Email = correoValido.Address;
                return true;
            }
            catch (FormatException) 
            {
                return false;
            }
        }
        public bool ActualizarCredito(decimal monto)
        {
            if (!Estado)
            {
                return false;
            }
            if (monto <= 0)
            {
                return false;
            }
            Credito += monto;
            return true;
        }
        public bool DisminuirCredito(decimal monto)
        {
            if (monto <= 0)
            {
                return false;
            }
            if (monto>Credito)
            {
                return false;
            }
            if (!Estado)
            {
                return false;
            }
            Credito -= monto;
            return true;
        }
        public bool Desactivar()
        {
            if (Estado)
            {
                Estado = false;
                return true;
            }
            return false;
        }
        public bool Activar()
        {
            if (Estado)
            {
                return false;
            }
            Estado = true;
            return true;
        }
    }
}

