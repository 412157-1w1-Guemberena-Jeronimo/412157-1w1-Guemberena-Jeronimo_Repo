using Ejercicio_1._5__Comercio_.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1._5__Comercio_.Data
{
    public class ArticleRepository : IArticleRepository
    {
        public bool Delete(int id)
        {
            List<ParameterSP> parameters = new List<ParameterSP>()//Creamos el parametro que le vamos a pasar despues
            {
                new ParameterSP()
                {
                    Name= "@codigo",  
                    Value= id
                }
            };

             int rowsAffected = DataHelper.GetInstance().ExcecuteSPDMLQuery("SP_ELIMINAR_ARTICULO", parameters);
             return rowsAffected > 0;

            
            
        }

        public List<Article> GetAllArticles()
        {
            List<Article> lst = new List<Article>();
            var dt = DataHelper.GetInstance().ExcecuteSPQuery("SP_RECUPERAR_ARTICULOS");
            foreach (DataRow row in dt.Rows)
            {
                Article a = new Article();
                a.Cod_articulo = (int)row["cod_articulo"];
                a.Nombre = (string)row["nombre"];
                a.Pre_unitario = (decimal)row["pre_unitario"];
                lst.Add(a);
            }
            return lst;

        }

        public Article? GetArticleById(int id)
        {
            
            List<ParameterSP> parameters = new List<ParameterSP>()
            {
                new ParameterSP()
                {Name= "@codigo", Value=id }
            };
            //traemos el articulo a traves del sp
            var dt = DataHelper.GetInstance().ExcecuteSPQuery("SP_RECUPERAR_ARTICULO_POR_CODIGO", parameters);//ponemos la variable de parameters porque el sp necesita un parametro
            //si vino un registro lo mapeamos y devolvemos el articulo
            if (dt != null & dt.Rows.Count > 0)
            {
                Article a = new Article()
                {
                    Cod_articulo = (int)dt.Rows[0]["cod_articulo"],
                    Nombre = (string)dt.Rows[0]["nombre"],
                    Pre_unitario = (decimal)dt.Rows[0]["pre_unitario"]
                };
                return a;
            }
            else
            {
                return null;
            }
        }

        public bool Save(Article article)
        {
                try
                {
                List<ParameterSP> parameters = new List<ParameterSP>()
                {
                    new ParameterSP { Name= "@codigo", Value= article.Cod_articulo},
                    new ParameterSP { Name= "@nombre", Value= article.Nombre },
                    new ParameterSP { Name= "@precio", Value= article.Pre_unitario }
                };

                int rowsAffected = DataHelper.GetInstance().ExcecuteSPDMLQuery("SP_GUARDAR_ARTICULO", parameters);
                return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Manejar la excepción (por ejemplo, registrar el error)
                    Console.WriteLine(ex.Message);
                    return false;
                }







        }


    }
}
