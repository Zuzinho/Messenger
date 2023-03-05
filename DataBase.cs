using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Messanger.Models;
using Microsoft.Data.SqlClient;

namespace Messanger
{
    public class DataBase
    {
        private static string connection = "data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=|DataDirectory|\\Database.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        private static Message Message(SqlDataReader reader) {
            int id = (int)reader.GetValue(0);
            string text = (string)reader.GetValue(1);
            DateTime time = (DateTime)reader.GetValue(2);
            int userSend = (int)reader.GetValue(3);
            int userTake = (int)reader.GetValue(4);
            return new Message(id, text, time, userSend, userTake);
        }
        public static void CreateTable(Room room) {
            int? user1 = room.user1_id, user2 = room.user2_id;
            string name = "RoomContent" + user1 + "_" + user2;
            string sql = "if OBJECT_ID('" + name + "') is null CREATE TABLE " + name + "(id INT IDENTITY(1,1) NOT NULL,text NVARCHAR(MAX),time DATETIME,userSend INT,userTake INT,PRIMARY KEY (id));";
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public static void AddMessage(Room room, string message,DateTime time,int userSend,int userTake) {
            int? user1 = room.user1_id, user2 = room.user2_id;
            string name = "RoomContent" + user1 + "_" + user2;
            string sql = "SET IDENTITY_INSERT " +name +" OFF; INSERT INTO " + name + " (text,time,userSend,userTake) VALUES ('" + message + "',CONVERT(datetime, '" + time + "', 103)," + userSend + "," + userTake + ");";
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public static List<Message> GetMessages(Room room) {
            int? user1 = room.user1_id, user2 = room.user2_id;
            string name = "RoomContent" + user1 + "_" + user2;
            string sql = "SELECT * FROM " + name;
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<Message> list = new List<Message>();
            while (reader.Read())
            {
                list.Add(Message(reader));
            }
            con.Close();
            return list;
        }
        public static Message GetLastMessage(Room room)
        {
            int? user1 = room.user1_id, user2 = room.user2_id;
            string name = "RoomContent" + user1 + "_" + user2;
            string sql = "SELECT * FROM "+name+" WHERE id=(SELECT MAX(id) FROM "+name+");";
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            if(reader.Read()) return Message(reader);
            return null;
        }
        public static void CreateTypeMessage()
        {
            string sql = "CREATE TYPE Message AS (id INT NOT NULL,text NVARCHAR(MAX),time DATETIME,userSend User,userTake User,PRIMARY KEY (id));";
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();
        }
        public static void CreateTypeUser()
        {
            string sql = "CREATE TYPE User AS (Id iNT NOT NULL,Name NVARCHAR(MAX),avatar NVARCHAR(MAX),RoomNumvers NVARCHAR(MAX),PRIMARY KEY (Id));";
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();

        }
    }
}