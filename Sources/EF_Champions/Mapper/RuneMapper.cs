using EF_Champions.Entities;
using Model;


namespace EF_Champions.Mapper
{
    public static class RuneMapper
    {
        public static RuneEntity RuneToEntity(this Rune rune)
        {
            return new RuneEntity
            {
                Name = rune.Name,
                Description = rune.Description,
                RuneFamily = rune.Family,
                Icon = rune.Icon,
                Image = rune.Image.Base64
            };
        }
    }
}
