using Api.Pagination;
using DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using OLO_Champignons.Controllers;
using StubLib;
using System.Net;

namespace ApiControllers
{
    [TestClass]
    public class ChampionControllerTest
    {
        // attribut controlleur
        public ChampionsController controller = new(new StubData(), new NullLogger<ChampionsController>());

        [TestMethod]
        public async Task TestGetAsync()
        {
            // arrange
            List<ChampionDto> championsExpected = new()
            {
                new ChampionDto() {Name = "Akali", Class = "Assassin", Icon = "", Bio = "", Image = ""},
                new ChampionDto() {Name = "Aatrox", Class = "Fighter", Icon = "", Bio = "", Image = ""},
                new ChampionDto() {Name = "Ahri", Class = "Mage", Icon = "", Bio = "", Image = ""},
                new ChampionDto() {Name = "Akshan", Class = "Marksman", Icon = "", Bio = "", Image = ""},
                new ChampionDto() {Name = "Bard", Class = "Support", Icon = "", Bio = "", Image = ""},
                new ChampionDto() {Name = "Alistar", Class = "Tank", Icon = "", Bio = "", Image = ""},

            };
            // act
            // ici on verifie que la requete vers l'api a bien marché
            var championResult = await controller.Get(new PageRequest(0,5));
            championResult.Should().NotBeNull();

            // assert
            // ici on verifie que la requete a bien retourner quelque chose (et pas vide)
            var objectResult = championResult as ObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200);

            /* finalement on verifie que la liste retourner par la requete et la même
            * que celle créer avant (qui est sensé a être pareil que celle du stub. 
            * Maintenant qu'on a la pagination, il faut également vérifer que cela est 
            * bien retourner.
            */
            var page = objectResult.Value as Page;
            page.Should().NotBeNull();
            page.MyChampions.Should().NotBeNull();
            // seulement les 5 premiers champions
            page.MyChampions.Should().BeEquivalentTo(championsExpected.Take(5));

            page.Index.Should().Be(0);
            page.Count.Should().Be(5);
            page.TotalCount.Should().Be(championsExpected.Count);
        }

        [TestMethod]
        public async Task TestPostAsync()
        {
            // arrange
            ChampionDto champion = new ChampionDto() 
            { 
                Name = "TheChamp", 
                Class = "Marksman", 
                Icon = "myIcon", 
                Bio = "myBio", 
                Image = "myImage" 
            };
            var nbChamps = await controller.dataManager.ChampionsMgr.GetNbItems();

            // act
            var championResult = await controller.Post(champion);
            championResult.Should().NotBeNull();
           
            // assert
            var objectResult = championResult as ObjectResult;
            objectResult.Should().NotBeNull();
            
            // je récupère le champion retourner par la requete
            var returnedChampion = objectResult.Value as ChampionDto;
            returnedChampion.Should().NotBeNull();

            /* ici je verifie que le nom du champion ajouté correspond a celui qu'on voulais 
             ajouté. De plus je verifie le que le count des champions a bien augmenter de 1.
            
            Le problème c'est que en faisant ça cela n'est plus un test UNITAIRE vu qu'on 
            dépend de la methode GetNbItems.*/

            returnedChampion.Name.Should().Be(champion.Name);
            nbChamps.Should().Be(await controller.dataManager.ChampionsMgr.GetNbItems() - 1);
        }

        [TestMethod]
        public async Task TestPutAsync()
        {
            // arrange
            ChampionDto newChampion = new ChampionDto() { Name = "Pluto" };

            // act
            var championResult = await controller.Put("Akali", newChampion);

            // assert
            var objectResult = championResult as ObjectResult;
            objectResult.Should().NotBeNull();
            var returnedChampion = objectResult.Value as ChampionDto;
            returnedChampion.Should().NotBeNull();
            returnedChampion.Name.Should().Be(newChampion.Name);
        }

        [TestMethod]
        public async Task TestDeleteAsync()
        {
            // arrange
            string championName = "Akali";

            // act
            var championResult = await controller.Delete(championName);

            // assert
            var objectResult = championResult as ObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task TestGetByNameAsync()
        {
            // arrange
            ChampionDto expectedChampion = new ChampionDto 
            { 
                Name = "Akali",
                Class = "Assassin",
                Icon = "",
                Bio = "",
                Image = ""
            };

            // act
            var actionResult = await controller.GetByName("Akali");
            var objectResult = actionResult as ObjectResult;
            var returnedChampion = objectResult?.Value as ChampionDto;

            // assert
            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            returnedChampion.Should().NotBeNull();
            returnedChampion.Should().BeEquivalentTo(expectedChampion);
        }
    }
}