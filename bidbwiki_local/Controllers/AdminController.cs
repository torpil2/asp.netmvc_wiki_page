using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Sql;
using static bidbwiki_local.Classes.UrlFilter;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Data.Entity;


namespace bidbwiki_local.Controllers
{


    public class AdminController : Controller
    {



        //Users deneme = new Users();
        bidbwikiEntities1 dbContext = new bidbwikiEntities1();
        // GET: Admin

        public ActionResult Index()
        {

            try
            {
                if (Session["UserID"] != null)
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();
                    var posts = db.Posts.OrderByDescending(c => c.Create_Time).Take(5);
                    return View(posts);

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult LoginScreen()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users objUser)
        {
            if (ModelState.IsValid)
                using (bidbwikiEntities1 db = new bidbwikiEntities1())
                {
                    var obj = db.Users.Where(a => (a.username.Equals(objUser.username)) && a.password.Equals(objUser.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.user_id;
                        Session["UserName"] = obj.username.ToString();
                        obj.last_login = DateTime.Now;

                        db.SaveChanges();
                        return RedirectToAction("UserDashboard");

                    }
                }
            return View(objUser);

        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }


        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                bidbwikiEntities1 db = new bidbwikiEntities1();
                var girisyapankullanici = Session["UserId"];
                var posts = db.Posts.OrderByDescending(c => c.Create_Time).Where(x=> x.user_ID == (Int32)girisyapankullanici).ToList();
                return View(posts);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }



        [PreventFromUrl]
        public ActionResult AddUser(Users users)
        {
            try
            {
                if (Session["UserName"].ToString().Equals("admin"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }



        }


        // return RedirectToAction("ViewUsers","Admin");

        [PreventFromUrl]
        public ActionResult AddUserFunc(Users User)
        {

            if (Session["UserName"].ToString().Equals("admin"))
            {
                try
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();
                    Users user = new Users();
                    user.name = User.name;
                    user.email = User.email;
                    user.username = User.username;
                    user.password = User.password;
                    user.created_time = DateTime.Today;
                    db.Users.Add(user);
                    db.SaveChanges();

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index");
            }




            // return RedirectToAction("ViewUsers","Admin");
            return RedirectToAction("Index");
        }

        [PreventFromUrl]
        public ActionResult ViewUsers()
        {



            try
            {
                if (Session["UserName"].ToString().Equals("admin"))
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();

                    return View(db.Users.ToList());
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [PreventFromUrl]
        public ActionResult AddCategory()
        {
            if (Session["UserID"] != null)
            {
                try
            {
                return View();

            }
            catch (Exception)
            {

                throw;
            }

            }
           else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PreventFromUrl]
        public ActionResult AddCategoryFunc(Category Category)
        {

            if (Session["UserID"] != null)
            {
                try
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();

                    Category category = new Category();
                    category.name = Category.name;
                    db.Category.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("ViewCategories");

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("ViewCategories");
            }


            // return RedirectToAction("ViewUsers","Admin");

        }

        [PreventFromUrl]
        public ActionResult ViewCategories()
        {

            try
            {
                if (Session["UserID"] != null)
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();

                    return View(db.Category.ToList());
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [PreventFromUrl]
        public ActionResult ViewPosts()
        {

            try
            {
                if (Session["UserID"] != null)
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();
                    var posts = db.Posts.OrderByDescending(c => c.Create_Time).ToList();
                    
                    return View(posts);

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [PreventFromUrl]
        public ActionResult AddPost()
        {

            if (Session["UserID"] != null)
            {
                try
            {
                bidbwikiEntities1 db = new bidbwikiEntities1();

                List<SelectListItem> kategoriler = (from i in db.Category.ToList() select new SelectListItem { Text = i.name, Value = i.id.ToString() }).ToList();
                ViewBag.dgrcat = kategoriler;
                return View();

            }
            catch (Exception)
            {

                throw;
            }

            }
            else
            {
              return RedirectToAction("Index");
            }
            //Sonradan değiştirelecek
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PreventFromUrl]
        public ActionResult AddPostFunc(Posts postform)
        {

            if (Session["UserID"] != null)
            {
                try
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();

                    Posts post = new Posts();
                    post.Post_Name = postform.Post_Name;
                    post.Category_ID = postform.Category_ID;
                    post.Create_Time = DateTime.Now;
                    post.user_ID = (Int32)Session["UserID"];
                    db.Posts.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("ViewPosts");

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("ViewPosts");
            }
            // return RedirectToAction("ViewUsers","Admin");

        }

        
        
        [PreventFromUrl]
        public ActionResult ChangeCategory(int id)
        {

            bidbwikiEntities1 db = new bidbwikiEntities1();
            Posts post = db.Posts.Where(r => r.post_id == id).FirstOrDefault();
            List<SelectListItem> kategoriler = (from i in db.Category.ToList() select new SelectListItem { Text = i.name, Value = i.id.ToString()}).ToList();
            ViewBag.dgrcat = kategoriler;
            return View("ChangeCategory",post);
        }

        [HttpPost]
        public ActionResult ChangeCategoryFunc(Posts post)
        {
            //Posts post = new Posts();
            //post.Post_Name = postform.Post_Name;
            //post.Category_ID = postform.Category_ID;
            //post.Create_Time = DateTime.Now;
            //post.user_ID = (Int32)Session["UserID"];
            //db.Posts.Add(post);
            //db.SaveChanges();

            bidbwikiEntities1 db = new bidbwikiEntities1();
            Posts posted = db.Posts.Where(s => s.post_id == post.post_id).FirstOrDefault();
            posted.Post_Name = post.Post_Name;
            posted.Category_ID = post.Category_ID;
            post.Create_Time = DateTime.Now;
            UpdateModel(posted);
            db.SaveChanges();

            
            return RedirectToAction("ViewPosts");
        }

       
        //DÜZENELENCEK*************
        //public ActionResult DescpPost(int postid) 
        //{

        //    bidbwikiEntities1 db = new bidbwikiEntities1();
        //    Description desc = db.Description.Where(s => s.post_ID == postid).FirstOrDefault();


        //    return View("ViewPosts",desc);
        //}




        [PreventFromUrl]
        public ActionResult EditPost(int id, Description adesc)
        {

            if (Session["UserID"] != null)
            {
                try
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();


                    Posts post = db.Posts.Find(id);
                    Session["PostID"] = id;

                    if (post == null)
                    {

                        return HttpNotFound();
                    }
                    return View("EditPost", adesc);
                }
                catch (Exception)
                {
                    return View("ViewPosts");
                    throw;
                }

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PreventFromUrl]
        public ActionResult EditPostFunc(Description desc)
        {

            if (Session["UserID"] != null)
            {
                try
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();

                    Description description = new Description();
                    description.Description_Text = desc.Description_Text;


                    description.post_ID = (Int32)Session["PostID"];


                    description.user_ID = (Int32)Session["UserID"];
                    db.Description.Add(description);
                    db.SaveChanges();
                    return RedirectToAction("ViewPosts");

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("ViewPosts");
            }

        }


      
        [PreventFromUrl]
        public ActionResult PostDetails(int id)
        {
           
            if (Session["UserID"] != null)
            {
                try
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();

                    Description desc = db.Description.Where(r => r.post_ID == id).FirstOrDefault();

                    return View("PostDetails", desc);
                }
                catch
                {
                    throw;
                }
            }

            else
            {
                return View("Index");


            }

        }


      
        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile(HttpPostedFileBase aUploadedFile)
        {
            
                    var vReturnImagePath = string.Empty;
                    if (aUploadedFile.ContentLength > 0)
                    {
                        var vFileName = Path.GetFileNameWithoutExtension(aUploadedFile.FileName);
                        var vExtension = Path.GetExtension(aUploadedFile.FileName);

                        string sImageName = vFileName + DateTime.Now.ToString("YYYYMMDDHHMMSS");

                        var vImageSavePath = Server.MapPath("/UpImages/") + sImageName + vExtension;
                        //sImageName = sImageName + vExtension;  
                        vReturnImagePath = "/UpImages/" + sImageName + vExtension;
                        ViewBag.Msg = vImageSavePath;
                        var path = vImageSavePath;

                        // Saving Image in Original Mode  
                        aUploadedFile.SaveAs(path);
                        var vImageLength = new FileInfo(path).Length;
                        //here to add Image Path to You Database ,  
                        TempData["message"] = string.Format("Image was Added Successfully");
                    }
                    return Json(Convert.ToString(vReturnImagePath), JsonRequestBehavior.AllowGet);
                }



        [PreventFromUrl]
        public ActionResult ViewPostsWithCat(int id)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    bidbwikiEntities1 db = new bidbwikiEntities1();                  
                   
                    return View(db.Posts.Where(r => r.Category_ID == id).ToList());
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]       
        [PreventFromUrl]
        public ActionResult DeletePost(int id)
        {

            bidbwikiEntities1 db = new bidbwikiEntities1();

            Posts post = db.Posts.Where(r => r.post_id == id).FirstOrDefault();
            var item = db.Description.Where(x => x.post_ID == id);


            db.Description.RemoveRange(item);
            db.Posts.Remove(post);
            db.SaveChanges();
            
            return RedirectToAction("ViewPosts");
        }


        [HttpPost]
        [PreventFromUrl]
        public ActionResult DeleteCat(int id)
        {
            bidbwikiEntities1 db = new bidbwikiEntities1();

            Category cat = db.Category.Where(r => r.id == id).FirstOrDefault();
            var catposts = db.Posts.Where(x => x.Category_ID == id).ToList();
            catposts.ForEach(a => a.Category_ID = null);

            
            db.Category.Remove(cat);
            db.SaveChanges();

            return RedirectToAction("ViewCategories");
        }

    }

}