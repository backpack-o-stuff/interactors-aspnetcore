using System.Net;
using BOS.ClientLayer.ApplicationLayer.Monsters;
using Microsoft.AspNetCore.Mvc;

namespace BOS.ClientLayer.Controllers
{
    [Route("api/monsters")]
    [ApiController]
    public class MonsterController : Controller
    {
        private readonly ICreateMonsterInteractor _createMonster;

        public MonsterController(
            ICreateMonsterInteractor createMonster

            // EXAMPLE: you would have multiple interactors for their specific responsibility.
            // IViewAllMonstersInteractor viewAllMonsters,
            // IViewSpecificMonsterInteractor viewSpecificMonster,
            // IDeleteSpecificMonsterInteractor deleteSpecificMonster
        )
        {
            _createMonster = createMonster;
        }

        [HttpGet]
        public JsonResult Monsters()
        {
            // var result = _viewAllMonsters.Call(request);
            return Result(HttpStatusCode.OK, new {});
        }
        
        [HttpGet("{id}")]
        public JsonResult Monster(int id)
        {
            // var result = _viewSpecificMonster.Find(id);
            return Result(HttpStatusCode.OK, new {});
        }
        
        [HttpPost]
        public JsonResult AddMonster([FromBody] CreateMonsterRequest request)
        {
            var result = _createMonster.Call(request);
            return Result(HttpStatusCode.OK, result);
        }
                
        [HttpDelete("{id}")]
        public JsonResult RemoveMonster(int id)
        {
            // var result = _deleteSpecificMonster.Call(request);
            return Result(HttpStatusCode.OK);
        }
                
        private JsonResult Result(HttpStatusCode status)
        {
            return Result(status, new {});
        }

        private JsonResult Result(HttpStatusCode status, dynamic model)
        {
            var result = new JsonResult(new { Success = true, Model = model }) 
            {
                StatusCode = (int) status
            };
            return result;
        }
    }
}