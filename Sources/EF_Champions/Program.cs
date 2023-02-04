// See https://aka.ms/new-console-template for more information
using EF_Champions;
using Microsoft.Extensions.DependencyModel;
using Model;
using StubLib;

Console.WriteLine("Hello, World!");

/*using ici permet de explicité le nettoyage de mémoire qui n'est pas implicite car le 
 * garbage collector de dotnet ne peut pas s'occuper de connections a la base de donnée 
 * par lui même*/
using (var context = new ChampDbContext())
{
    context.Champions.AddRange(
        new ChampionEntity
        {
            name = "Akali"
        },
        new ChampionEntity
        {
            name = "Aatrox"
        },
        new ChampionEntity
        {
            name = "Ahri"
        }
    );
    context.SaveChanges(); // permet de synchroniser les ajouts a la base de do
}
