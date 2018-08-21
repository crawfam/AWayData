using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AWayData;
using AWayWeb.DataClasses;

namespace AWayWeb.Api
{
    public class PhotoImageController : ApiController
    {
        private IAWayRepository _repo;

        public PhotoImageController(IAWayRepository repo)
        {
            _repo = repo;
        }

        // TODO: consolidate the HttpResponseMessage code to a single method for all
        // See: http://www.codeguru.com/csharp/.net/returning-images-from-asp.net-web-api.htm

        //// GET api/PhotoImage/
        public HttpResponseMessage Get()
        {
            byte[] imgData = _repo.GetRandomPhoto().BytesFull;
            MemoryStream ms = new MemoryStream(imgData);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }
        
        //// GET api/PhotoImage/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                byte[] imgData = _repo.GetPhotoById(id).BytesFull;
                MemoryStream ms = new MemoryStream(imgData);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(ms);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                return response;
            }
            catch
            {
                return null;
            }
        }


    }
}
