using Microsoft.Data.SqlClient;
using Roommates.Models;
using System;
using System.Collections.Generic;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public List<Roommate> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Firstname, Lastname, RentPortion, MoveInDate, RoomId FROM Roommate";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Roommate> roommates = new List<Roommate>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("Firstname");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("Lastname");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int rentPortionColumnPosition = reader.GetOrdinal("RentPortion");
                        int rentPortionValue = reader.GetInt32(rentPortionColumnPosition);

                        int moveInDateColumnPosition = reader.GetOrdinal("MoveInDate");
                        DateTime moveInDateValue = reader.GetDateTime(moveInDateColumnPosition);

                        Roommate roommate = new Roommate
                        {
                            Id = idValue,
                            Firstname = firstNameValue,
                            Lastname = lastNameValue,
                            RentPortion = rentPortionValue,
                            MoveInDate = moveInDateValue,
                            Room = null
                        };

                        roommates.Add(roommate);
                    }

                    reader.Close();

                    return roommates;
                }
            }

        }

        public List<Roommate> GetAllWithRoom(int roomId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT rm.Id, rm.FirstName, rm.LastName, rm.RentPortion, rm.MoveInDate, r.Name AS RoomName, r.MaxOccupancy FROM Roommate rm JOIN Room r ON rm.RoomId = r.id WHERE r.id = @id";
                    cmd.Parameters.AddWithValue("@id", roomId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Roommate> roommatesWithRoom = new List<Roommate>();

                    Roommate roommate = null;

                    while (reader.Read())
                    {
                    
                        roommate = new Roommate
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Firstname = reader.GetString(reader.GetOrdinal("Firstname")),
                            Lastname = reader.GetString(reader.GetOrdinal("Lastname")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            MoveInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate")),
                            Room = new Room()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("RoomName")),
                                MaxOccupancy = reader.GetInt32(reader.GetOrdinal("MaxOccupancy"))
                            }
                        };

                        roommatesWithRoom.Add(roommate);
                    }
                    reader.Close();

                    return roommatesWithRoom;
                }
            }
        }
        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Firstname, Lastname, RentPortion, MoveInDate, RoomId FROM Roommate WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;

                    if (reader.Read())
                    {
                        roommate = new Roommate
                        {
                            Id = id,
                            Firstname = reader.GetString(reader.GetOrdinal("Firstname")),
                            Lastname = reader.GetString(reader.GetOrdinal("Lastname")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            MoveInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate")),
                            Room = null
                        };
                    }

                    reader.Close();

                    return roommate;
                }
            }
        }

        public void Insert(Roommate roommate)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Roommate (Firstname, Lastname, RentPortion, MoveInDate, RoomId)
                                           OUTPUT INSERTED.Id
                                            VALUES (@Firstname, @Lastname, @RentPortion, @MoveInDate, @RoomId)";
                    cmd.Parameters.AddWithValue("@Firstname", roommate.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", roommate.Lastname);
                    cmd.Parameters.AddWithValue("@RentPortion", roommate.RentPortion);
                    cmd.Parameters.AddWithValue("@MoveInDate", roommate.MoveInDate);
                    cmd.Parameters.AddWithValue("@RoomId", roommate.Room.Id);
                    int id = (int)cmd.ExecuteScalar();

                    roommate.Id = id;
                }
            }
        }

        public void Update(Roommate roommate)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Roommate
                                        SET Firstname = @Firstname,
                                             Lastname = @Lastname,
                                          RentPortion = @RentPortion,
                                           MoveInDate = @MoveInDate,
                                               RoomId = @RoomId
                                             WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@Firstname", roommate.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", roommate.Lastname);
                    cmd.Parameters.AddWithValue("@RentPortion", roommate.RentPortion);
                    cmd.Parameters.AddWithValue("@MoveInDate", roommate.MoveInDate);
                    cmd.Parameters.AddWithValue("@RoomId", roommate.Room.Id);
                    cmd.Parameters.AddWithValue("@Id", roommate.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Roommate WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
            
    }
}
