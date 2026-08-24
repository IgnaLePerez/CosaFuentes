namespace EscapeRoomCuento.Models
{
    /// <summary>
    /// Versión "segura" de la pregunta que sí viaja a la vista/cliente.
    /// No incluye CorrectDigit.
    /// </summary>
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
    }
}
