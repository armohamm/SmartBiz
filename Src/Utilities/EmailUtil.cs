using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Serilog;
using System.Threading;

namespace ThirdParty.lib
{
    public class EmailUtil
    {

        public static string FullyQualifiedApplicationPath(HttpRequestBase httpRequestBase)
        {
            string appPath = string.Empty;

            if (httpRequestBase != null)
            {
                //Formatting the fully qualified website url/name
                appPath = string.Format("{0}://{1}{2}{3}",
                            httpRequestBase.Url.Scheme,
                            httpRequestBase.Url.Host,
                            httpRequestBase.Url.Port == 80 ? string.Empty : ":" + httpRequestBase.Url.Port,
                            httpRequestBase.ApplicationPath);
            }

            if (!appPath.EndsWith("/"))
            {
                appPath += "/";
            }

            return appPath;
        }


        public static void SendEmail(Controller Controller, EmailRequest request)
        {
            string subject = RenderSubjectToString(request);
            string body = RenderEmailToString(Controller.ControllerContext, request, request.Template);
            SendEmail(request.ToEmail, subject, body, null);
        }


        public static void SendEmail(Controller Controller, List<EmailRequest> requestList)
        {
            foreach (EmailRequest request in requestList)
            {
                string subject = RenderSubjectToString(request);
                string body = RenderEmailToString(Controller.ControllerContext, request, request.Template);
                SendEmail(request.ToEmail, subject, body, null);
            }
        }

        private static string RenderSubjectToString(EmailRequest request)
        {
            string subject = request.Subject;
            foreach (string key in request.Params.Keys)
            {
                subject = subject.Replace("@" + key + "@", request.Params[key]);
            }
            return subject;
        }

        public static string RenderEmailToString(ControllerContext context, object model, string templateView)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            viewEngineResult = ViewEngines.Engines.FindPartialView(context, "~/views/emails/" + templateView + ".cshtml");

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                            context.Controller.ViewData,
                                            context.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }

    }

    public class EmailRequest
    {
        public EmailRequest()
        {
            Params = new Dictionary<string, string>();
            Data = new Dictionary<string, string>();
            Template = "Basic";
        }

        public string To { set; get; }
        public string ToEmail { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
        public string Template { set; get; }
        public bool ShowDearTitle { set; get; }
        public Dictionary<string, string> Params { set; get; }
        public Dictionary<string, string> Data { set; get; }
    }

    /* This object will get setup when application starts */
    public class EmailInfo
    {
        public static SmtpClient SMTP = null;
        public static string FROM_EMAIL = null;
        public static string FROM_DIAPLAY = null;
    }
}