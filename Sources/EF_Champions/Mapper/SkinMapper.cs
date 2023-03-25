using EF_Champions.Entities;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions.Mapper
{
    public static class SkinMapper
    {
        public static SkinEntity SkinToEntity(this Skin skin)
        {
            return new SkinEntity
            {
                Name = skin.Name,
                Price = skin.Price,
                Description = skin.Description,
                Image = skin.Image.Base64,
                Icon = skin.Icon,
                Champion = skin.Champion.ChampionToEntity()
            };
        }

        public static Skin EntityToModel(this SkinEntity skinEntity)
        {
            return new Skin(skinEntity.Name, skinEntity.Champion.EntityToModel(), skinEntity.Price, null, skinEntity.Description);
        }


    }
}
