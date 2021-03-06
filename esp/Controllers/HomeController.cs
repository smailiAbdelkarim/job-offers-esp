﻿using esp.Models;
using esp_test.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace esp_test.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); //db reference from database
        public ActionResult Index()
        {

            return View(db.Categories.ToList()); //list categories
        }
        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);

            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);

        }
        [Authorize]
        public ActionResult Apply() {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var userId = User.Identity.GetUserId(); //id de user deja connecter
            var jobId = (int)Session["JobId"]; // job id ...get from session
            var check = db.ApplyForJobs.Where(a => a.JobId == jobId && a.UserId == userId).ToList();

            if (check.Count < 1)
            {
                var job = new ApplyForJob();
                job.UserId = userId;
                job.JobId = jobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "Your apply successed";

            }
            else {
                ViewBag.Result = "You cannot apply to this job again !!";
            }
            return View();

        }
        [Authorize]
        public ActionResult GetJobsByUser() {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());

        }
        [Authorize]
        public ActionResult GetJobsByPublisher() {
            var UserID = User.Identity.GetUserId();
            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.UserID == UserID 
                       select app;

            var grouped = from j in Jobs
                          group j by j.job.JobName
                         into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                             Items = gr
                         };

            return View(grouped.ToList());

        }
        [Authorize]
        public ActionResult DetailsOfJobs(int id) {
            var job = db.ApplyForJobs.Find(id);

            if (job == null)
            {
                return HttpNotFound();
            }
            
            return View(job);
        }
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            var myJob = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(myJob);
            db.SaveChanges();
            return RedirectToAction("GetJobsByUser");
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
          

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("abdelkarimsmaili3@gmail.com","gll44g2q52egll44g2q52e");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("abdelkarimsmaili709@gmail.com"));
            mail.Subject = contact.Subject;
            mail.Body = contact.Message;
            var smtpClient = new SmtpClient("smtp.gmail.com",587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);


            return View();
        }






        public ActionResult Search() {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName) {
            var result = db.Jobs.Where(a => a.JobName.Contains(searchName)
            || a.JobPlace.Contains(searchName)
            || a.JobDescription.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }
    }
}