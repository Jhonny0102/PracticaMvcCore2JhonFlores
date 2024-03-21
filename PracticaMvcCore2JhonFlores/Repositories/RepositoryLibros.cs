using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2JhonFlores.Data;
using PracticaMvcCore2JhonFlores.Models;

#region Procedures

//CREATE PROCEDURE SP_FINALIZARCOMPRA
//(@idfactura int, @fecha date, @idlibro int, @idusuario int)
//AS
//	--IDPEDIDO tiene que ser autoincremental
//	--IDFACTURA se incrementa pero se mantiene ese numero en los pedido que haya hecho(FUERA CON LAMBDA)
//	--FECHA la de hoy
//	--IDLIBRO hay que pasar los idlibros de la session(FUERA CON UN FOR)
//	--IDUSUARIO quien lo haya pedido
//	--CANTIDAD SIEMPRE 1
//	DECLARE @maxidpedido int
//	SELECT @maxidpedido = COUNT(IDPEDIDO) + 1 FROM PEDIDOS
//	INSERT INTO PEDIDOS (IDPEDIDO, IDFACTURA, FECHA, IDLIBRO, IDUSUARIO, CANTIDAD) VALUES (@maxidpedido, @idfactura, @fecha, @idlibro, @idusuario,1)
//GO

#endregion

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

        public async Task<List<ViewPedido>> GetPedidosUsuario(int idusuario)
        {
            return await this.context.ViewPedidos.Where(z => z.IdUsuario == idusuario).ToListAsync();
        }

        public void FinalizarCompra(List<int> idsalmacenados, int idusuario)
        {
            var consulta = from datos in this.context.Pedidos
                           select datos;
            DateTime fecha = DateTime.Today;
            int maxidfactura = consulta.Max(z => z.IdFactura)+1;

            for (int i = 0; i < idsalmacenados.Count(); i++)
            {
                string sql = "SP_FINALIZARCOMPRA @idfactura ,@fecha ,@idlibro , @idusuario";
                SqlParameter pamIdfactura = new SqlParameter("@idfactura", maxidfactura);
                SqlParameter pamFecha = new SqlParameter("@fecha", fecha);
                SqlParameter pamIdLibro = new SqlParameter("@idlibro", idsalmacenados[i]);
                SqlParameter pamIdUsuario = new SqlParameter("@idusuario", idusuario);
                this.context.Database.ExecuteSqlRaw(sql, pamIdfactura, pamFecha, pamIdLibro, pamIdUsuario);
            }
        }
    }
}
