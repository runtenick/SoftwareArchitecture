using EF_Champions.Entities;
using Model;

namespace EF_Champions.Mapper
{
    public static class SkillMapper
    {
        public static SkillEntity SkillToEntity(this Skill skill)
        {
            return new SkillEntity
            {
                Name = skill.Name,
                Description = skill.Description,
                SkillType = skill.Type
            };
        }

        public static Skill EntityToModel(this SkillEntity skillEntity)
           => new Skill(skillEntity.Name, skillEntity.SkillType, skillEntity.Description);


    }
}
