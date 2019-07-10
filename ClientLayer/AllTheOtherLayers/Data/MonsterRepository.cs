using System.Collections.Generic;
using System.Linq;
using BOS.ClientLayer.AllTheOtherLayers.Entities;

namespace BOS.ClientLayer.AllTheOtherLayers.Data
{
    public interface IMonsterRepository
    {
        Monster Find(int id);
        List<Monster> All();
        Monster Add(Monster monster);
        Monster Update(Monster monster);
        void Remove(int id);
        void RemoveAll();
    }

    public class MonsterRepository : IMonsterRepository
    {
        private readonly List<Monster> _monsterPersistence;

        public MonsterRepository()
        {
            _monsterPersistence = new List<Monster>();
        }

        public Monster Find(int id)
        {
            return _monsterPersistence.FirstOrDefault(x => x.Id == id);
        }

        public List<Monster> All()
        {
            return _monsterPersistence;
        }

        public Monster Add(Monster monster)
        {
            var highestId = _monsterPersistence.Any()
                ? _monsterPersistence.Select(x => x.Id).Max(x => x)
                : 0;
            monster.Id = highestId + 1;
            _monsterPersistence.Add(monster);
            return monster;
        }

        public Monster Update(Monster monster)
        {
            var entity = _monsterPersistence.FirstOrDefault(x => x.Id == monster.Id);
            _monsterPersistence.Remove(entity);
            _monsterPersistence.Add(monster);
            return monster;
        }

        public void Remove(int id)
        {
            var entity = _monsterPersistence.FirstOrDefault(x => x.Id == id);
            _monsterPersistence.Remove(entity);
        }

        public void RemoveAll()
        {
            _monsterPersistence.Clear();
        }
    }
}