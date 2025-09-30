namespace MotoFacil.API.DTOs.Moto
{
    public class MotoDTO
    {
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Chassi { get; set; } = string.Empty;
        public string? ProblemaIdentificado { get; set; }
    }
}