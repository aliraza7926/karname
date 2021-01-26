using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp3
{

    class Database
    {
        private static SQLiteConnection DBpath = new SQLiteConnection(@"Data Source=" + Application.StartupPath + "\\Database.db");
        public static void Add_New_Student_Sanjesh(string FirstName, string LastName, string PersonID, string BirthYear, string Grade)
        {
            string CommandstrAddStudnetInformation = "INSERT into [Student_Information_Sanjesh] (first_name,last_name,person_id,birth_year,grade)" +
                 "VALUES (@FirstName,@LastName,@PersonID,@BirthYear,@Grade)";

            SQLiteCommand AddStudnetInformation = new SQLiteCommand(CommandstrAddStudnetInformation, DBpath);

            AddStudnetInformation.Parameters.AddWithValue("@FirstName", FirstName);
            AddStudnetInformation.Parameters.AddWithValue("@LastName", LastName);
            AddStudnetInformation.Parameters.AddWithValue("@PersonID", PersonID);
            AddStudnetInformation.Parameters.AddWithValue("@BirthYear", BirthYear);
            AddStudnetInformation.Parameters.AddWithValue("@Grade", Grade);


            DBpath.Open();
            AddStudnetInformation.ExecuteNonQuery();
            DBpath.Close();
        }
        public static void Add_New_Student_Kanoon(string FirstName, string LastName, string PersonID, string Counter, string Grade)
        {
            string CommandstrAddStudnetInformation = "INSERT into [Student_Information_Kanoon] (first_name,last_name,person_id,counter,grade)" +
                 "VALUES (@FirstName,@LastName,@PersonID,@Counter,@Grade)";

            SQLiteCommand AddStudnetInformation = new SQLiteCommand(CommandstrAddStudnetInformation, DBpath);

            AddStudnetInformation.Parameters.AddWithValue("@FirstName", FirstName);
            AddStudnetInformation.Parameters.AddWithValue("@LastName", LastName);
            AddStudnetInformation.Parameters.AddWithValue("@PersonID", PersonID);
            AddStudnetInformation.Parameters.AddWithValue("@Counter", Counter);
            AddStudnetInformation.Parameters.AddWithValue("@Grade", Grade);


            DBpath.Open();
            AddStudnetInformation.ExecuteNonQuery();
            DBpath.Close();
        }


    }
}
