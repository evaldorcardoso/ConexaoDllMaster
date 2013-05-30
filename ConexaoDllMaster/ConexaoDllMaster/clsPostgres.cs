using System;
using System.Data;
using Npgsql;
using System.Windows.Forms;
using ConexaoDllMaster;



namespace ConexaoDllMaster
{
    
    public class clsPostgres
    {
        static NpgsqlDataAdapter da;
        static DataTable dt = new DataTable();
        
        //static NpgsqlConnection _conn=new NpgsqlConnection();
        #region Atributos

        private static string _nomeBanco = "matriculasDb";
        private static string _nomeServidor = "localhost";
        private static string _senha = "99080035";
        private static string _usuario = string.Empty;
        private static NpgsqlConnection _conn = new NpgsqlConnection();

        #endregion

        #region Propriedades

        private static string connString
        {
            get
            {
                return "Server=localhost;" +
                    "Port=5432;" +
                    "UserId=postgres;" +
                    "Password='99080035';" +
                    "Database=matriculasDb";

            }
        }
         
        #endregion

        #region Construtores
            
            public clsPostgres()
            { }
        /// <summary>
        /// Constroi um objeto para conectar ao postgresql
        /// </summary>
        /// <param name="pNomeServidor">Passar o nome do Servidor</param>
        /// <param name="pNomeBanco">Passar o nome do banco</param>
        /// <param name="pSenha">Passar a senha do usuário</param>
            public clsPostgres(string pNomeServidor,
                                string pNomeBanco,
                                string pSenha)
            {
                _nomeServidor = pNomeServidor;
                _nomeBanco = pNomeBanco;
                _senha = pSenha;

            }

        #endregion

        #region Métodos
        /// <summary>
        /// Método para conectar com o banco de dados postgresql
        /// Passe os parâmettros corretos para o construtor
        /// </summary>
            public static void conectar()
            {
                try
                {
                    _conn.ConnectionString = connString;
                    _conn.Open();
                }
                catch (NpgsqlException ex)
                {
                    throw ex;
                }
                
            }
            public static void desconectar()
            {
                _conn.Close();
            }

        #endregion

        #region Métodos Staticos
            

            public static DataTable getDataTable(string pSql)
            {
                try
                {
                    conectar();
                }
                catch (Exception)
                {
                    //throw;
                }
                da = new NpgsqlDataAdapter(pSql, _conn);
                if (dt != null)
                    dt = new DataTable();
                da.Fill(dt);
                desconectar();
                return dt;
            }
            
            public static IDataAdapter setDataAdapter(string SQL)
            {
                
                da = new NpgsqlDataAdapter(SQL, _conn);
                try
                {
                    da.Fill(dt);
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show(MessageBoxIcon.Error + "Não foi possível inserir!\n\n" + Convert.ToString(ex));
                }
                
                return da;
            }
            public static NpgsqlDataReader getDataReader()
            {
                return null;
            }

            public static int SqlCommand(String pComando)
            {
                NpgsqlCommand cmd = new NpgsqlCommand();
                int max;
                conectar();
                cmd.Connection = _conn;
                cmd.CommandText = pComando;
                cmd.ExecuteNonQuery();
                max = (int)cmd.ExecuteScalar();
                desconectar();
                return max;
            }

        #endregion
    }
}
