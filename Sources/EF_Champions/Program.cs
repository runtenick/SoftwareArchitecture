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
            Name = "Akali",
            Class = (ChampClassEntity)1
        },
        new ChampionEntity
        {
            Name = "Aatrox",
            Class = (ChampClassEntity)2
        },
        new ChampionEntity
        {
            Name = "Ahri",
            Class = (ChampClassEntity)3
        }
    ) ;
    context.SaveChanges(); // permet de synchroniser les ajouts a la base de do
}


// Afficher le contenu de la base en console
using (var context2 = new ChampDbContext())
{
    var champions = context2.Champions;

    foreach (var champion in champions)
    {
        Console.WriteLine($"{champion.Id} : {champion.Name}");
    }
}
