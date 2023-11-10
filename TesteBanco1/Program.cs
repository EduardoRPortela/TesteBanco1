using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBanco1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().testaExecuteReader();
            Console.WriteLine("\n\nFIM. Pressione qualquer tecla...");
            Console.ReadKey();
        }

        public void testaConexao()
        {
            try
            {
                String conString = @"Data Source=(LocalDB)\MSSQLLocalDB; 
                                AttachDbFilename=|DataDirectory|\APP_DATA\Database1.mdf; 
                                Integrated Security = True";
                SqlConnection connection = new SqlConnection(conString);
                connection.Open();
                Console.WriteLine("\nConexão aberta com sucesso!\n");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"\nOcorreu uma exceção : {ex.Message}\n");
            }
        }
        public void testaExecuteReader()//vê todos os professores
        {

            SqlConnection con = null;
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                con.Open();

                SqlCommand cm = new SqlCommand("select * from professor", con);
                SqlDataReader sDR = cm.ExecuteReader();
                while (sDR.Read())
                {
                    Console.WriteLine(sDR["idprofessor"] + ") " + sDR["matricula"] + " - " + sDR["nome"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {    
                con.Close();
            }
        }
        public void testaExecuteScalar()//Conta quantos professores tem cadastrados
        {
            SqlConnection con = null;
            try
            {
              
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                con.Open();

                SqlCommand cm = new SqlCommand("SELECT COUNT(idprofessor) FROM professor", con);
                int totalProfs = (int)cm.ExecuteScalar();
                Console.WriteLine("Número de professores cadastrados :  " + totalProfs);
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {  
                con.Close();
            }
        }
        public void testaExecuteNonQuery()//Insere, remove e atualiza dados no banco de dados, além de contar quantos dados foram modificados
        {
            SqlConnection con = null;
            try
            {
                
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                con.Open();
                
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Professor (matricula,nome) 
                                                VALUES('1011','Biazzi')", con);
                int linhasAfetadas = cmd.ExecuteNonQuery();
                Console.WriteLine("Linhas INSERIDAS = " + linhasAfetadas);
                cmd.CommandText = "UPDATE Professor set nome = 'Carla' WHERE matricula = '1011'";
                linhasAfetadas = cmd.ExecuteNonQuery();
                Console.WriteLine("Linhas ATUALIZADAS = " + linhasAfetadas);
                cmd.CommandText = "DELETE FROM Professor WHERE matricula='1011'";
                linhasAfetadas = cmd.ExecuteNonQuery();
                Console.WriteLine("Linhas DELETADAS = " + linhasAfetadas);
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {  
                con.Close();
            }
        }

      /*  public void AdicionarProfessor()
        {
            SqlConnection con = null;
            try { 
            string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(ConString);
            con.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Professor (matricula,nome) 
                                                VALUES('1213','Helo')", con);
                cmd.CommandText = "INSERT INTO Professor (matricula,nome) VALUES ('1213', 'Helo')";
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {
                con.Close();
            }
        }*/
    }
}
