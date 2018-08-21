using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using AWayData;
using AWayWeb.Models;

namespace AWayWeb.DataClasses
{
    public class AWayRepository : IAWayRepository
    {
        private AWayEntities _context;

        public AWayRepository(AWayEntities context)
        {
            _context = context;
        }

        public AWayData.Photo GetRandomPhoto()
        {
            return _context.getRandomPhoto().FirstOrDefault();           
        }

        public List<Photo> GetPhotosTop(int numberOfPhotosToGet)
        {
            return _context.Photos.OrderByDescending(p => p.PhotoDate).Take(numberOfPhotosToGet).ToList();
        }

        public List<Photo> GetPhotosBottom(int numberOfPhotosToGet)
        {
            return _context.Photos.OrderBy(p => p.PhotoDate).Take(numberOfPhotosToGet).ToList();
        }

        public Photo GetPhotoById(int id)
        {
            return _context.Photos.Where(p => p.PhotoId == id).FirstOrDefault();
        }

        public Photo GetPhotoByDate(DateTime dt)
        {
            throw new NotImplementedException();
        }

        public Photo GetPhotoByCaption(string caption)
        {
            return _context.Photos.Where(c => c.Caption == caption).FirstOrDefault();
        }

        public List<vwPhotoData> GetRandomPhotoData(int numberOfPhotosToGet)
        {
            List<vwPhotoData> photos = new List<vwPhotoData>();

            for (int i = 0; i < numberOfPhotosToGet; i++)
            {
                photos.Add(_context.spGetRandomPhotoData().First());
            }

            return photos;
        }

        public List<Photo> GetPhotosByDate(DateTime dt)
        {
            return _context.Photos.Where(p => EntityFunctions.TruncateTime(p.PhotoDate) == dt.Date).ToList();
        }

        public List<Photo> GetPhotosByDate(string date)
        {
            DateTime dt = DateTime.Parse(date);
            return _context.Photos.Where(p => EntityFunctions.TruncateTime(p.PhotoDate) == dt.Date).ToList();
        }

        public bool AddPhoto(Photo photo)
        {
            
            if (GetPhotoByCaption(photo.Caption) != null)
            {
                return false;
            }

            try
            {
                _context.Photos.Add(photo);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool DeletePhoto(int id)
        {
            try
            {
                Photo photo = _context.Photos.Where(p => p.PhotoId == id).FirstOrDefault();
                _context.Photos.Remove(photo);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }           
        }


        public BookText getRandomBookText()
        {
            getRandomBookText_Result result = _context.getRandomBookText().First();
            BookText bt = new BookText
            {
                Author = result.Author,
                ShortName = result.ShortName,
                Text = result.Text,
                Title = result.Title,
                Reference = result.Reference
            };

            return bt;
        }
    }
}