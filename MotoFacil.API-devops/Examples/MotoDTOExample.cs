
using MotoFacil.API.DTOs.Moto;
using Swashbuckle.AspNetCore.Filters;

namespace MotoFacil.API.Examples
{
    public class MotoDTOExample : IExamplesProvider<MotoDTO>
    {
        public MotoDTO GetExamples()
        {
            return new MotoDTO
            {
                Modelo = "Mottu Sport",
                Placa = "ABC99E",
                Chassi = "9C2JC4110JR000001",
                ProblemaIdentificado = "Motor com ruído excessivo",
                
            };
        }
    }
}