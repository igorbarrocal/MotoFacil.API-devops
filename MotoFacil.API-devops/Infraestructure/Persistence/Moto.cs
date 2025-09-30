namespace MotoFacil.API.Infrastructure.Persistence
{
    public class Moto
    {
        public long Id { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Chassi { get; set; } = string.Empty;
        public string? ProblemaIdentificado { get; set; }
    }
}