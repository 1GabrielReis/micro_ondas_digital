using Microondas.API.DataBase;
using Microondas.API.Model.Dao;
using Microondas.API.Model.Entity;
using Microsoft.Data.SqlClient;

namespace Microondas.API.Model.Implementation
{
    public class MicroondasMDSC : IMicroondasDao
    {
        private SqlConnection? conn = null;

        public MicroondasMDSC(SqlConnection conn)
        {
            this.conn = conn;
        }

        public void Insert(MicroondasEntity microondasEntity)
        {
            try
            {
                string sql = @"INSERT INTO opcoes_pre_definidas 
                           (nome_programa, alimento, tempo, potencia, instrucoes, pre_definido)
                           OUTPUT INSERTED.id
                           VALUES (@nome_programa, @alimento, @tempo, @potencia, @instrucoes, @pre_definido)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome_programa", microondasEntity.nomePrograma);
                    cmd.Parameters.AddWithValue("@alimento", microondasEntity.alimento);
                    cmd.Parameters.AddWithValue("@tempo", microondasEntity.tempo);
                    cmd.Parameters.AddWithValue("@potencia", microondasEntity.potencia);
                    cmd.Parameters.AddWithValue("@instrucoes", microondasEntity.instrucoes);
                    cmd.Parameters.AddWithValue("@pre_definido", microondasEntity.preDefinido);

                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        throw new DbException("Falha ao inserir o registro.");
                    }
                    microondasEntity.id = (int)result;
                }
            }
            catch (SqlException ex)
            {
                throw new DbException(ex.Message);
            }
            finally
            {
                Db.CloseConnection();
            }
        }

        public void Update(MicroondasEntity microondasEntity)
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); }
                string sql = @"UPDATE opcoes_pre_definidas 
                           SET nome_programa = @nome_programa,
                               alimento = @alimento,
                               tempo = @tempo,
                               potencia = @potencia,
                               instrucoes = @instrucoes,
                               pre_definido = @pre_definido
                           WHERE id = @id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome_programa", microondasEntity.nomePrograma);
                    cmd.Parameters.AddWithValue("@alimento", microondasEntity.alimento);
                    cmd.Parameters.AddWithValue("@tempo", microondasEntity.tempo);
                    cmd.Parameters.AddWithValue("@potencia", microondasEntity.potencia);
                    cmd.Parameters.AddWithValue("@instrucoes", microondasEntity.instrucoes);
                    cmd.Parameters.AddWithValue("@pre_definido", microondasEntity.preDefinido);
                    cmd.Parameters.AddWithValue("@id", microondasEntity.id);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new DbException("No record found with the specified ID.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DbException(ex.Message);
            }
            finally
            {
                Db.CloseConnection();
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); }
                string sql = "DELETE FROM opcoes_pre_definidas WHERE id = @id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        throw new DbException("No record found with the specified ID.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DbException(ex.Message);
            }
            finally
            {
                Db.CloseConnection();
            }
        }

        public MicroondasEntity FindById(int id)
        {
            SqlDataReader? reader = null;
            try
            {
                string sql = "SELECT id, nome_programa, alimento, tempo, potencia, instrucoes, pre_definido FROM opcoes_pre_definidas WHERE id = @id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return _InstantiateMicroondas(reader);
                    }
                    else
                    {
                        return null!;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DbException(ex.Message);
            }
            finally
            {
                Db.CloseConnection();
                Db.closeReader(reader);
            }
        }

        public List<MicroondasEntity> FindAll()
        {
            var lista = new List<MicroondasEntity>();
            SqlDataReader reader = null;

            try
            {
                string sql = "SELECT id, nome_programa, alimento, tempo, potencia, instrucoes, pre_definido FROM opcoes_pre_definidas";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var entity = _InstantiateMicroondas(reader);
                        lista.Add(entity);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DbException(ex.Message);
            }
            finally
            {
                Db.CloseConnection();
                Db.closeReader(reader);
            }
            return lista;
        }

        private MicroondasEntity _InstantiateMicroondas(SqlDataReader reader)
        {
            try
            {
                return new MicroondasEntity(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetInt32(3),
                    reader.GetInt32(4),
                    reader.GetString(5),
                    reader.GetBoolean(6)
                );
            }
            catch (Exception ex)
            {
                throw new DbException(ex.Message);
            }
        }
    }
}
