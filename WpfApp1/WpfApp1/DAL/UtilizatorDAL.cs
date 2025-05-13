using EasyFoodManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EasyFoodManager.DAL
{
    public static class UtilizatorDAL
    {
        public static Utilizator Login(string email, string parola)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Parola", parola)
            };

            using (var reader = DatabaseHelper.ExecuteReader("LoginUtilizator", parameters))
            {
                if (reader.Read())
                {
                    return new Utilizator
                    {
                        Id = (int)reader["Id"],
                        Nume = reader["Nume"].ToString(),
                        Prenume = reader["Prenume"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefon = reader["Telefon"].ToString(),
                        Adresa = reader["Adresa"].ToString(),
                        Tip = reader["Tip"].ToString()
                    };
                }
            }

            return null;
        }

        public static void Register(Utilizator u)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Nume", u.Nume),
                new SqlParameter("@Prenume", u.Prenume),
                new SqlParameter("@Email", u.Email),
                new SqlParameter("@Telefon", u.Telefon),
                new SqlParameter("@Adresa", u.Adresa),
                new SqlParameter("@Parola", u.Parola),
                new SqlParameter("@Tip", u.Tip) // ex: "Client" sau "Angajat"
            };

            DatabaseHelper.ExecuteNonQuery("RegisterUtilizator", parameters);
        }

        public static Utilizator GetUtilizatorByEmail(string email)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email)
            };

            using (var reader = DatabaseHelper.ExecuteReader("GetUtilizatorByEmail", parameters))
            {
                if (reader.Read())
                {
                    return new Utilizator
                    {
                        Id = (int)reader["Id"],
                        Nume = reader["Nume"].ToString(),
                        Prenume = reader["Prenume"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefon = reader["Telefon"].ToString(),
                        Adresa = reader["Adresa"].ToString(),
                        Tip = reader["Tip"].ToString()
                    };
                }
            }

            return null;
        }
    }
}
