using ComercioApiRest.Models;

namespace ComercioApiRest.Data
{
    public class ArticleRepository : IArticleRepository
    {   DbContextComercio _context;
        public ArticleRepository(DbContextComercio context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            if(id>0)
            {
                var articulo = _context.Articulos.Find(id);
                if(articulo != null)
                {
                    _context.Articulos.Remove(articulo);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }

        public List<Articulo>? GetAll()
        {
            var lst = _context.Articulos.ToList();
            if(lst != null && lst.Count > 0)
            {
                return lst;
            }
            return null;
        }

        public bool Save(Articulo articulo)
        {
            if(articulo.CodArticulo == 0)
            {
                _context.Articulos.Add(articulo); 
            }
            else
            {
                _context.Articulos.Update(articulo);
            }
            return _context.SaveChanges()>0;
        }
    }
}
