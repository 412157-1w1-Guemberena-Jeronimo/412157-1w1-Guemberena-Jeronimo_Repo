using Ejercicio_1._5__Comercio_.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1._5__Comercio_.Data
{
    internal class BudgetRepository : IBudgetRepository
    {
        public readonly SqlConnection _connection;
        public readonly SqlTransaction _transaction;
        public BudgetRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Budget> GetAll()
        {
            var budgets = new List<Budget>();

            using(var cmd = new SqlCommand("SP_RECUPERAR_FACTURAS",_connection,_transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using(var reader = cmd.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            var budget = new Budget
                            {
                                Id = (int)(reader["nro_factura"]),
                                Date = (DateTime)(reader["fecha"]),
                                Client = (string)(reader["cliente"]),
                                PayMethod = new PayMethod
                                {
                                    Id = (int)(reader["cod_formaPago"]),
                                    Name = (string)(reader["descripcion"])

                                }
                            };
                            var detail = new BudgetDetail()
                            {
                                Id = (int)(reader["cod_detalleFactura"]),
                                Count = (int)(reader["cantidad"]),
                                Article = new Article
                                {
                                    Cod_articulo = (int)(reader["cod_articulo"]),
                                    Nombre = (string)(reader["nombre"]),
                                    Pre_unitario = (decimal)(reader["pre_unitario"])
                                }
                            };

                            budget.AddDetail(detail);
                            budgets.Add(budget);
                        }


                }
                    catch (Exception ex)
                    {
                    // Manejar la excepción (por ejemplo, registrar el error)
                    Console.WriteLine("Error al leer los datos: " + ex.Message);
                }
            }   
            }
            return budgets;
        }

        public Budget GetById(int id)
        {
            Budget budget = null;
            try 
            {
                using (var cmd = new SqlCommand("SP_RECUPERAR_FACTURA_POR_CODIGO", _connection, _transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", id);
                    using (var reader = cmd.ExecuteReader())
                    {   
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No se encontraron facturas con el id solicitado");
                        }
                        while (reader.Read())
                        {
                            
                                budget = new Budget
                                {
                                    Id = (int)(reader["nro_factura"]),
                                    Date = (DateTime)(reader["fecha"]),
                                    Client = (string)(reader["cliente"]),
                                    PayMethod = new PayMethod
                                    {
                                        Id = (int)(reader["cod_formaPago"]),
                                        Name = (string)(reader["descripcion"])
                                    }
                                };
                            
                            var detail = new BudgetDetail()
                            {
                                Id = (int)(reader["cod_detalleFactura"]),
                                Count = (int)(reader["cantidad"]),
                                Article = new Article
                                {
                                    Cod_articulo = (int)(reader["cod_articulo"]),
                                    Nombre = (string)(reader["nombre"]),
                                    Pre_unitario = (decimal)(reader["pre_unitario"])
                                }
                            };
                            budget.AddDetail(detail);
                        }
                    }
                    return budget;
                }
                
            
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al leer los datos: " + ex.Message);
                return null;
            }
        }

        public bool Save(Budget budget)
        {
            
            try
            {
                using (var cmd = new SqlCommand("SP_GUARDAR_FACTURA", _connection, _transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod_formaPago", budget.PayMethod.Id);
                    cmd.Parameters.AddWithValue("@cliente", budget.Client);

                    SqlParameter param = new SqlParameter("@id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    budget.Id = (int)param.Value;//Asigno el id que me devuelve el procedimiento almacenado al objeto presupuesto

                }
                foreach (var detail in budget.GetDetails())
                {
                    using (var cmdDetail = new SqlCommand("SP_INSERTAR_DETALLE", _connection, _transaction))
                    {
                        cmdDetail.CommandType = CommandType.StoredProcedure;
                        cmdDetail.Parameters.AddWithValue("@cod_detalleFactura", detail.Id);
                        cmdDetail.Parameters.AddWithValue("@nro_factura", budget.Id);
                        cmdDetail.Parameters.AddWithValue("@cod_articulo", detail.Article.Cod_articulo);
                        cmdDetail.Parameters.AddWithValue("@cantidad", detail.Count);
                        cmdDetail.ExecuteNonQuery();
                    }
                }
                return true;


            }
            catch 
            {
                
                return false;
            }

        }
        public bool Update(Budget budget)
        {
            try
            {
                using (var cmd = new SqlCommand("SP_ACTUALIZAR_FACTURA", _connection, _transaction))
                {   
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", budget.Id);
                    cmd.Parameters.AddWithValue("@cliente", budget.Client);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    
                }

                using(var cmdDelete = new SqlCommand("Delete from detallesFacturas where nro_factura = @codigo", _connection, _transaction))
                {
                    cmdDelete.Parameters.AddWithValue("@codigo", budget.Id);
                    cmdDelete.ExecuteNonQuery();

                }
                foreach (var detail in budget.GetDetails())
                {
                    using (var cmdDetail = new SqlCommand("SP_INSERTAR_DETALLE", _connection, _transaction))
                    {
                        cmdDetail.CommandType = CommandType.StoredProcedure;
                        cmdDetail.Parameters.AddWithValue("@id_detalle", detail.Id);
                        cmdDetail.Parameters.AddWithValue("@nro_factura", budget.Id);
                        cmdDetail.Parameters.AddWithValue("@cod_articulo", detail.Article.Cod_articulo);
                        cmdDetail.Parameters.AddWithValue("@cantidad", detail.Count);
                        cmdDetail.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch(Exception ex)
            {   Console.WriteLine("Error al actualizar la factura: " + ex.Message);
                return false;
            }
        }
    }
}
