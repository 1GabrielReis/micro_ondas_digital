using Microondas.API.Model.Dao;
using Microondas.API.Model.DataTransferObject;
using Microondas.API.Model.Entity;

namespace Microondas.API.Model.Service
{
    public class MicroondasService : IMicroondasDao
    {
        public IMicroondasDao? microondasDao = null;
        public MicroondasService()
        {
            this.microondasDao = DaoFactory.CreateMicroondasDao();
        }

        public void Insert(MicroondasEntity microondas)
        {
            try
            {
                if (microondas == null)
                {
                    throw new ServiceException("Objeto microondas não pode ser nulo.");
                }

                if (microondasDao == null)
                {
                    throw new ServiceException("microondasDao não foi inicializado.");
                }

                if (microondas.preDefinido == true)
                {
                    throw new ServiceException("Não é possível inserir uma opção pré-definida.");
                }
                microondasDao.Insert(microondas);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro ao inserir nova opção: " + ex.Message);
            }
        }

        public void Update(MicroondasEntity microondas)
        {
            try
            {
                if (microondasDao == null)
                {
                    throw new ServiceException("microondasDao não foi inicializado.");
                }
                var microondasDb= microondasDao.FindById(microondas.id);
                if (microondasDb == null)
                {
                    throw new ServiceException("Opção não encontrada para atualização.");
                }
                if (microondasDb.preDefinido || microondas.preDefinido)
                {   
                  throw new ServiceException("Não é possível atualizar uma opção pré-definida."); 
                }
            
                microondasDao.Update(microondas);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro ao atualizar opção: " + ex.Message);
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                if (microondasDao == null)
                {
                    throw new ServiceException("microondasDao não foi inicializado.");
                }

                MicroondasEntity microondas = microondasDao.FindById(id);
                if (microondas == null)
                {
                    throw new ServiceException("Opção não encontrada para exclusão.");
                }
                if (microondas.preDefinido == true)
                {
                    throw new ServiceException("Não é possível deletar uma opção pré-definida.");
                }
            

                microondasDao.DeleteById(id);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro ao deletar opção: " + ex.Message);
            }
        }

        public MicroondasEntity FindById(int id)
        {
            try
            {
                if (microondasDao == null)
                {
                    throw new ServiceException("microondasDao não foi inicializado.");
                }

                return microondasDao.FindById(id);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro ao buscar opção por ID: " + ex.Message);
            }
        }

        public List<MicroondasEntity> FindAll()
        {
            try
            {
                if (microondasDao == null)
                {
                    throw new ServiceException("microondasDao não foi inicializado.");
                }

                return microondasDao.FindAll();
            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro ao buscar todos as opção: " + ex.Message);
            }
        }

        public MicroondasEntity InstanciarObjeto(MicroondasDto obj, int? id)
        {
            try
            {
                var microondas = new MicroondasEntity();
                if (id is not null)
                {
                    microondas.id = (int)id;
                }
                microondas.nomePrograma = obj.nomePrograma;
                microondas.alimento = obj.alimento;
                microondas.tempo = obj.tempo;
                microondas.potencia = obj.potencia;
                microondas.instrucoes = obj.instrucoes;
                microondas.preDefinido = obj.preDefinido;
                return microondas;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro ao instanciar objeto MicroondasEntity: " + ex.Message);
            }
        }
    }
}
