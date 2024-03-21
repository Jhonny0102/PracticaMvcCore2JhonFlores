using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2JhonFlores.Data;
using PracticaMvcCore2JhonFlores.Models;

namespace PracticaMvcCore2JhonFlores.Repositories
{
    public class RepositoryLibros
    {
        private LibrosContext context;
        public RepositoryLibros(LibrosContext context)
        {
            this.context = context;
        }

        public async Task<List<Libro>> GetAllLibrosAsync()
        {
            return await this.context.Libros.ToListAsync();
        }

        public async Task<List<Libro>> GetLibrosGeneroAsync(int idgenero)
        {
            return await this.context.Libros.Where(z => z.IdGenero == idgenero).ToListAsync();
        }

        public async Task<Libro> FindLibroAsync(int idlibro)
        {
            return await this.context.Libros.Where(z => z.IdLibro == idlibro).FirstOrDefaultAsync();
        }

        public async Task<List<Genero>> GetAllGenerosAsync()
        {
            return await this.context.Generos.ToListAsync();
        }

        public async Task<Usuario> FindusuarioAsync(string email, string password)
        {
            return await this.context.Usuarios.Where(z => z.Email == email && z.Pass == password).FirstOrDefaultAsync();
        }

        public async Task<List<Libro>> GetIdsLibrosAsync(List<int> ids)
        {
            var consulta = from datos in this.context.Libros
                           where ids.Contains(datos.IdLibro)
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return await consulta.ToListAsync();
            }
        }
    }
}
