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
    
    public partial class Rol
    {
        public int ID_Rol { get; set; }
        public int Identificacion { get; set; }
        public string Nombre_Rol { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}
