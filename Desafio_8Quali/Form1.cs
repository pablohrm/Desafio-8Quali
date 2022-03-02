using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Desafio_8Quali
{
    public partial class Form1 : Form
    {
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        
        string strSQL;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server = localhost; Database = desafio; Uid = root; Pwd = 99798950;");

                if (txtNome.Text != "")
                {
                    strSQL = "INSERT INTO CADASTRO_DESAFIO (NOME, EMPRESA, EMAIL, TELEFONE_PESSOAL, TELEFONE_COMERCIAL) VALUES (@NOME, @EMPRESA, @EMAIL, @TELEFONE_PESSOAL, @TELEFONE_COMERCIAL)";

                    comando = new MySqlCommand(strSQL, conexao);
                    comando.Parameters.AddWithValue("@NOME", txtNome.Text);
                    comando.Parameters.AddWithValue("@EMPRESA", txtEmpresa.Text);
                    comando.Parameters.AddWithValue("@EMAIL", txtEmail.Text);
                    comando.Parameters.AddWithValue("@TELEFONE_PESSOAL", txtTelefone1.Text);
                    comando.Parameters.AddWithValue("@TELEFONE_COMERCIAL", txtTelefone2.Text);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastrado com Sucesso!");
                }
                else
                {
                    MessageBox.Show("Insira o Nome");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }

            txtNome.Clear();
            txtEmpresa.Clear();
            txtEmail.Clear();
            txtTelefone1.Clear();
            txtTelefone2.Clear();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server = localhost; Database = desafio; Uid = root; Pwd = 99798950;");

                strSQL = "UPDATE CADASTRO_DESAFIO SET NOME = @NOME, EMPRESA = @EMPRESA, EMAIL = @EMAIL, TELEFONE_PESSOAL = @TELEFONE_PESSOAL, TELEFONE_COMERCIAL = @TELEFONE_COMERCIAL WHERE ID = @ID";
                comando = new MySqlCommand(strSQL, conexao);
                comando.Parameters.AddWithValue("@ID", textBox1.Text);
                comando.Parameters.AddWithValue("@NOME", txtNome.Text);
                comando.Parameters.AddWithValue("@EMPRESA", txtEmpresa.Text);
                comando.Parameters.AddWithValue("@EMAIL", txtEmail.Text);
                comando.Parameters.AddWithValue("@TELEFONE_PESSOAL", txtTelefone1.Text);
                comando.Parameters.AddWithValue("@TELEFONE_COMERCIAL", txtTelefone2.Text);

                conexao.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Atualizado com Sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }

            txtNome.Clear();
            txtEmpresa.Clear();
            txtEmail.Clear();
            txtTelefone1.Clear();
            txtTelefone2.Clear();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server = localhost; Database = desafio; Uid = root; Pwd = 99798950;");

                if (textBox1.Text != "")
                {
                    if (MessageBox.Show("Tem certeza que deseja excluir este cadastro?", "Excluir Cadastro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        strSQL = "DELETE FROM CADASTRO_DESAFIO WHERE ID = @ID";
                        comando = new MySqlCommand(strSQL, conexao);
                        comando.Parameters.AddWithValue("@ID", textBox1.Text);

                        conexao.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Deletado com Sucesso!");
                    }
                }
                else
                {
                    MessageBox.Show("Informe o ID que deseja excluir.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }

            txtNome.Clear();
            txtEmpresa.Clear();
            txtEmail.Clear();
            txtTelefone1.Clear();
            txtTelefone2.Clear();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {                
                conexao = new MySqlConnection("Server = localhost; Database = desafio; Uid = root; Pwd = 99798950;");
                strSQL = "SELECT * FROM cadastro_desafio WHERE id = @ID OR nome = @NOME OR empresa = @EMPRESA OR email = @EMAIL OR telefone_pessoal = @TELEFONE_PESSOAL OR telefone_comercial = @TELEFONE_COMERCIAL";
                comando = new MySqlCommand(strSQL, conexao);
                
                comando.Parameters.AddWithValue("@ID", textBox1.Text);
                comando.Parameters.AddWithValue("@NOME", txtNome.Text);
                comando.Parameters.AddWithValue("@EMPRESA", txtEmpresa.Text);
                comando.Parameters.AddWithValue("@EMAIL", txtEmail.Text);
                comando.Parameters.AddWithValue("@TELEFONE_PESSOAL", txtTelefone1.Text);
                comando.Parameters.AddWithValue("@TELEFONE_COMERCIAL", txtTelefone2.Text);

                conexao.Open();
                dr = comando.ExecuteReader();

                while (dr.Read())
                {

                    textBox1.Text = Convert.ToString(dr["Id"]);
                    txtNome.Text = Convert.ToString(dr["Nome"]);
                    txtEmpresa.Text = Convert.ToString(dr["Empresa"]);
                    txtEmail.Text = Convert.ToString(dr["Email"]);
                }
               
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
            conexao = new MySqlConnection("Server = localhost; Database = desafio; Uid = root; Pwd = 99798950;");
            strSQL = "SELECT * FROM cadastro_desafio WHERE id = '" + textBox1.Text + "' OR nome = '" + txtNome.Text + "' OR empresa = '" + txtEmpresa.Text + "' OR email = '" + txtEmail.Text + "' OR telefone_pessoal = '" + txtTelefone1.Text + "' OR telefone_comercial = '" + txtTelefone2.Text + "'";
            da = new MySqlDataAdapter(strSQL, conexao);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvDados.DataSource = dt;
            
        }

        //Listar
        private void btnExibir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server = localhost; Database = desafio; Uid = root; Pwd = 99798950;");

                strSQL = "SELECT * FROM CADASTRO_DESAFIO";

                da = new MySqlDataAdapter(strSQL, conexao);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDados.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            txtNome.Clear();
            txtEmpresa.Clear();
            txtEmail.Clear();
            txtTelefone1.Clear();
            txtTelefone2.Clear();
        }

        private void dgvDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow linha = dgvDados.Rows[e.RowIndex];
            textBox1.Text = linha.Cells[0].Value.ToString();
            txtNome.Text = linha.Cells[1].Value.ToString();
            txtEmpresa.Text = linha.Cells[2].Value.ToString();
            txtEmail.Text = linha.Cells[3].Value.ToString();
            txtTelefone1.Text = linha.Cells[4].Value.ToString();
            txtTelefone2.Text = linha.Cells[5].Value.ToString();
        }
    }
}
