using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TLTCBlog.Models;

namespace TLTCBlog.Controllers
{
    public class CommentsController : Controller
    {
        private TLTCBlogDbContext db = new TLTCBlogDbContext();

        /// <summary>
        /// Gets the article based on the passed in ID from the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>article object</returns>
        private BlogArticle getArticle(int? id)
        {
            var article = db.BlogArticles.Where(a => a.ArticleID == id).Include(a => a.Creator).Include(a => a.Comments).FirstOrDefault();
        
            return article;
        }

        /// <summary>
        /// Gets the comments from that article
        /// </summary>
        /// <param name="id"></param>
        /// <returns>comment object</returns>
       /* private List<Comment> GetComments(int? id)
        {
            var comment = db.BlogArticles.Where(a => a.ArticleID == id).Include(a => a.Creator).Include(a => a.Comments);

            List<Comment> comments = new List<Comment>();
            comments.Add(comment);
            return comment;
        }*/
    

        /// <summary>
        /// GET Comments/List
        /// </summary>
        /// <returns>list of comments</returns>
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Article).Include(c => c.Creator);
            return View(comments.ToList());
        }

        /// <summary>
        /// GET Comments/Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>commment with all relevant fields</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }


        /// <summary>
        /// GET Comments/Create
        /// </summary>
        /// <param name="article"></param>
        /// <returns>Comment View</returns>
        public ActionResult Create(ArticleViewModel article)
        {
            getArticle(article.Id);
            ViewBag.CreatorID = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// POST Comments/Create
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>new comment entry on article and in DB</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentViewModel model, int? id)
        {
            var article = getArticle(id); 

            if (ModelState.IsValid)
            {

                Comment comment = new Comment()
                {
                    articleID = (int)id,
                    CreatorID = User.Identity.GetUserId(),
                    Text = model.Text,

                };

                article.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Article", new { id });
            }

            ViewBag.CreatorID = new SelectList(db.Users, "Id", "FullName", model.CreatorID);
            return View(model);
        }

        /// <summary>
        /// GET Comments/Edit
        /// </summary>
        /// <param name="commentID"></param>
        /// <param name="id"></param>
        /// <returns>current comment</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (db)
            {
                Comment comment = db.Comments.Find(id);

            if (comment == null)
                {
                    return HttpNotFound();
                }
                ViewBag.PostId = new SelectList(db.BlogArticles, "ArticleId", "Title", comment.articleID);
                ViewBag.UserId = new SelectList(db.Users, "Id", "Forename", comment.CreatorID);
                return View(comment);
            }
        }

        /// <summary>
        /// POST Comments/Edit
        /// gets the data from the comments edit page and updates the comment in the database
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>Comments Index Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Date,Body,UserId,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostId = new SelectList(db.BlogArticles, "ArticleId", "Title", comment.articleID);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Forename", comment.CreatorID);
            return View(comment);
        }

        /// <summary>
        /// GET Comments/Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        /// <summary>
        /// POST Comment/Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// DB Disposal function
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
