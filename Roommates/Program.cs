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
            //Selects all Rooms and displays them to the console
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

            Console.WriteLine("Getting All Rooms:");
            Console.WriteLine(); 

            List<Room> allRooms = roomRepo.GetAll();

            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }
            Console.WriteLine("--------------------------");
            

            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            Console.WriteLine("Getting All Roommates:");
            Console.WriteLine();

            List<Roommate> allRoommates = roommateRepo.GetAll();
            

            foreach(Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname} {roommate.RentPortion} {roommate.MoveInDate}");
            }
            Console.WriteLine("-------------------------------");

            /////////////////////////////////////////
            /* Gets a single Room via Id
            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Room with Id 1");

            Room singleRoom = roomRepo.GetById(1);

            Console.WriteLine($"{singleRoom.Id} {singleRoom.Name} {singleRoom.MaxOccupancy}");
            */

            Console.WriteLine("Getting Roommate with Id 2");

            Roommate singleRoommate = roommateRepo.GetById(2);

            Console.WriteLine($"{singleRoommate.Id} {singleRoommate.Firstname} {singleRoommate.Lastname} {singleRoommate.RentPortion} {singleRoommate.MoveInDate}");
            Console.WriteLine("-------------------------------");

            Console.WriteLine("Getting Roommates within a Specific Room");

            List<Roommate> roommatesWithRoom = roommateRepo.GetAllWithRoom(3);
            foreach (Roommate roommate in roommatesWithRoom)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname} lives in the {roommate.Room.Name}, pays ${roommate.RentPortion}, and moved in on {roommate.MoveInDate}");
            }
            Console.WriteLine("-------------------------------");
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


            /*
            Roommate Kevin = new Roommate
            {
                Firstname = "Kevin",
                Lastname = "Schmidt",
                RentPortion = 75,
                MoveInDate = new DateTime(2015, 12, 31),
                Room = allRooms[1]
            };

            roommateRepo.Insert(Kevin);
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Added a new roommate, please welcome {Kevin.Id} {Kevin.Firstname} {Kevin.Lastname}");
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

            /*
            Kevin.RentPortion = 500;
            roommateRepo.Update(Kevin);
            Console.WriteLine($"{Kevin.Firstname}'s new rent payment is ${Kevin.RentPortion}");
            Console.WriteLine("-------------------------------");
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