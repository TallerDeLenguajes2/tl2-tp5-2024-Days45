using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using EspacioTp5;

namespace rapositoriosTP5
{
    public class PresupuestosRepository : IPresupuestoRepository
    {
        private string cadenaConexion = "Data Source=DB/presupuestos.db;Cache=Shared";

        // Crear un nuevo Presupuesto (recibe un objeto Presupuesto)
        public void CrearPresupuesto(Presupuestos presupuesto)
        {
            var query = "INSERT INTO presupuestos (idPresupuesto, nombreDestinatario) VALUES (@idPresupuesto, @nombreDestinatario)";
            using (var connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idPresupuesto", presupuesto.IdPresupuesto);
                    command.Parameters.AddWithValue("@nombreDestinatario", presupuesto.NombreDestinatario);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        // Listar todos los Presupuestos registrados (devuelve un List de Presupuesto)
        public List<Presupuestos> ListarPresupuestos()
        {
            var presupuestos = new List<Presupuestos>();
            var query = "SELECT * FROM presupuestos";
            using (var connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var presupuesto = new Presupuestos(
                                Convert.ToInt32(reader["idPresupuesto"]),
                                reader["nombreDestinatario"].ToString()
                            );
                            presupuestos.Add(presupuesto);
                        }
                    }
                }
                connection.Close();
            }
            return presupuestos;
        }

        // Obtener detalles de un Presupuesto por su ID (recibe un Id y devuelve un Presupuesto)
        public Presupuestos ObtenerPresupuesto(int id)
        {
            Presupuestos presupuesto = null;
            var query = "SELECT * FROM presupuestos WHERE idPresupuesto = @idPresupuesto";
            using (var connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idPresupuesto", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            presupuesto = new Presupuestos(
                                Convert.ToInt32(reader["idPresupuesto"]),
                                reader["nombreDestinatario"].ToString()
                            );
                        }
                    }
                }
                connection.Close();
            }
            return presupuesto;
        }

        // Agregar un producto y una cantidad a un presupuesto
        public void AgregarProductoAPresupuesto(int presupuestoId, Productos producto, int cantidad)
        {
            var query = "INSERT INTO presupuesto_productos (idPresupuesto, idProducto, cantidad) VALUES (@idPresupuesto, @idProducto, @cantidad)";
            using (var connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idPresupuesto", presupuestoId);
                    command.Parameters.AddWithValue("@idProducto", producto.IdProducto);
                    command.Parameters.AddWithValue("@cantidad", cantidad);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        // Eliminar un Presupuesto por ID (recibe un Id)
        public void EliminarPresupuesto(int id)
        {
            var query = "DELETE FROM presupuestos WHERE idPresupuesto = @idPresupuesto";
            using (var connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idPresupuesto", id);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
