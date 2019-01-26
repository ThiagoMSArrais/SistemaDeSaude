using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaDeSaude.presentation.Models
{
    public class VidaDAO : IVidaDAO
    {
        private readonly string connectionString = @"Data Source=LAPTOP-TH4M96NM\SQLEXPRESS;Initial Catalog=SISTEMADESAUDE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void AdicionarVida(VidaViewModel vida)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQLEndereco = "INSERT INTO ENDERECOS (LOGRADOURO, NUMERO, COMPLEMENTO, BAIRRO, MUNICIPIO, " +
                                     "ESTADO, UF, CEP) OUTPUT INSERTED.ENDERECOID VALUES (@LOGRADOURO, @NUMERO, @COMPLEMENTO, @BAIRRO, @MUNICIPIO, " +
                                     "@ESTADO, @UF, @CEP)";

                SqlCommand cmdEndereco = new SqlCommand(SQLEndereco, con);
                cmdEndereco.CommandType = CommandType.Text;
                cmdEndereco.Parameters.AddWithValue("@LOGRADOURO", vida.Endereco.Logradouro);
                cmdEndereco.Parameters.AddWithValue("@NUMERO", vida.Endereco.Numero);
                cmdEndereco.Parameters.AddWithValue("@COMPLEMENTO", vida.Endereco.Complemento);
                cmdEndereco.Parameters.AddWithValue("@BAIRRO", vida.Endereco.Bairro);
                cmdEndereco.Parameters.AddWithValue("@MUNICIPIO", vida.Endereco.Municipio);
                cmdEndereco.Parameters.AddWithValue("@ESTADO", vida.Endereco.Estado);
                cmdEndereco.Parameters.AddWithValue("@UF", vida.Endereco.UF);
                cmdEndereco.Parameters.AddWithValue("@CEP", vida.Endereco.CEP);
                con.Open();
                Guid guidEnderecoId;
                try
                {
                    guidEnderecoId = (Guid)cmdEndereco.ExecuteScalar();
                }
                finally
                {
                    con.Close();
                }


                string SQLAdicionarVida = "INSERT INTO VIDAS (NOME, DATADENASCIMENTO, SEXO, ESTADOCIVIL " +
                                          (vida.Beneficiario ? ")" : ", BENEFICIARIO_ID)") + " OUTPUT INSERTED.VIDAID VALUES (@NOME, @DATADENASCIMENTO, @SEXO, @ESTADOCIVIL " +
                                          (vida.Beneficiario ? ")" : ", @BENEFICIARIO_ID)");

                SqlCommand cmdAdicionarVida = new SqlCommand(SQLAdicionarVida, con);
                cmdAdicionarVida.CommandType = CommandType.Text;
                cmdAdicionarVida.Parameters.AddWithValue("@NOME", vida.Nome);
                cmdAdicionarVida.Parameters.AddWithValue("@DATADENASCIMENTO", vida.DataDeNascimento.ToShortDateString());
                cmdAdicionarVida.Parameters.AddWithValue("@SEXO", vida.Sexo);
                cmdAdicionarVida.Parameters.AddWithValue("@ESTADOCIVIL", vida.EstadoCivil);
                if (!vida.Beneficiario)
                    cmdAdicionarVida.Parameters.AddWithValue("@BENEFICIARIO_ID", vida.BeneficiarioId);
                con.Open();
                Guid guidVida;
                try
                {
                    guidVida = (Guid)cmdAdicionarVida.ExecuteScalar();
                }
                finally
                {
                    con.Close();
                }

                string SQLTelefones = "INSERT INTO TELEFONES (TIPO, DDD, NUMERO, VIDA_ID) " +
                                      "VALUES (@TIPO, @DDD, @NUMERO, @VIDA_ID)";

                SqlCommand cmdAdicionarTelefone = new SqlCommand(SQLTelefones, con);
                cmdAdicionarTelefone.CommandType = CommandType.Text;
                cmdAdicionarTelefone.Parameters.AddWithValue("@TIPO", vida.Telefone.Tipo);
                cmdAdicionarTelefone.Parameters.AddWithValue("@DDD", vida.Telefone.DDD);
                cmdAdicionarTelefone.Parameters.AddWithValue("@NUMERO", vida.Telefone.Numero);
                cmdAdicionarTelefone.Parameters.AddWithValue("@VIDA_ID", guidVida.ToString());
                con.Open();
                try
                { 
                    cmdAdicionarTelefone.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }

            string SQLEnderecoUpdateVida_Id = "UPDATE ENDERECOS SET VIDA_ID = @VIDA_ID WHERE ENDERECOID = @ENDERECOID";

                SqlCommand cmdUpdateEndereco = new SqlCommand(SQLEnderecoUpdateVida_Id, con);
                cmdUpdateEndereco.CommandType = CommandType.Text;
                cmdUpdateEndereco.Parameters.AddWithValue("@VIDA_ID", guidVida);
                cmdUpdateEndereco.Parameters.AddWithValue("@ENDERECOID", guidEnderecoId);
                con.Open();
                try
                {
                    cmdUpdateEndereco.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void AtualizarVida(VidaViewModel vida)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQLAtualizarVida = "UPDATE VIDAS " +
                                          "SET NOME = @NOME, DATADENASCIMENTO = @DATADENASCIMENTO, SEXO = @SEXO, ESTADOCIVIL = @ESTADOCIVIL " +
                                          "WHERE VIDAID = @VIDAID; " +
                                          "UPDATE ENDERECOS " +
                                          "SET LOGRADOURO = @LOGRADOURO, NUMERO = @NUMEROENDERECO , COMPLEMENTO = @COMPLEMENTO, " +
                                          "BAIRRO = @BAIRRO, MUNICIPIO = @MUNICIPIO, ESTADO = @ESTADO, UF = @UF, CEP = @CEP " +
                                          "WHERE ENDERECOID = @ENDERECOID; " +
                                          "UPDATE TELEFONES " +
                                          "SET TIPO = @TIPO, DDD = @DDD, NUMERO = @NUMEROTELEFONE " +
                                          "WHERE TELEFONEID = @TELEFONEID;";

                SqlCommand cmdAtualizarVida = new SqlCommand(SQLAtualizarVida, con);
                cmdAtualizarVida.CommandType = CommandType.Text;
                cmdAtualizarVida.Parameters.AddWithValue("@VIDAID", vida.VidaId);
                cmdAtualizarVida.Parameters.AddWithValue("@NOME", vida.Nome);
                cmdAtualizarVida.Parameters.AddWithValue("@DATADENASCIMENTO", vida.DataDeNascimento.ToShortDateString());
                cmdAtualizarVida.Parameters.AddWithValue("@SEXO", vida.Sexo);
                cmdAtualizarVida.Parameters.AddWithValue("@ESTADOCIVIL", vida.EstadoCivil);
                cmdAtualizarVida.Parameters.AddWithValue("@ENDERECOID", vida.Endereco.EnderecoId);
                cmdAtualizarVida.Parameters.AddWithValue("@LOGRADOURO", vida.Endereco.Logradouro);
                cmdAtualizarVida.Parameters.AddWithValue("@NUMEROENDERECO", vida.Endereco.Numero);
                cmdAtualizarVida.Parameters.AddWithValue("@COMPLEMENTO", vida.Endereco.Complemento);
                cmdAtualizarVida.Parameters.AddWithValue("@BAIRRO", vida.Endereco.Bairro);
                cmdAtualizarVida.Parameters.AddWithValue("@MUNICIPIO", vida.Endereco.Municipio);
                cmdAtualizarVida.Parameters.AddWithValue("@ESTADO", vida.Endereco.Estado);
                cmdAtualizarVida.Parameters.AddWithValue("@UF", vida.Endereco.UF);
                cmdAtualizarVida.Parameters.AddWithValue("@CEP", vida.Endereco.CEP);
                cmdAtualizarVida.Parameters.AddWithValue("@TELEFONEID", vida.Telefone.TelefoneId);
                cmdAtualizarVida.Parameters.AddWithValue("@TIPO", vida.Telefone.Tipo);
                cmdAtualizarVida.Parameters.AddWithValue("@DDD", vida.Telefone.DDD);
                cmdAtualizarVida.Parameters.AddWithValue("@NUMEROTELEFONE", vida.Telefone.Numero);
                con.Open();
                try
                {
                    cmdAtualizarVida.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public List<VidaViewModel> ObterVidas()
        {
            List<VidaViewModel> vidas = new List<VidaViewModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQLObterVidas = "SELECT V.VIDAID, V.NOME, V.DATADENASCIMENTO, V.SEXO, V.ESTADOCIVIL, T.TELEFONEID, T.TIPO, T.DDD, T.NUMERO " +
                                   "FROM VIDAS AS V " +
                                   "INNER JOIN TELEFONES AS T " +
                                   "ON V.VIDAID = T.VIDA_ID " +
                                   "ORDER BY V.NOME";

                SqlCommand cmdObterVidas = new SqlCommand(SQLObterVidas, con);
                cmdObterVidas.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmdObterVidas.ExecuteReader();
                while(rdr.Read())
                {
                    VidaViewModel vidaViewModel = new VidaViewModel();
                    vidaViewModel.VidaId = (Guid)rdr["VIDAID"];
                    vidaViewModel.Nome = rdr["NOME"].ToString();
                    vidaViewModel.DataDeNascimento = (DateTime)rdr["DATADENASCIMENTO"];
                    vidaViewModel.Sexo = rdr["SEXO"].ToString();
                    vidaViewModel.EstadoCivil = rdr["ESTADOCIVIL"].ToString();
                    vidaViewModel.Telefone.TelefoneId = (Guid)rdr["TELEFONEID"];
                    vidaViewModel.Telefone.Tipo = rdr["TIPO"].ToString();
                    vidaViewModel.Telefone.DDD = rdr["DDD"].ToString();
                    vidaViewModel.Telefone.Numero = rdr["NUMERO"].ToString();
                    vidas.Add(vidaViewModel);
                }
                con.Close();
            }
            return vidas;
        }

        public VidaViewModel ObterVidaPorId(Guid id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SqlObterVidaPorId = "SELECT V.VIDAID, V.NOME, V.DATADENASCIMENTO, V.SEXO, V.ESTADOCIVIL, " +
                                           "E.LOGRADOURO, E.NUMERO, E.COMPLEMENTO, E.BAIRRO, E.MUNICIPIO, E.ESTADO, " +
                                           "E.UF, E.CEP, E.ENDERECOID, T.TELEFONEID, T.TIPO, T.DDD, T.NUMERO " +
                                           "FROM VIDAS AS V " +
                                           "INNER JOIN ENDERECOS AS E " +
                                           "ON V.VIDAID = E.VIDA_ID " +
                                           "INNER JOIN TELEFONES AS T " +
                                           "ON V.VIDAID = T.VIDA_ID " +
                                           "WHERE V.VIDAID = @ID";

                SqlCommand cmdObterVidaPorId = new SqlCommand(SqlObterVidaPorId, con);
                cmdObterVidaPorId.CommandType = CommandType.Text;
                cmdObterVidaPorId.Parameters.AddWithValue("@ID", id);
                con.Open();

                SqlDataReader rdr = cmdObterVidaPorId.ExecuteReader();
                VidaViewModel vidaViewModel = null;
                while(rdr.Read())
                {
                    vidaViewModel = new VidaViewModel();
                    vidaViewModel.VidaId = (Guid)rdr["VIDAID"];
                    vidaViewModel.Nome = rdr["NOME"].ToString();
                    vidaViewModel.DataDeNascimento = (DateTime)rdr["DATADENASCIMENTO"];
                    vidaViewModel.Sexo = rdr["SEXO"].ToString();
                    vidaViewModel.EstadoCivil = rdr["ESTADOCIVIL"].ToString();
                    vidaViewModel.Endereco.Logradouro = rdr["LOGRADOURO"].ToString();
                    vidaViewModel.Endereco.Numero = rdr["NUMERO"].ToString();
                    vidaViewModel.Endereco.Complemento = rdr["COMPLEMENTO"].ToString();
                    vidaViewModel.Endereco.Bairro = rdr["BAIRRO"].ToString();
                    vidaViewModel.Endereco.Municipio = rdr["MUNICIPIO"].ToString();
                    vidaViewModel.Endereco.Estado = rdr["ESTADO"].ToString();
                    vidaViewModel.Endereco.UF = rdr["UF"].ToString();
                    vidaViewModel.Endereco.CEP = rdr["CEP"].ToString();
                    vidaViewModel.Endereco.EnderecoId = (Guid)rdr["ENDERECOID"];
                    vidaViewModel.Telefone.TelefoneId = (Guid)rdr["TELEFONEID"];
                    vidaViewModel.Telefone.Tipo = rdr["TIPO"].ToString();
                    vidaViewModel.Telefone.DDD = rdr["DDD"].ToString();
                    vidaViewModel.Telefone.Numero = rdr[17].ToString();
                }
                con.Close();

                return vidaViewModel;
            }
        }

        public void DeletarVida(VidaViewModel vidaViewModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SqlDeletarVida = "DELETE FROM TELEFONES WHERE TELEFONEID = @TELEFONEID; " +
                                        "DELETE FROM ENDERECOS WHERE ENDERECOID = @ENDERECOID; " +
                                        "DELETE FROM VIDAS WHERE VIDAID = @VIDAID;";


                SqlCommand cmdDeletarVida = new SqlCommand(SqlDeletarVida, con);
                cmdDeletarVida.CommandType = CommandType.Text;
                cmdDeletarVida.Parameters.AddWithValue("@VIDAID", vidaViewModel.VidaId);
                cmdDeletarVida.Parameters.AddWithValue("@ENDERECOID", vidaViewModel.Endereco.EnderecoId);
                cmdDeletarVida.Parameters.AddWithValue("@TELEFONEID", vidaViewModel.Telefone.TelefoneId);
                con.Open();
                try
                {
                    cmdDeletarVida.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public List<VidaViewModel> ConsultarPorNome(string nome)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SqlConsultarPorNome = "SELECT V.VIDAID, V.NOME, V.DATADENASCIMENTO, V.SEXO, V.ESTADOCIVIL, T.TELEFONEID, T.TIPO, T.DDD, T.NUMERO " +
                                             "FROM VIDAS AS V " +
                                             "INNER JOIN TELEFONES AS T " +
                                             "ON V.VIDAID = T.VIDA_ID " +
                                             "WHERE V.NOME LIKE @NOME " +
                                             "ORDER BY V.NOME";

                SqlCommand cmdConsultarPorNome = new SqlCommand(SqlConsultarPorNome, con);
                cmdConsultarPorNome.CommandType = CommandType.Text;
                cmdConsultarPorNome.Parameters.AddWithValue("@NOME",nome + "%");
                con.Open();
                SqlDataReader rdr = cmdConsultarPorNome.ExecuteReader();
                List<VidaViewModel> vidas = new List<VidaViewModel>();
                
                while(rdr.Read())
                {
                    VidaViewModel vidaViewModel = new VidaViewModel();
                    vidaViewModel.VidaId = (Guid)rdr["VIDAID"];
                    vidaViewModel.Nome = rdr["NOME"].ToString();
                    vidaViewModel.DataDeNascimento = (DateTime)rdr["DATADENASCIMENTO"];
                    vidaViewModel.Sexo = rdr["SEXO"].ToString();
                    vidaViewModel.EstadoCivil = rdr["ESTADOCIVIL"].ToString();
                    vidaViewModel.Telefone.TelefoneId = (Guid)rdr["TELEFONEID"];
                    vidaViewModel.Telefone.Tipo = rdr["TIPO"].ToString();
                    vidaViewModel.Telefone.DDD = rdr["DDD"].ToString();
                    vidaViewModel.Telefone.Numero = rdr["NUMERO"].ToString();
                    vidas.Add(vidaViewModel);
                }
                con.Close();
                return vidas;
            }
        }

        public void AdicionarBeneficiado(VidaViewModel beneficiado)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQLAdicionarBeneficiario = "INSERT INTO VIDAS (NOME, DATADENASCIMENTO, SEXO, ESTADOCIVIL, RG, CPF_CNPJ, BENEFICIARIO_ID) " +
                                                  "VALUES (@NOME, @DATADENASCIMENTO, @SEXO, @ESTADOCIVIL, @RG, @CPF_CNPJ, @BENEFICIARIO_ID)";

                SqlCommand cmdAdicionarBeneficiario = new SqlCommand(SQLAdicionarBeneficiario, con);
                cmdAdicionarBeneficiario.CommandType = CommandType.Text;
                cmdAdicionarBeneficiario.Parameters.AddWithValue("@NOME", beneficiado.Nome);
                cmdAdicionarBeneficiario.Parameters.AddWithValue("@DATADENASCIMENTO", beneficiado.DataDeNascimento.ToShortDateString());
                cmdAdicionarBeneficiario.Parameters.AddWithValue("@SEXO", beneficiado.Sexo);
                cmdAdicionarBeneficiario.Parameters.AddWithValue("@ESTADOCIVIL", beneficiado.EstadoCivil);
                cmdAdicionarBeneficiario.Parameters.AddWithValue("@RG", beneficiado.RG);
                cmdAdicionarBeneficiario.Parameters.AddWithValue("@CPF_CNPJ", beneficiado.CPF_CNPJ);
                cmdAdicionarBeneficiario.Parameters.AddWithValue("@BENEFICIARIO_ID", beneficiado.BeneficiarioId);
                con.Open();
                try
                {
                    cmdAdicionarBeneficiario.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public List<VidaViewModel> ObterBeneficiados(Guid id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQLObterBeneficiados = "SELECT B.NOME, B.DATADENASCIMENTO, B.SEXO, B.ESTADOCIVIL, B.RG, B.CPF_CNPJ, B.BENEFICIARIO_ID, B.VIDAID " +
                                              "FROM VIDAS AS V " +
                                              "INNER JOIN VIDAS AS B " +
                                              "ON V.VIDAID = @BENEFICIARIO_ID " +
                                              "WHERE B.BENEFICIARIO_ID IS NOT NULL " +
                                              "ORDER BY B.NOME";

                SqlCommand cmdObterBeneficiados = new SqlCommand(SQLObterBeneficiados, con);
                cmdObterBeneficiados.CommandType = CommandType.Text;
                cmdObterBeneficiados.Parameters.AddWithValue("@BENEFICIARIO_ID", id);
                con.Open();
                SqlDataReader rdr = cmdObterBeneficiados.ExecuteReader();
                List<VidaViewModel> Beneficiados = new List<VidaViewModel>();

                while(rdr.Read())
                {
                    VidaViewModel vidaViewModel = new VidaViewModel();
                    vidaViewModel.Nome = rdr["NOME"].ToString();
                    vidaViewModel.DataDeNascimento = (DateTime)rdr["DATADENASCIMENTO"];
                    vidaViewModel.Sexo = rdr["SEXO"].ToString();
                    vidaViewModel.EstadoCivil = rdr["ESTADOCIVIL"].ToString();
                    vidaViewModel.RG = rdr["RG"].ToString();
                    vidaViewModel.CPF_CNPJ = rdr["CPF_CNPJ"].ToString();
                    vidaViewModel.BeneficiarioId = (Guid)rdr["BENEFICIARIO_ID"];
                    vidaViewModel.VidaId = (Guid)rdr["VIDAID"];
                    Beneficiados.Add(vidaViewModel);
                }

                con.Close();

                return Beneficiados;
            }
        }
    }
}