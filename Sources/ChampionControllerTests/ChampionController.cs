using DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OLO_Champignons.Controllers;
using StubLib;

namespace ApiControllers
{
    [TestClass]
    public class ChampionControllerTest
    {
        // attribut controlleur
        public ChampionsController controller = new(new StubData());

        [TestMethod]
        public async Task TestGetAsync()
        {
            // arrange
            List<ChampionDto> champions = new()
            {
                new ChampionDto() {Name = "Akali"},
                new ChampionDto() {Name = "Aatrox"},
                new ChampionDto() {Name = "Ahri"},
                new ChampionDto() {Name = "Akshan"},
                new ChampionDto() {Name = "Bard"},
                new ChampionDto() {Name = ""},

            };
            // act
            // ici on verifie que la requete vers l'api a bien marché
            var championResult = await controller.Get();
            championResult.Should().NotBeNull();

            // assert
            // ici on verifie que la requete a bien retourner quelque chose (et pas vide)
            var objectResult = championResult as ObjectResult;
            objectResult.Should().NotBeNull();

            // finalement on verifie que la liste retourner par la requete et la même
            // que celle créer avant (qui est sensé a être pareil que celle du stub.
            var champs = objectResult?.Value as IEnumerable<ChampionDto>;
            champs.Should().NotBeNull();
            champs.Should().BeEquivalentTo(champions);
        }

        [TestMethod]
        public async Task TestPostAsync()
        {
            // arrange
            ChampionDto champion = new ChampionDto() { Name = "TheChamp" };
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
    }
}