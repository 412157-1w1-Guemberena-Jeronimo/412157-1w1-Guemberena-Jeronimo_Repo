using Ejercicio_1._5__Comercio_.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio_1._5__Comercio_.Domain;

namespace Ejercicio_1._5__Comercio_.Services
{
    public class ArticleService : IArticleService
    {
        IArticleRepository _repository;

        public ArticleService()
        {
             _repository = new ArticleRepository();
        }
        public List<Article> GetArticles()
        {
            return _repository.GetAllArticles();
        }
        public Article? GetArticleById(int id)
        {
            return _repository.GetArticleById(id);
        }
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
        public bool Save(Article article)
        {
            return _repository.Save(article);
        }
    }
}
