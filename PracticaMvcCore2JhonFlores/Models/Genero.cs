﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2JhonFlores.Models
{
    [Table("GENEROS")]
    public class Genero
    {
        [Key]
        [Column("IdGenero")]
        public int IdGenero { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
    }
}
