﻿using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2JhonFlores.Models;

namespace PracticaMvcCore2JhonFlores.Data
{
    public class LibrosContext : DbContext
    {
        public LibrosContext(DbContextOptions<LibrosContext> options)
            :base(options)
        {

        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ViewPedido> ViewPedidos { get; set; }
    }
}
