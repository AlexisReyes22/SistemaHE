//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaHE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SolicitudHoras
    {
        public int ID_Transaccion { get; set; }
        public Nullable<int> CantidadDeHoras { get; set; }
        public Nullable<int> ID_Tarea { get; set; }
        public int Remitente { get; set; }
        public Nullable<int> JefeDestinatario { get; set; }
        public Nullable<int> Destinatario1 { get; set; }
        public Nullable<int> Destinatario2 { get; set; }
        public Nullable<int> Destinatario3 { get; set; }
        public string Estado { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
        public virtual Usuarios Usuarios1 { get; set; }
        public virtual Usuarios Usuarios2 { get; set; }
        public virtual Tareas Tareas { get; set; }
        public virtual Usuarios Usuarios3 { get; set; }
        public virtual Usuarios Usuarios4 { get; set; }
    }
}
