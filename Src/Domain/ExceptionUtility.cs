using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace Biz
{
    public enum UserMessageType
    {
        Success, Error, warning, Info
        //** Never add new types in the middle
    }

    // Create our own utility for exceptions 
    public sealed class ExceptionUtility
    {
        // All methods are static, so this can be private 
        private ExceptionUtility()
        { }

        public static void RaiseItemNotFound(string entity)
        {
            throw new HttpException(500, entity + " not found!");
        }

        public static void RaiseBadRequest(string entity)
        {
            throw new HttpException(500, "We are sorry, the " + entity + " page you requested cannot be found. The URL may be misspelled or the page you're looking for is no longer available");
        }

        public static void RaiseSystemMessage(string message)
        {
            throw new HttpException(500, message);
        }

        public static void AddUserErrorMessage(Controller controller, string message)
        {
            if (controller != null && message != null)
            {
                controller.TempData["_Message"] = message;
                controller.TempData["_AddMessage"] = true;
                controller.TempData["_MessageType"] = UserMessageType.Error;
            }
        }

    }
}