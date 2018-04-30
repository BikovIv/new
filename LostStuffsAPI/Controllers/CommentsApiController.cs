using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using LostStuffs.Entities;
using LostStuffs.Models;
using LostStuffs.DataAccess;
using LostStuffs.Models.CommentModels;

namespace LostStuffs.Controllers
{
    [RoutePrefix("api/comment")]
    public class CommentsApiController : ApiController
    {
        CommentsRepository repository = new CommentsRepository();
        private ApplicationDbContext db = new ApplicationDbContext();

        LostStuffsRepository repo = new LostStuffsRepository();

        // GET: api/CommentsApi
        [Route("get")]
        public IHttpActionResult GetComments(int id)  //LostStuff id !
        {
            try
            {
                List<Comment> allCommetns = new List<Comment>();
                allCommetns.AddRange(repository.GetAll().Where(x => x.LostStuffId == id));

                List<object> result = new List<object>();

                var lostStuffName = repo.GetById(id).Name;

                foreach (var comment in allCommetns)
                {
                    result.Add(new
                    {
                        comment.UserName,
                        comment.UserId,
                        comment.LostStuffId,
                        comment.Content,
                        lostStuffName
                    }
                    );
                }
                return Ok(result);
            }
            catch (Exception)
            {
               
                return BadRequest("Invalid id!");
            }
          
        }

        // POST: api/CommentsApi
        [ResponseType(typeof(Comment))]
        [HttpPost]
        [Route("post")]
        public IHttpActionResult PostComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            comment.LostStuffId = id;
            comment.CreatedAt = DateTime.Now;
            comment.UpdatedAt = DateTime.Now;
            repository.Add(comment);

            return Ok(comment);
        }

        // PUT: api/CommentsApi/5
        [ResponseType(typeof(void))]
        [Route("edit")]
        public IHttpActionResult PutComment(int id, CommentRequestModel commentRequest)
        {
            Comment comment;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                comment = repository.GetById(id);
            }
            catch (Exception)
            {

                throw;
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }
            //
            commentRequest.Id = comment.Id;
            commentRequest.LostStuffId = comment.LostStuffId;
            //

            comment.UpdatedAt = DateTime.Now;
            comment.Content = commentRequest.Content;

            repository.Update(comment);

            return Ok();
        }


        // DELETE: api/CommentsApi/5
        [ResponseType(typeof(Comment))]
        [Route("delete")]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = repository.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            repository.Delete(comment);

            return Ok("The comment is deleted");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.Id == id) > 0;
        }
    }
}