using DTO;
using Model;

namespace Api.Mapper
{
    public static class SkinMapper
    {
        public static SkinDto ToDto(this Skin skin)
        {
            return new SkinDto()
            {
                Name = skin.Name,
                Description = skin.Description,
                Icon = skin.Icon,
                Image = skin.Image.Base64,
                Price = skin.Price,
                Champion = skin.Champion.Name
            };
        }

        public static Skin ToModel (this SkinDto skinDto)
        {
            throw new NotImplementedException ();
        }
    }
}
