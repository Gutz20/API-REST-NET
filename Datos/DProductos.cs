using System.Data.SqlClient;
using Tiendaapi.Conexion;
using Tiendaapi.Modelo;
using System.Data;

namespace Tiendaapi.Datos
{
    public class DProductos
    {
        Conexionbd cn = new Conexionbd();
        public async Task <List<MProductos>> MostrarProductos() {
            var lista = new List<MProductos>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("mostrarProductos", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mproductos = new MProductos();
                            mproductos.id = (int)item["id"];
                            mproductos.descripcion = (string)item["descripcion"];
                            mproductos.precio = (decimal)item["precio"];
                            lista.Add(mproductos);
                        }
                    }
                }
            }
            return lista;
        }
        public async Task InsertarProductos(MProductos parametros)
        {
            using(var sql = new SqlConnection (cn.cadenaSQL()))
            {
                using(var cmd = new SqlCommand("insertarProductos", sql))
                {
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descripcion", parametros.descripcion);
                    cmd.Parameters.AddWithValue("@precio", parametros.precio);
                    await sql.OpenAsync() ;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task EditarProductos(MProductos parametros)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("editarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", parametros.id);
                    cmd.Parameters.AddWithValue("@precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task EliminarProductos(MProductos parametros)
        {
            using(var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using(var cmd = new SqlCommand("eliminarProductos", sql))
                {
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", parametros.id);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
