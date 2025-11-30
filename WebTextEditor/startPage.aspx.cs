using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTextEditor
{
    public partial class startPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod] //allows methods to be called from javaScript/AJAX
        public static string[] GetTextFiles()
        {
            //path to the folder containing text files
            string folderPath = HttpContext.Current.Server.MapPath("~/MyFiles/");

            //get all .txt files from folder
            string[] files = Directory.GetFiles(folderPath, "*.txt");

            //extract file names from path
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileName(files[i]);
            }

            return files;
        }

        [WebMethod]
        public static object ReadFile(string fileName)
        {
            string fileStatus;
            string folderPath;
            string fullPath;
            string fileContents;

            try
            {
                //build full path to selected file
                folderPath = HttpContext.Current.Server.MapPath("~/MyFiles/");
                fullPath = Path.Combine(folderPath, fileName);

                //check if file exists
                if (File.Exists(fullPath))
                {
                    fileStatus = "Success";
                    fileContents = File.ReadAllText(fullPath);
                }
                else
                {
                    fileStatus = "Failure";
                    fileContents = "File does not exist.";
                }
            }
            catch (Exception ex)
            {
                fileStatus = "Exception";
                fileContents = "ERROR: " + ex.Message;
            }
            var returnObject = new {
                status = fileStatus,
                description = fileContents
            };
            //return object and serialize in on ASP.NET end
            return returnObject;

        }

    }
}