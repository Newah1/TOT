using System.ComponentModel.DataAnnotations;

namespace TheOregonTrailAI.Models;

public class CharacterModel
{
    [Key]
    public int CharacterId { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Occupation { get; set; }
}