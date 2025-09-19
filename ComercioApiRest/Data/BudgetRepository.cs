using ComercioApiRest.Models;
using Microsoft.EntityFrameworkCore;

namespace ComercioApiRest.Data
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly DbContextComercio _context;
        public BudgetRepository(DbContextComercio context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            if(id>0)
            {
                var factura = _context.Facturas.Find(id);
                if (factura != null)
                {
                    _context.Facturas.Remove(factura); //Esto deberia ser una baja logica pero en mi base de datos no tengo campo para eso
                    return _context.SaveChanges() > 0;
                }
                return false;
            }
            return false;
        }

        public List<Factura>? GetAll()
        {
            var lst = _context.Facturas.Include(f => f.DetallesFacturas).ToList();
            if(lst != null && lst.Count > 0)
            {
                return lst;
            }
            return null;
        }

        public bool Save(Factura factura)
        {
            if(factura.Nro_Factura == 0)
            {
                _context.Facturas.Add(factura); 
            }
            else
            {
                _context.Facturas.Update(factura);
            }
            return _context.SaveChanges()>0;
        }
    }
}
