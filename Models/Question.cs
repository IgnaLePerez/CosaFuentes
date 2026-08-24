namespace EscapeRoomCuento.Models
{
    /// <summary>
    /// Representa una pregunta completa, incluyendo el dígito correcto (1-4).
    /// Esta clase vive solo en el servidor: el dígito correcto nunca se serializa
    /// hacia la vista ni hacia el cliente.
    /// </summary>
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        /// <summary>Las 4 opciones, en el mismo orden en que se muestran (1 a 4).</summary>
        public List<string> Options { get; set; } = new();

        /// <summary>Dígito correcto (1 a 4) que el jugador debe cargar en el candado.</summary>
        public int CorrectDigit { get; set; }
    }
}
