using ComercioApiRest.Data;
using ComercioApiRest.Models;
using System.Collections.Generic;
namespace ComercioApiRest.Services

{


public class ArticleService : IArticleService
{
    IArticleRepository _repo;
    public ArticleService(IArticleRepository repo)
    {
        _repo = repo;
    }
    public bool Delete(int id)
    {
        return _repo.Delete(id);
    }

    public List<Articulo> GetAll()
    {
        return _repo.GetAll();
    }

    public bool Save(Articulo articulo)
    {
        return _repo.Save(articulo);
    }
}
}
