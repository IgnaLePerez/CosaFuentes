namespace EscapeRoomCuento.Models
{
    public class CheckCodeRequest
    {
        public List<int> Code { get; set; } = new();
    }

    public class CheckCodeResponse
    {
        public bool Success { get; set; }

        /// <summary>
        /// Marca por posición si ese dígito es correcto, para dar una pequeña pista
        /// visual sin revelar el resto del código.
        /// </summary>
        public List<bool> CorrectPositions { get; set; } = new();
    }
}
