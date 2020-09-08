using System;
using System.Collections.Generic;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        /// <summary>
        ///  This is the address of the database.
        ///  We define it here as a constant since it will never change.
        /// </summary>
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            /* Selects all Rooms and displays them to the console
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

            Console.WriteLine("Getting All Rooms:");
            Console.WriteLine();

            List<Room> allRooms = roomRepo.GetAll();

            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }
            Console.WriteLine("--------------------------");
            */

            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            Console.WriteLine("Getting All Roommates:");
            Console.WriteLine();

            List<Roommate> allRoommates = roommateRepo.GetAll();

            foreach(Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname} {roommate.RentPortion} {roommate.MoveInDate}");
            }

            /////////////////////////////////////////
            /* Gets a single Room via Id
            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Room with Id 1");

            Room singleRoom = roomRepo.GetById(1);

            Console.WriteLine($"{singleRoom.Id} {singleRoom.Name} {singleRoom.MaxOccupancy}");
            */


            /////////////////////////////////////////
            /* Generates a new room and adds it to the database
            Room bathroom = new Room
            {
                Name = "Bathroom",
                MaxOccupancy = 1
            };

            roomRepo.Insert(bathroom);

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Added the new Room {bathroom.Name} with id {bathroom.Id}");
            */



            /////////////////////////////////////////
            /* Updates an entry in the database
            bathroom.MaxOccupancy = 3;

            roomRepo.Update(bathroom);
           foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }
            */



            /////////////////////////////////////////
            /* Deletes a room entry from the database
              
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Deleting bathrooms");
            roomRepo.Delete(12);
            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }*/

        }
    }
}