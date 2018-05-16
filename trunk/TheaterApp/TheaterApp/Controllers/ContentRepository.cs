using System;
using System.Data.Entity;
using System.IO;
using System.Web;
using TheaterApp.Models;

namespace TheaterApp.Controllers
{
    internal class ContentRepository
    {
        private Context db = new Context();
        public Movie UploadImageInDataBase(HttpPostedFileBase file, Movie contentViewModel)
        {
            contentViewModel.Logo = ConvertToBytes(file);
            var movie = new Movie
            {
                MovieName = contentViewModel.MovieName,
                Description = contentViewModel.Description,
                Logo = contentViewModel.Logo
            };
            return movie ;
            
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {

            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}