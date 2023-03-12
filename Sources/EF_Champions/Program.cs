// See https://aka.ms/new-console-template for more information
using EF_Champions;
using EF_Champions.Entities;
using EF_Champions.Mapper;
using Microsoft.EntityFrameworkCore;
using Model;
using StubLib;

class Program
{
    static void Main()
    {
        Console.WriteLine("Starting...");

        // insertion de one-to-many avec skin
        using (ChampDbContext db = new ChampDbContext())
        {
            //...

            ChampionEntity akali = new ChampionEntity { Name = "Akali", Class = ChampionClass.Assassin };
            SkinEntity[] skins_akali =
            {
                new SkinEntity() { Name = "Skin1", Champion = akali },
                new SkinEntity() { Name = "Skin2", Champion = akali },
                new SkinEntity() { Name = "Skin3", Champion = akali },
             };

            foreach (var m in skins_akali)
            {
                akali.Skins.Add(m);
            }

            db.Add(akali);
            db.SaveChanges();
        }

        // display on console to check
        using (ChampDbContext db = new ChampDbContext())
        {
            Console.WriteLine("Champions : ");
            foreach (var c in db.Champions.Include(c => c.Skins))
            {
                Console.WriteLine($"\t{c.Id}: {c.Name}");
                foreach (var s in c.Skins)
                {
                    Console.WriteLine($"\t\t{s.Name}");
                }
            }

            Console.WriteLine();

            Console.WriteLine("Skins :");
            foreach (var s in db.Skins)
            {
                Console.WriteLine($"\t{s.Id}: {s.Name} (Champion : {s.Champion.Name})");
            }
        }

        // add fake Skill data
        using (ChampDbContext db = new ChampDbContext())
        {
            ChampionEntity aatrox = new ChampionEntity { Name = "Aatrox", Class = ChampionClass.Mage };
            SkillEntity[] skills =
            {
                new SkillEntity() { Name = "Skill1", Description = "desc..1", SkillType = SkillType.Basic },
                new SkillEntity() { Name = "Skill2", Description = "desc..2", SkillType = SkillType.Passive },
                new SkillEntity() { Name = "Skill2", Description = "desc..3", SkillType = SkillType.Passive }
            };

            SkinEntity[] skins_aatrox =
            {
                new SkinEntity() { Name = "Skin4", Champion = aatrox },
                new SkinEntity() { Name = "Skin5", Champion = aatrox },
                new SkinEntity() { Name = "Skin6", Champion = aatrox },
             };

            foreach (var m in skins_aatrox)
            {
                aatrox.Skins.Add(m);
            }

            foreach (var sk in skills)
            {
                sk.Champions.Add(aatrox);
                aatrox.Skills.Add(sk);
            }

            db.Add(aatrox);
            db.SaveChanges();
        }

        // add fake Rune / RunePage data
        using (ChampDbContext db = new ChampDbContext())
        {
            RuneEntity r1 = new RuneEntity() { Name = "Conqueror", RuneFamily = RuneFamily.Precision };
            RuneEntity r2 = new RuneEntity() { Name = "Triumph", RuneFamily = RuneFamily.Domination };
            RuneEntity r3 = new RuneEntity() { Name = "Legend: Alacrity", RuneFamily = RuneFamily.Unknown };
            RuneEntity r4 = new RuneEntity() { Name = "Legend: Tenacity", RuneFamily = RuneFamily.Precision };


            RunePageEntity p1 = new RunePageEntity() { Name = "Page1" };
            RunePageEntity p2 = new RunePageEntity() { Name = "Page2" };

            // p1 has r1, r2, r3
            p1.Runes.Add(r1);
            p1.Runes.Add(r2);
            p1.Runes.Add(r3);

            // p2 has r4, r2, r3
            p2.Runes.Add(r2);
            p2.Runes.Add(r3);
            p2.Runes.Add(r4);

            ChampionEntity champ = new ChampionEntity { Name = "Third Champion", Class = ChampionClass.Mage };
            champ.PagesRune.Add(p1);
            champ.PagesRune.Add(p2);

            db.Add(champ);
            db.AddRange(r1,r2,r3,r4);
            db.AddRange(p1,p2);
            db.SaveChanges();

            

            /*Rune[] runes =
            {
                new Rune("Conqueror", RuneFamily.Precision),
                new Rune("Triumph", RuneFamily.Precision),
                new Rune("Legend: Alacrity", RuneFamily.Precision),
                new Rune("Legend: Tenacity", RuneFamily.Precision),
                new Rune("last stand", RuneFamily.Domination),
                new Rune("last stand 2", RuneFamily.Domination),
            };

            foreach (var run in runes)
            {
                db.Add(run.RuneToEntity());
            }

            RunePage runePage1 = new RunePage("rune page 1 from stub");
            runePage1[RunePage.Category.Major] = runes[0];
            runePage1[RunePage.Category.Minor1] = runes[1];
            runePage1[RunePage.Category.Minor2] = runes[2];
            runePage1[RunePage.Category.Minor3] = runes[3];
            runePage1[RunePage.Category.OtherMinor1] = runes[4];
            runePage1[RunePage.Category.OtherMinor2] = runes[5];

            db.Add(runePage1.RunePageToEntity());
            db.SaveChanges();*/
        }
    }
}


// --------------------------------------------------------------------------------------------------------------
/* TEST SKINS
 * 
 * StubData stub = new();
var skins = (await stub.SkinsMgr.GetItems(0,
               (await stub.SkinsMgr.GetNbItems()))).Select(s => s?.SkinToEntity());

using (var context = new SkinDbContext())
{
    foreach (SkinEntity skin in skins)
    {
        context.Skins.Add(skin);
    }
    context.SaveChanges();
}*/


/* TEST CHAMPIONS
 * 
 * using ici permet de explicité le nettoyage de mémoire qui n'est pas implicite car le 
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
