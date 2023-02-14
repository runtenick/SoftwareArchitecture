// See https://aka.ms/new-console-template for more information
using EF_Champions;
using EF_Champions.Entities;
using EF_Champions.Mapper;
using Microsoft.EntityFrameworkCore;
using StubLib;

Console.WriteLine("Starting...");

StubData stub = new();
var skins = (await stub.SkinsMgr.GetItems(0,
               (await stub.SkinsMgr.GetNbItems()))).Select(s => s?.SkinToEntity());

using (var context = new SkinDbContext())
{
    foreach (SkinEntity skin in skins)
    {
        context.Skins.Add(skin);
    }
    context.SaveChanges();
}


/*using ici permet de explicité le nettoyage de mémoire qui n'est pas implicite car le 
 * garbage collector de dotnet ne peut pas s'occuper de connections a la base de donnée 
 * par lui même*//*
using (var context = new ChampDbContext())
{
    context.Champions.AddRange(
        new ChampionEntity
        {
            Name = "Akali",
            Class = ChampClassEntity.Assassin
        },
        new ChampionEntity
        {
            Name = "Aatrox",
            Class = ChampClassEntity.Tank
        },
        new ChampionEntity
        {
            Name = "Ahri",
            Class = ChampClassEntity.Mage
        }
    );
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
}*/
