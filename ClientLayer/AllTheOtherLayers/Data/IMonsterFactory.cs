using BOS.ClientLayer.AllTheOtherLayers.Entities;
using BOS.ClientLayer.ApplicationLayer.Monsters;

namespace BOS.ClientLayer.AllTheOtherLayers.Data
{
    // Request to Entity mapper, feel free to manual, automap, or what ever
    public interface IMonsterFactory
    {
        Monster For(CreateMonsterRequest request);
    }

    public class MonsterFactory : IMonsterFactory
    {
        public Monster For(CreateMonsterRequest request)
        {
            return new Monster
            {
                Name = request.Name,
                Power = request.Power
            };
        }
    }
}