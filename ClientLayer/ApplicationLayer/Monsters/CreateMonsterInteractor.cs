    // INTENT
    // Interactors are single method services that are responsibile for
    // all of the necessary dependencies and logic to perform a business
    // need/case.
    // When a new business need/case arrises, a new interactor will be
    // created to handle such concearns.

    // NOTE 
    // Where it all lives: There is nothing wrong with splitting these
    // files out. In staticly typed languages, I tend to keep their boundary
    // contract and reqeusts inline to further highlight their nature.

    // NOTE
    // Eventually you will want to chain a series of interactors together 
    // to form a larger 'process'. In such cases, an Interactor Coordinator
    // with the same style of use Result Call(Request) would be created to wrap
    // the coordination of and between the interactors it wraps to create a larger
    // business need/case.
    // 
    // ex. playerDefeatedByMonsterInteractor
    // Would call and coordinate the following:
    //   defeatPlayerInterator
    //   levelupMonsterInteractor
    //   monsterScoresPlayerDefeatInteractor

using BOS.ClientLayer.AllTheOtherLayers.Data;
using BOS.ClientLayer.AllTheOtherLayers.Entities;

namespace BOS.ClientLayer.ApplicationLayer.Monsters
{
    public interface ICreateMonsterInteractor 
        : IInteractor<Monster, CreateMonsterRequest> {}

    public class CreateMonsterRequest
    {
        public string Name { get; set; }
        public int Power { get; set; }
    }

    public class CreateMonsterInteractor : InteractorBase, ICreateMonsterInteractor
    {
        private readonly IMonsterRepository _monsterRepository;
        private readonly IMonsterFactory _monsterFactory;

        public CreateMonsterInteractor(
            IMonsterRepository monsterRepository,
            IMonsterFactory monsterFactory
        )
        {
            _monsterRepository = monsterRepository;
            _monsterFactory = monsterFactory;
        }

        public InteractorResult<Monster> Call(CreateMonsterRequest request)
        {
            return PerformCall(() => 
            {
                var monster = _monsterFactory.For(request);

                var monsterValidation = monster.Validate();
                if(!monsterValidation.IsValid)
                    return InteractorResult<Monster>.ForFailure(monsterValidation.Messages);

                var result = _monsterRepository.Add(monster);
                return InteractorResult<Monster>.ForSuccess(result);
            });
        }
    }
}