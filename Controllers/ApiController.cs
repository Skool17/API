using Microsoft.AspNetCore.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    [Route("/values")]
    public class ApiController : Controller
    {
        private static List<Values> values = new List<Values>(new[] //создаем список элементов (значений)
        {
            new Values() {Id = 1, Name = "Value1"},
            new Values() {Id = 2, Name = "Value2"},
            new Values() {Id = 3, Name = "Value3"},
        });

        [HttpGet]       // для получения списка с помощью API использует метод Get

        public IEnumerable<Values> Get() => values;

        [HttpGet("{id}")]       //функция для поиска элемента с определенным ID

        public IActionResult Get(int id)  
        {
            var value = values.SingleOrDefault(p => p.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpDelete("{id}")]  //удаление элемента с определенным id
        
        public IActionResult Delete(int id)
        {
            values.Remove(values.SingleOrDefault(p => p.Id == id));
            return Ok();
        }

        private int NextValueId => values.Count() == 0 ? 1 : values.Max(x => x.Id) + 1;

        [HttpPost]  //добавление элемента

        public IActionResult Post(Values value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            value.Id = NextValueId;
            values.Add(value);
            return CreatedAtAction(nameof(Get), new { id = value.Id },
                value);
        }

        /* [HttpPost("AddValue")]

         public IActionResult PostBody([FromBody] Values value) => Post(value);*/

        [HttpPut] //изменение данных элемента

        public IActionResult Put(Values value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var oldValue = values.SingleOrDefault(p => p.Id == value.Id);
            if (oldValue == null) return NotFound();
            oldValue.Name = value.Name;
            return Ok(oldValue);
        }
       /* public IActionResult Index()
        {
            return View();
        }*/
    }
}
