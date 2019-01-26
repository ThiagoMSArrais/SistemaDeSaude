using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaDeSaude.presentation.Models
{
    public class ContratoDAO : IContratoDAO
    {
        private readonly string connectionString = @"Data Source=LAPTOP-TH4M96NM\SQLEXPRESS;Initial Catalog=SISTEMADESAUDE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void AdicionarContrato(ContratoViewModel contrato)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQLAdicionarContrato = "INSERT INTO CONTRATOS (PLANO_ID, DATACADASTRO, VALIDADE, CONTRATOEXPIRADO, VIDA_ID) " +
                                              "VALUES (@PLANO_ID, @DATACADASTRO, @VALIDADE, @CONTRATOEXPIRADO, @VIDA_ID)";

                SqlCommand cmdAdicionarContrato = new SqlCommand(SQLAdicionarContrato, con)
                {
                    CommandType = CommandType.Text
                };
                cmdAdicionarContrato.Parameters.AddWithValue("@PLANO_ID", contrato.Plano.PlanoId);
                cmdAdicionarContrato.Parameters.AddWithValue("@DATACADASTRO", contrato.DataCadastro);
                cmdAdicionarContrato.Parameters.AddWithValue("@VALIDADE", contrato.Validade);
                cmdAdicionarContrato.Parameters.AddWithValue("@CONTRATOEXPIRADO", contrato.ContratoExpirado);
                cmdAdicionarContrato.Parameters.AddWithValue("@VIDA_ID", contrato.VidaId);
                con.Open();
                cmdAdicionarContrato.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<ContratoViewModel> ObterContratos()
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> ObterNomeDoPlano(Guid id_prestadora)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQLObterNomeDoPlano = "SELECT PL.PLANOID, PL.NOME " +
                                             "FROM PLANOS AS PL " +
                                             "WHERE PL.PRESTADORA_ID = @PRESTADORAID";

                SqlCommand cmdObterNomeDoPlano = new SqlCommand(SQLObterNomeDoPlano, con)
                {
                    CommandType = CommandType.Text
                };
                cmdObterNomeDoPlano.Parameters.AddWithValue("@PRESTADORAID", id_prestadora);
                con.Open();
                SqlDataReader rdr = cmdObterNomeDoPlano.ExecuteReader();

                List<SelectListItem> Planos = new List<SelectListItem>();
                while(rdr.Read())
                {
                    Planos.Add(new SelectListItem { Text = rdr["NOME"].ToString(), Value = rdr["PLANOID"].ToString() });
                }

                con.Close();

                return Planos;
            }
        }

        public List<SelectListItem> ObterPrestadora()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQLObterPrestadora = "SELECT PRESTADORAID, NOME FROM PRESTADORAS " +
                                            "ORDER BY NOME";

                SqlCommand cmdObterPrestadora = new SqlCommand(SQLObterPrestadora, con)
                {
                    CommandType = CommandType.Text
                };
                con.Open();
                SqlDataReader rdr = cmdObterPrestadora.ExecuteReader();

                List<SelectListItem> Prestadoras = new List<SelectListItem>();

                while (rdr.Read())
                {
                    Prestadoras.Add(new SelectListItem { Text = rdr["NOME"].ToString(), Value = rdr["PRESTADORAID"].ToString() });
                }

                con.Close();

                return Prestadoras;
            }
        }

        public List<SelectListItem> ObterVidas()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQLObterVidas = "SELECT VIDAID, NOME FROM VIDAS " +
                                       "WHERE BENEFICIARIO_ID IS NULL";

                SqlCommand cmdObterVidas = new SqlCommand(SQLObterVidas, con)
                {
                    CommandType = CommandType.Text
                };
                con.Open();
                SqlDataReader rdr = cmdObterVidas.ExecuteReader();

                List<SelectListItem> ListaVidas = new List<SelectListItem>();

                while(rdr.Read())
                {
                    ListaVidas.Add(new SelectListItem { Text = rdr["NOME"].ToString(), Value = rdr["VIDAID"].ToString() });
                }

                con.Close();

                return ListaVidas;
            }
        }
    }
}