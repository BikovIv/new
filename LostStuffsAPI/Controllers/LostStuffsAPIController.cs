using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using LostStuffs.Entities;
using LostStuffs.Models;
using LostStuffs.DataAccess;
using System.Net.Http;
using System;
using System.Web;
using System.IO;

namespace LostStuffs.Controllers
{
    [RoutePrefix("api/lost-stuffs")]
    public class LostStuffsAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        LostStuffsRepository repository = new LostStuffsRepository();

        // GET: api/LostStuffsAPI
        [Route("get-all")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<LostStuff> allLostStuffs = new List<LostStuff>();
            allLostStuffs = repository.GetAll();

            List<object> result = new List<object>();

            foreach (var item in allLostStuffs)
            {
                result.Add(new
                {
                    name = item.Name,
                    decscription = item.Description,
                    userName = item.UserName,
                    price = item.Price,
                    imageName = item.ImageName,
                    imagePath = item.ImagePath,
                    phoneNumber = item.PhoneNumber
                });
            }

            return this.Ok(result);
        }

        // GET: api/LostStuffsAPI/5
        //[ResponseType(typeof(LostStuff))]
        [Route("get")]
        public IHttpActionResult GetLostStuff(int id)
        {
            LostStuff lostStuff = repository.GetById(id);

            if (lostStuff == null)
            {
                return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }

            return this.Ok(new
            {
                name = lostStuff.Name,
                description = lostStuff.Description,
                price = lostStuff.Price,
                userId = lostStuff.UserId,
                userName = lostStuff.UserName
            });
        }

        // POST: api/LostStuffsAPI
        [ResponseType(typeof(LostStuff))]
        [HttpPost]
        [Route("post")]
        public IHttpActionResult PostLostStuff(LostStuffRequestModel lostStuffRequest)    //ostava problema sys snimkite

        {
            LostStuff lostStuff = new LostStuff();

            var httpRequest = HttpContext.Current.Request;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            lostStuff.CreatedAt = DateTime.Now;
            lostStuff.UpdatedAt = DateTime.Now;
            lostStuff.UserId = "2d1b6e60-70fa-4439-8532-40667bb76941";
            lostStuff.UserName = "Ivan";
            lostStuff.Name = lostStuffRequest.Name;
            lostStuff.Description = lostStuffRequest.Description;
            lostStuff.Price = lostStuffRequest.Price;
            lostStuff.PhoneNumber = lostStuffRequest.PhoneNumber;
            db.LostStuffs.Add(lostStuff);
            db.SaveChanges();

            ////create directory for entity
            string directoryPath = string.Format("~/Content/UploadedFiles/" + lostStuff.Id.ToString() + "/");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directoryPath));
            }

            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    if (file.Equals("mainImage"))
                    {
                        if (httpRequest.Files[file] != null)
                        {
                            lostStuff.ImageName = postedFile.FileName;
                            lostStuff.ImagePath = directoryPath + postedFile.FileName;
                            postedFile.SaveAs(HttpContext.Current.Server.MapPath(directoryPath + postedFile.FileName));
                            db.SaveChanges();
                        }
                        else
                        {
                            lostStuff.ImageName = "default.jpg";
                            lostStuff.ImagePath = "~/Content/UploadedFiles/default.jpg";
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        if (httpRequest.Files[file] != null)
                        {
                            postedFile.SaveAs(HttpContext.Current.Server.MapPath(directoryPath + postedFile.FileName));
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            else
            {
                lostStuff.ImageName = "default.jpg";
                lostStuff.ImagePath = "~/Content/UploadedFiles/default.jpg";
                db.SaveChanges();
            }
            return Ok();
        }

        // PUT: api/LostStuffsAPI/5
        [ResponseType(typeof(void))]
        [Route("put")]
        public IHttpActionResult PutLostStuff(int id, LostStuffRequestModel lostStuffRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LostStuff lostStuff;
            try
            {
                lostStuff = repository.GetById(id);
                lostStuffRequest.Id = lostStuff.Id;
            }
            catch (Exception ex)
            {
                return this.NotFound();
            }

            if (lostStuff.Id != lostStuffRequest.Id)
            {
                return BadRequest();
            }

            lostStuff.UpdatedAt = DateTime.Now;
            lostStuff.UserId = "2d1b6e60-70fa-4439-8532-40667bb76941";
            lostStuff.UserName = "Ivan";
            lostStuff.Name = lostStuffRequest.Name;
            lostStuff.Description = lostStuffRequest.Description;
            lostStuff.Price = lostStuffRequest.Price;
            lostStuff.PhoneNumber = lostStuffRequest.PhoneNumber;
            repository.Update(lostStuff);

            return this.Ok(lostStuffRequest);
        }

        // DELETE: api/LostStuffsAPI/5
        [ResponseType(typeof(LostStuff))]
        [Route("delete")]
        public IHttpActionResult DeleteLostStuff(int id)
        {
            LostStuff lostStuff = repository.GetById(id);
            if (lostStuff == null)
            {
                return NotFound();
            }

            string directoryPath = string.Format("~/Content/UploadedFiles/" + lostStuff.Id.ToString() + "/");

            try
            {
                Directory.Delete(HttpContext.Current.Server.MapPath(directoryPath));
            }catch (Exception ex)
            {

            }
            repository.Delete(lostStuff);
            return Ok(lostStuff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LostStuffExists(int id)
        {
            return db.LostStuffs.Count(e => e.Id == id) > 0;
        }
    }
}