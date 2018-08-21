using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AWayData;
using AWayWeb.Models;

namespace AWayWeb.DataClasses
{
    public interface IAWayRepository
    {
        List<Photo> GetPhotosTop(int numberOfPhotosToGet);
        List<Photo> GetPhotosBottom(int numberOfPhotosToGet);
        Photo GetPhotoById(int id);
        Photo GetPhotoByDate(DateTime dt);
        List<Photo> GetPhotosByDate(DateTime dt);
        List<Photo> GetPhotosByDate(string date);
        Photo GetRandomPhoto();
        Photo GetPhotoByCaption(string caption);
        List<vwPhotoData> GetRandomPhotoData(int numberOfPhotosToGet);
        bool DeletePhoto(int id);
        bool AddPhoto(Photo photo);
        BookText getRandomBookText();
    }
}