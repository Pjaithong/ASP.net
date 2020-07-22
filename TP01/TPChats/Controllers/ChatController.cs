using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPChats.Database;
using TPChats.Models;

namespace TPChats.Controllers
{
    public class ChatController : Controller
    {
        
        // GET: Chat
        public ActionResult Index()
        {
            return View(FakeDb.Instance.Chats);
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            Chat chat = FakeDb.Instance.Chats.Single(c => c.Id.Equals(id));
            if (chat != null)
            {
                return View(chat);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        
        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            Chat chat = FakeDb.Instance.Chats.Single(c => c.Id.Equals(id));
            if (chat != null)
            {
                return View(chat);
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Chat chat = FakeDb.Instance.Chats.Single(c => c.Id.Equals(id));

                FakeDb.Instance.Chats.Remove(chat);
                return RedirectToAction("Index");
            }
            catch
            {
                
                return View(id);
            }
            
        }
    }
}
