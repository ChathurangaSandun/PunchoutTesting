using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using PunchoutTesting.Models;

namespace PunchoutTesting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string request = "";
            var fileStream = new FileStream(@"C:\Users\Chathuranga.Sandun\Documents\visual studio 2017\Projects\PunchoutTesting\PunchoutTesting\Documentations\request.xml", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                request = streamReader.ReadToEnd();
            }

            var xmlSerializer = new XmlSerializer(typeof(MainRequest));

            MainRequest punchOutSetupRequest = (MainRequest) xmlSerializer.Deserialize(new StringReader(request));


       
          
            MainResponse mainResponse = new MainResponse()
            {
                PayloadID = punchOutSetupRequest.PayloadID,
                Timestamp = punchOutSetupRequest.Timestamp,
                Response = new Response()
                {
                    Status = new Status()
                    {
                        Code = "200",
                        Text = "Ok" 
                    },
                    PunchOutSetupResponse = new PunchOutSetupResponse()
                    {
                        StartPage = new StartPage()
                        {
                            URL = ""
                        }
                    }

                }
            };

            var stringwriter = new StringWriter();

            XmlSerializer serializer = new XmlSerializer(typeof(MainResponse));
            serializer.Serialize(stringwriter, mainResponse);
            var s = stringwriter.ToString();

            return View(s);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}