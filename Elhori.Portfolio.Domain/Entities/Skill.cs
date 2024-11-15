using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elhori.Portfolio.Domain.Dtos;

namespace Elhori.Portfolio.Domain.Entities;

public class Skill
{
    private Skill() { }

    public Skill(SkillDto dto)
    {
        Name = dto.Name;
        Icon = dto.Icon;
    }

    public int Id { get; private set; }

    /// <summary>
    /// the name of the skill.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// The icon of the skill from font awesome.
    /// </summary>
    public string Icon { get; private set; } = string.Empty;

    public void Update(SkillDto dto)
    {
        Name = dto.Name;
        Icon = dto.Icon;
    }

    public SkillDto ToDto()
    {
        return new SkillDto(Id, Name, Icon);
    }
}