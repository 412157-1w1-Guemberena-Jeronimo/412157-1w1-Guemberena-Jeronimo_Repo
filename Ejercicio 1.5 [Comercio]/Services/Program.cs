using Ejercicio_1._5__Comercio_.Domain;
using Ejercicio_1._5__Comercio_.Services;
using System.ComponentModel.DataAnnotations;

ArticleService aService = new ArticleService();
//Mostrar un articulo por id
Article? articleById = aService.GetArticleById(2);
if (articleById != null)
{
    Console.WriteLine(articleById);
}
else
{
    Console.WriteLine("No se pudo mostrar un articulo con ese id");
}
Console.WriteLine("--------------------------------------------------"); //Los id que estan en los select/delete/select by id seguramente no esten para el momento de la revision
                                                                         //En la base de datos agregue nuevos con ids mas altos, pero en resumen todo funciona
//Eliminar un articulo
bool deleted = aService.Delete(2);
if (deleted)
{
    Console.WriteLine("Se elimino el articulo");
}
else
{
    Console.WriteLine("No se pudo eliminar un articulo con ese id");
}
Console.WriteLine("--------------------------------------------------");
//Guardar un articulo
Article newArticle = new Article()
{
    Cod_articulo = 0,
    Nombre = "Bolsa de pochoclos",
    Pre_unitario = 200
};
bool isSaved = aService.Save(newArticle);
if (isSaved)
{
    Console.WriteLine("Articulo guardado correctamente");
}
else
{
    Console.WriteLine("No se pudo guardar el articulo");
}
Console.WriteLine("--------------------------------------------------");
//Mostrar todos los articulos
List<Article> articles = aService.GetArticles();
if (articles.Count > 0)
{
    foreach (Article article in articles)
    {
        Console.WriteLine(article);
    }
}
else
{
    Console.WriteLine("No hay articulos para mostrar");
}
Console.WriteLine("--------------------------------------------------");
var bService = new BudgetService(Ejercicio_1._5__Comercio_.Properties.Resources.CadenaConexionLocal);
//insertar factura
var budget = new Budget()
{
    Client = "Pepito",
    PayMethod = new PayMethod() { Id = 1 }
};
budget.AddDetail(new BudgetDetail()
{
    Id = 10,
    Article = new Article() { Cod_articulo = 1 },
    Count = 6


});

bool result = bService.SaveBudget(budget);
if (result)
{
    Console.WriteLine("Detalle agregado correctamente");
}
else
{
    Console.WriteLine("No se pudo agregar el detalle");
}
Console.WriteLine("--------------------------------------------------");
////Consultar facturas con detalles
Console.WriteLine("Facturas con detalles: ");
Console.WriteLine("--------------------------------------------------");
List<Budget> budgets = bService.GetAllBudgets();
if (budgets.Count > 0)
{
    foreach (Budget b in budgets)
    {
        Console.WriteLine("Factura: " + b);
        var details = b.GetDetails();
        if (details.Count > 0)
        {
            foreach (var d in details)
            {
                Console.WriteLine("\tDetalle: " + d);
            }
        }
        else
        {
            Console.WriteLine("\t No hay detalles para mostrar");
        }
    }
}
//Factura con id
Console.WriteLine("--------------------------------------------------");
var budgetById = bService.GetBudgetById(2);
if (budgetById != null)
{
    Console.WriteLine("Factura con id: " + budgetById);
    var details = budgetById.GetDetails();
    if (details.Count > 0)
    {
        foreach (var d in details)
        {
            Console.WriteLine("\tDetalle de la factura con id: " + d);
        }
    }
    else
    {
        Console.WriteLine("\t No hay detalles para mostrar");
    }
}
else
{
    Console.WriteLine("No se encontro la factura con ese id");
}
//Actualizar factura
Console.WriteLine("--------------------------------------------------");
Budget budgetup = new Budget()
{
    Id = 4,
    Client = "Jorge",
    PayMethod = new PayMethod() { Id = 2 }
};
budgetup.GetDetails().Clear();
budgetup.AddDetail(new BudgetDetail()
{   Id = 4,
    Article = new Article() { Cod_articulo = 2 },
    Count = 3
});
bool resultUpdate = bService.UpdateBudget(budgetup);
if(resultUpdate)
{
    Console.WriteLine("Factura actualizada correctamente");
}
else
{
    Console.WriteLine("No se pudo actualizar la factura");
}









