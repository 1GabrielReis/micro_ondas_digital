using System.ComponentModel.DataAnnotations;

namespace Microondas.API.Model.Entity
{
    public class MicroondasEntity
    {
        [Key]
        public int id { get; set; }
        public string nomePrograma { get; set; }
        public string alimento { get; set; }
        public int tempo { get; set; }
        public int potencia { get; set; }
        public string instrucoes { get; set; }
        public bool preDefinido { get; set; }

        public MicroondasEntity()
        {
            // Construtor vazio ou inicializações, se necessário
        }

        public MicroondasEntity(int id, string nomePrograma, string alimento, int tempo, int potencia, string instrucoes, bool preDefinido)
        {
            this.id = id; 
            this.nomePrograma = nomePrograma;
            this.alimento = alimento;
            this.tempo = tempo;
            this.potencia = potencia;
            this.instrucoes = instrucoes;
            this.preDefinido = preDefinido;
        }
    }
}
