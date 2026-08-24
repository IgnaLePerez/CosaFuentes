using EscapeRoomCuento.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoomCuento.Controllers
{
    public class HomeController : Controller
    {
        // -----------------------------------------------------------------
        // DEMO: acá van las 5 preguntas del cuento. Cuando tengas las
        // preguntas reales, reemplazá esta lista (Text, Options y
        // CorrectDigit de cada una). CorrectDigit va de 1 a 4 y es el
        // dígito que el jugador debe cargar en esa posición del candado.
        // -----------------------------------------------------------------
        private static readonly List<Question> _questions = new()
        {
            new Question
            {
                Id = 1,
                Text = "Pregunta de ejemplo 1: ¿De qué color era la capa del protagonista?",
                Options = new() { "Roja", "Azul", "Verde", "Dorada" },
                CorrectDigit = 1
            },
            new Question
            {
                Id = 2,
                Text = "Pregunta de ejemplo 2: ¿Dónde se escondía el tesoro?",
                Options = new() { "En el bosque", "Bajo el puente", "En la torre", "En la cueva" },
                CorrectDigit = 3
            },
            new Question
            {
                Id = 3,
                Text = "Pregunta de ejemplo 3: ¿Quién ayudó al héroe al final?",
                Options = new() { "El mago", "El herrero", "La reina", "El zorro" },
                CorrectDigit = 4
            },
            new Question
            {
                Id = 4,
                Text = "Pregunta de ejemplo 4: ¿Cuántos hermanos tenía el personaje principal?",
                Options = new() { "Uno", "Dos", "Tres", "Ninguno" },
                CorrectDigit = 2
            },
            new Question
            {
                Id = 5,
                Text = "Pregunta de ejemplo 5: ¿Qué objeto rompió el hechizo?",
                Options = new() { "Un espejo", "Una campana", "Un anillo", "Una llave" },
                CorrectDigit = 3
            },
        };

        public IActionResult Index()
        {
            var viewModel = _questions
                .Select(q => new QuestionViewModel
                {
                    Id = q.Id,
                    Text = q.Text,
                    Options = q.Options
                })
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        [Route("Home/CheckCode")]
        public IActionResult CheckCode([FromBody] CheckCodeRequest request)
        {
            var response = new CheckCodeResponse();

            if (request?.Code == null || request.Code.Count != _questions.Count)
            {
                response.Success = false;
                return Json(response);
            }

            var ordered = _questions.OrderBy(q => q.Id).ToList();

            for (int i = 0; i < ordered.Count; i++)
            {
                response.CorrectPositions.Add(request.Code[i] == ordered[i].CorrectDigit);
            }

            response.Success = response.CorrectPositions.All(x => x);

            return Json(response);
        }

        public IActionResult Error() => View();
    }
}
