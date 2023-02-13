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
                new ChampionDto() {Name = "Alistar"},

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
    }
}