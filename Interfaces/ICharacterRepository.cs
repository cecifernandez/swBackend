using swBackend.Models;

namespace swBackend.Interfaces
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<CharacterModel>> GetAllCharacters();
        Task<Dictionary<string, string>> GetFilmsUrlToTitleMap();
    }
}
