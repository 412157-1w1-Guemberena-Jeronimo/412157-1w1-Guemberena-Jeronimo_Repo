using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1._5__Comercio_.Data
{
    public class UnitOfWork : IDisposable//El disposable es para liberar recursos
    {
        private readonly SqlConnection _connection;//Que sea solo de lectura quiere decir que no se va a poder modificar
        private SqlTransaction _transaction;//
        private IBudgetRepository _repository;//Interfaz del repositorio

        public UnitOfWork(string cnnString)
        {
            _connection = new SqlConnection(cnnString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();

        }

        public IBudgetRepository BudgetRepository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new BudgetRepository(_connection,_transaction);
                }
                return _repository;
            }
        }
        public void SaveChanges()
        {
            try
            {
                _transaction.Commit();
            }
            catch(Exception ex)
            {
                _transaction.Rollback();
                throw new Exception ("Error al guardar cambios en la base de datos");
            }
            
        }
        public void Dispose()
        {
            if(_transaction != null)
            {
                _transaction.Dispose();
                
            }
            if(_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
