# Sala de Escape del Cuento

Demo en ASP.NET Core MVC (.NET 8) de una sala de escape con un candado de 5
dígitos (1 a 4) y 5 preguntas sobre un cuento, presentadas en popups estilo
Kahoot.

## Cómo correrlo

Necesitás tener instalado el [.NET 8 SDK](https://dotnet.microsoft.com/download).

```bash
cd EscapeRoomCuento
dotnet run
```

Luego abrí en el navegador la URL que muestra la consola (algo como
`https://localhost:5001`).

## Cómo funciona

- **`Models/Question.cs`**: guarda el texto, las 4 opciones y el `CorrectDigit`
  (1 a 4) de cada pregunta. Vive solo en el servidor.
- **`Models/QuestionViewModel.cs`**: versión "segura" de la pregunta que sí
  viaja a la vista (sin la respuesta correcta), para que no se pueda ver el
  código mirando el HTML.
- **`Controllers/HomeController.cs`**:
  - `Index()` arma la lista de preguntas (sin respuestas) y las pasa a la vista.
  - `CheckCode()` es un endpoint POST que recibe los 5 dígitos ingresados y
    responde si el código es correcto, además de indicar qué posiciones están
    bien (sin revelar cuáles están mal).
- **`Views/Home/Index.cshtml`**: candado central + botones de preguntas +
  modales con las opciones estilo Kahoot (triángulo/rombo/círculo/cuadrado,
  numerados 1 a 4, mismo número que hay que cargar en el candado).
- **`wwwroot/js/site.js`**: maneja la selección de casillero del candado, el
  teclado numérico, abrir/cerrar modales y la llamada AJAX de verificación.

## Para reemplazar las preguntas

Editá la lista `_questions` en `Controllers/HomeController.cs`: cambiá
`Text`, `Options` y `CorrectDigit` (1 a 4) de cada una. No hace falta tocar
nada más.
