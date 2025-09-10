using Ejercicio_1._5__Comercio_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1._5__Comercio_.Data
{   //Primer paso
    public interface IArticleRepository
    {
        List<Article> GetAllArticles();

        Article GetArticleById(int id);

        bool Save(Article article);

        bool Delete(int id);


    }
}
