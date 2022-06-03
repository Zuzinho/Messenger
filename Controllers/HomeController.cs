using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Messanger.Models;

namespace Messanger
{
    public class HomeController : Controller
    {
        private static int user_id = 1;
        private static DatabaseEntities1 db = new DatabaseEntities1();
        private static User user = db.User.FirstOrDefault(x => x.Id == user_id);
        private static User userTake;
        private static Room room;
        private static List<Message> lastMessages;
        private static List<Message> messages;
        List<Room> rooms = Room.GetRooms(user.RoomNumbers,db);
        public ActionResult Connect()
        {
            user_id++;
            user = db.User.FirstOrDefault(x => x.Id == user_id);
            List<Room> rooms = Room.GetRooms(user.RoomNumbers, db);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Index(int current_page = 0)
        {
            if(current_page == 0) current_page = rooms[0].Id;
            List<User> users = new List<User>();
            lastMessages = new List<Message>();
            foreach (Room room in rooms)
            {
                if(room.user1_id == user_id)
                {
                    users.Add(db.User.FirstOrDefault(x => x.Id == room.user2_id));

                }
                else
                {
                    users.Add(db.User.FirstOrDefault(x => x.Id == room.user1_id));
                }
                DataBase.CreateTable(room);
                lastMessages.Add(DataBase.GetLastMessage(room));
            }
            var view = (rooms,current_page ,users,lastMessages);
            return View(view);
        }
        [HttpGet]
        public ActionResult RoomPage(int room_id) {
            if (room != null)
            {
                if (room.Id != room_id)
                {
                    room = rooms.FirstOrDefault(x => x.Id == room_id && (x.user2_id == user_id || x.user1_id == user_id));
                }
            }
            else
            {
                room = rooms.FirstOrDefault(x => x.Id == room_id && (x.user2_id == user_id || x.user1_id == user_id));
            }
            if (room == null) return Content("<h1>Page is not found</h1>");
            messages = DataBase.GetMessages(room);
            if (room.user1_id == user_id)
            {
                userTake = db.User.FirstOrDefault(x => x.Id == room.user2_id);
            }
            else
            {
                userTake = db.User.FirstOrDefault(x => x.Id == room.user1_id);
            }
            var view = (user_id,messages,userTake);
            return View(view);
        }

        [HttpPost]
        public void AddMessage(string message)
        {
            DateTime time = DateTime.Now;
            DataBase.AddMessage(room, message, time, user_id, userTake.Id);
            int id;
            if (messages.Count == 0)
            {
                id = 1;
            }
            else
            {
                id = messages.Last().id + 1;
            }
            Message newMessage = new Message(id, message, time, user.Id, userTake.Id);
            messages.Add(new Message(id, message, time, user.Id, userTake.Id));
            int index = rooms.FindIndex(x => x.user1_id == userTake.Id || x.user2_id == userTake.Id);
            lastMessages.RemoveAt(index);
            lastMessages.Insert(index,newMessage);
        }
    }
}