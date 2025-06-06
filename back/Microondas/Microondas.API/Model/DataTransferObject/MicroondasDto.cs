namespace Microondas.API.Model.DataTransferObject
{
    public class MicroondasDto
    {
        public string nomePrograma { get; set; }
        public string alimento { get; set; }
        public int tempo { get; set; }
        public int potencia { get; set; }
        public string instrucoes { get; set; } 
        public bool preDefinido { get; set; } 

        public MicroondasDto(){ }

        public MicroondasDto(string nomePrograma, string alimento, int tempo, int potencia, string instrucoes, bool preDefinido )
        {
            this.nomePrograma = nomePrograma;
            this.alimento = alimento;
            this.tempo = tempo;
            this.potencia = potencia;
            this.instrucoes = instrucoes;
            this.preDefinido = preDefinido;
        }
    }
}
