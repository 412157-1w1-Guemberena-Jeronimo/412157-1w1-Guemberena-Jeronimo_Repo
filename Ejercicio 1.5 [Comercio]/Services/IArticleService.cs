using Ejercicio_1._5__Comercio_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1._5__Comercio_.Services
{
    public interface IArticleService
    {
        List<Article> GetArticles();
        Article? GetArticleById(int id);
        bool Delete(int id);
        bool Save(Article article);
    }
}
