using Dapper;
using System;
using System.Data.SqlClient;
namespace MRO.ROI.Automation.Utility
{
    public class MRODBConnection
    {

        public static void SampleDBConnection()
        {
            using (SqlConnection db = new SqlConnection("uid=webclient_MRO;pwd=icc48*PLX;server=HQSQLSTG06L;database=MRO_Facilities"))//Connectionsrting)
            {
                string sSQL = "select * from tblFacilities where nfacilityid = 1";

                var result = db.Query(sSQL);
                Console.WriteLine("DB Results" + result);
            }
        }

        public static void GetQueryResult()
        {
            SqlConnection conn = new SqlConnection(
               new SqlConnectionStringBuilder()
               {
                   DataSource = "hqsqlstg06a",
                   InitialCatalog = "MRO_Facillities",
                   UserID = "MRO\tmirza",
                   Password = "Mihran2007$"
               }.ConnectionString
         );
            Console.WriteLine("Error in getting result of query.");
            conn.Open();

            conn.Close();
            conn.Dispose();
        }
    }
}
