using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AWayData;
using AWayWeb.Code;
using AWayWeb.DataClasses;

namespace AWayWeb.Controllers
{
    [Authorize]
    public class ManagePhotosController : Controller
    {
        private IAWayRepository _repo;

        public ManagePhotosController(IAWayRepository repo)
        {
            _repo = repo;
        }

        //
        // GET: /ManagePhotos/

        public ActionResult Index()
        {
            DirectoryInfo d = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/Upload"));
            FileInfo[] aryFileInfo = d.GetFiles("*.jpg");

            return View(aryFileInfo);
        }

        //
        // GET: /ManagePhotos/Details/5

        public ActionResult Details(int id = 0)
        {
            Photo photo = _repo.GetPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        public ActionResult Upload()
        {
            List<Photo> pList = PhotoUpload.getUploadFiles();

            if (pList == null || pList.Count() < 1)
            {
                return View("Index");
            }

            foreach (Photo p in pList)
            {
                try
                {
                    _repo.AddPhoto(p);                
                }
                catch
                {
                    return View("Index");
                }
            }

            return View("NewFileList", pList);          
        }

        public ActionResult DeleteFiles()
        {
            DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/Upload"));

            foreach (FileInfo f in d.GetFiles("*.*"))
            {
                try
                {
                    f.Delete();
                }
                catch
                {
                    return RedirectToAction("Index", "ManagePhotos");
                }
            }

            return View("Index");
        }

        ////
        //// GET: /ManagePhotos/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /ManagePhotos/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Photo photo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Photos.Add(photo);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(photo);
        //}

        ////
        //// GET: /ManagePhotos/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Photo photo = db.Photos.Find(id);
        //    if (photo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(photo);
        //}

        ////
        //// POST: /ManagePhotos/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Photo photo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(photo).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(photo);
        //}

        ////
        //// GET: /ManagePhotos/Delete/5

        public ActionResult DeletePhoto(int id)
        {
            Photo photo = _repo.GetPhotoById(id);
            if (photo == null)
            {
                _repo.DeletePhoto(id);
            }
            return RedirectToAction("Index", "ManagePhotos");
        }

        ////
        //// POST: /ManagePhotos/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool IsDeleted = _repo.DeletePhoto(id);
            if (IsDeleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // TODO: show error in delete process
                return RedirectToAction("Index");
            }
        }

        /// </summary>
        /// <param name="file">FileInfo of required file</param>
        /// <returns>If that specified file is being processed 
        /// or not found is return true</returns>
        public static Boolean IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                //Don't change FileAccess to ReadWrite, 
                //because if a file is in readOnly, it fails.
                stream = file.Open
                (
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.None
                );
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

    }

}