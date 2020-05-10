using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadOefening
{
    class DbInsertion
    {
        public async static Task AddDbLog(int monkeyID, int woodID, string message)
        {
            string connectionString = @"Data Source=LAPTOP-353R5D9A\SQLEXPRESS;Initial Catalog=boeki;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Logs (monkeyID, woodID, message) VALUES ('{monkeyID}', '{woodID}', '{message}');";
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    await cmd.ExecuteScalarAsync();//
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Er is een probleem opgetreden.");
                    Console.WriteLine("Fout: " + e.Message);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            await TxtLog.WriteText($"forestLog{woodID}.txt", "written the following message to database: " + message);
        }

        public async static Task AddMonkeyRecord(int monkeyID, int woodID, int seqNr, int treeID, int x, int y)
        {
            string connectionString = @"Data Source=LAPTOP-353R5D9A\SQLEXPRESS;Initial Catalog=boeki;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO MonkeyRecords (monkeyID, woodID, seqnr ,treeID, x, y) " +
                    $"VALUES ('{monkeyID}', '{woodID}', '{seqNr}', '{treeID}', '{x}', '{y}');";
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    await cmd.ExecuteScalarAsync();//

                }
                catch (Exception e)
                {
                    Console.WriteLine("Er is een probleem opgetreden.");
                    Console.WriteLine("Fout: " + e.Message);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public async static Task AddWoodRecord(int woodID, int treeID, int x, int y)
        {
            string connectionString = @"Data Source=LAPTOP-353R5D9A\SQLEXPRESS;Initial Catalog=boeki;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO WoodRecords (woodID, treeID, x, y) " +
                    $"VALUES ('{woodID}', '{treeID}', '{x}', '{y}');";
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    await cmd.ExecuteScalarAsync();//

                }
                catch (Exception e)
                {
                    Console.WriteLine("Er is een probleem opgetreden.");
                    Console.WriteLine("Fout: " + e.Message);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
    }
}
