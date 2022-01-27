using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using playWmvc.Models;
using System.Runtime.Caching;

namespace playWmvc.Controllers
{
    public class HomeController : Controller
    {
        ObjectCache cache = MemoryCache.Default;
        List<User> users = new List<User>();
        public HomeController()
        {
            users = cache["users"] as List<User>;
            if (users == null)
            {
                users = new List<User>();
            }
        }
        public void SaveCache()
        {
            cache["users"] = users;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ViewUser(string id)//string Name, string PhoneNumber, GenderType Gender, bool Glasses=false)
        {
            User user = users.FirstOrDefault(u => u.Id == id);
            //User user = new User();
            //user.Id = (Guid.NewGuid().ToString());
            //user.Gender = u.Gender;
            //user.Name = u.Name;//"Ziv";
            //user.PhoneNumber = u.PhoneNumber;//"0544777548";
            //user.HasGlasses = u.HasGlasses;// true;

            //List<User> users = new List<User>();
            //users.Add(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(user);
            }
        }
        public ActionResult GetUserList()
        {
            //List<User> users = new List<User>();
            //users.Add(new User()
            //{
            //    Gender = GenderType.Female,
            //    Name = "Liora",
            //    HasGlasses = false,
            //    PhoneNumber ="0564821953",
            //    Id= Guid.NewGuid().ToString()
            //});
            //users.Add(new User()
            //{
            //    Gender = GenderType.Male,
            //    Name = "Lebron",
            //    HasGlasses = false,
            //    PhoneNumber ="0536428665",
            //    Id= Guid.NewGuid().ToString()
            //});
            //users.Add(new User()
            //{

            //    Id = (Guid.NewGuid().ToString()),
            //    Gender = GenderType.Male,
            //    Name = "Ziv",
            //    PhoneNumber = "0544777548",
            //    HasGlasses = true,

            //});
            return View(users);
        }
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            user.Id = Guid.NewGuid().ToString();
            users.Add(user);
            SaveCache();
            return RedirectToAction("GetUserList");
        }
        public ActionResult EditUser(string id)
        {
            User user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(user);
            }
        }
        [HttpPost]
        public ActionResult EditUser(User user, string id)
        {
            User userToEdit = users.FirstOrDefault(u => u.Id == id);//FirstOrDefault is a reference and this is why when you compare the attribute with the user and this is why we dont need to add the list
            if (userToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                userToEdit.Name = user.Name;
                userToEdit.PhoneNumber = user.PhoneNumber;
                userToEdit.HasGlasses = user.HasGlasses;

                //users.Where(userId => userToEdit.Id == userId.Id) = userToEdit;
                SaveCache();
                return RedirectToAction("GetUserList");
            }

        }
        //[HttpPost]
        //public ActionResult ConfirmDeleteUser(string id)
        //{
        //    User user = users.FirstOrDefault(u => u.Id == id);
        //     if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        users.Remove(user);
        //        //SaveCache();
        //        return RedirectToAction("GetUserList");//RedirectToAction("GetUserList");
        //    }
        //    //User user = users.FirstOrDefault(u => u.Id == id);
        //    //users.Remove(user);

        //    //RedirectToAction("GetUserList");
        //}
        //public ActionResult ConfirmDeleteUser(string id)
        //{

        //    User user = users.FirstOrDefault(u => u.Id == id);

        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        //users.Remove(user);
        //        //SaveCache();
        //        return View(user);//RedirectToAction("GetUserList");
        //    }

        //}
        public ActionResult DeleteUser(string id)
        {
            User user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(user);
            }

        }
        [HttpPost]
        [ActionName("DeleteUser")]
         public ActionResult ConfirmDeleteUser(string id)
        {
            User user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                users.Remove(user);
                return RedirectToAction("GetUserList");
            }

        }

        public PartialViewResult Basket()
        {
            BasketViewModel model = new BasketViewModel();
            model.Total = "100$";
            model.Count = 5;
            return PartialView(model);
        }
    }
}