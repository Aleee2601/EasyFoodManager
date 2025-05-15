using EasyFoodManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

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
        public static bool EmailExista(string email)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email)
            };

            using (var reader = DatabaseHelper.ExecuteReader("GetUtilizatorByEmail", parameters))
            {
                return reader.Read(); // dacă există o linie, emailul e deja folosit
            }
        }

        public static bool Register(Utilizator utilizator)
        {
            if (EmailExista(utilizator.Email))
                return false;

            var parametri = new List<SqlParameter>
            {
                new SqlParameter("@Nume", utilizator.Nume),
                new SqlParameter("@Prenume", utilizator.Prenume),
                new SqlParameter("@Email", utilizator.Email),
                new SqlParameter("@Parola", utilizator.Parola),
                new SqlParameter("@Telefon", utilizator.Telefon ?? ""),
                new SqlParameter("@Adresa", utilizator.Adresa ?? ""),
                new SqlParameter("@Tip", utilizator.Tip)
            };

            try
            {
                DatabaseHelper.ExecuteNonQuery("RegisterUtilizator", parametri);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eroare SQL: " + ex.Message);
                throw; // temporar, să vezi în Output fereastra exactul motiv
            }

        }

        public static Utilizator GetUtilizatorById(int id)
        {
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@Id", id)
    };

            using (var reader = DatabaseHelper.ExecuteReader("GetUtilizatorById", parameters))
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
