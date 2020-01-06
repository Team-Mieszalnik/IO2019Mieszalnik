//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wypozyczalnia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class IndywidualnaOferta
    {
        public int Id { get; set; }
        [Required, StringLength(30)]
        public string Marka { get; set; }
        [Required, StringLength(30)]
        public string Model { get; set; }
        [Required]
        public System.DateTime Rok { get; set; }
        [Range(0,10000)]
        public Nullable<int> LimitKilometrow { get; set; }
        [StringLength(30)]
        public string Opony { get; set; }
        public Nullable<bool> AC { get; set; }
        [StringLength(100)]
        public string Opis { get; set; }
        public string UserId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
