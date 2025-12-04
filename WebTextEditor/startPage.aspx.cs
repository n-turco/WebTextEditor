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
        //Method Name: GetTextFiles
        //Description: This function builds the folder path to the specified directory
        //             It checks if the each file is a text file and adds them to the string array
        //             It extracts the file name from the file path and stores it back in the array
        //Parameters: none
        //Return: string[] files - array of files in the directory
        [WebMethod] //allows methods to be called from javaScript/AJAX
        public static string[] GetTextFiles()
        {
            //path to the folder containing text files
            string folderPath = HttpContext.Current.Server.MapPath("~/MyFiles/");
            //supported extensions
            string[] extensions = {"*.txt", "*.html", "*.htm", "*.csv", "*.xml", "*.log", "*.json", "*.js", "*.css", "*.doc", "*.docx" };

            //list to hold all readable files
            List<string> files = new List<string>();

            foreach (string ext in extensions)
            {
                //add all the files to the list
                files.AddRange(Directory.GetFiles(folderPath, ext));
            }

            //extract file names from path
            for (int i = 0; i < files.Count; i++)
            {
                files[i] = Path.GetFileName(files[i]);
            }
            //return list as an array
            return files.ToArray();
        }
        //Method Name: ReadFile
        //Description: This function builds the file path the the requested file
        //             It checks if the file exists, if it does it sets the status to success and saves the contents to a string
        //             If the file does not exist, it returns a message indicating so with a status of 404 Not found
        //             It puts the file status and file content in an object and returns it
        //Parameters: string fileName - the name of the file to be opened
        //Return: object - contains the status and content of the file
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
                    fileStatus = "200";
                    fileContents = File.ReadAllText(fullPath);
                }
                else
                {
                    fileStatus = "404 - Not Found";
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
        [WebMethod]
        public static object saveFile (string fileName, string textContent)
        {
            string fileStatus;
            string description;

            try
            {
                // Build full path
                string folderPath = HttpContext.Current.Server.MapPath("~/MyFiles/");
                string fullPath = Path.Combine(folderPath, fileName);

                // Save or overwrite file
                File.WriteAllText(fullPath, textContent);

                fileStatus = "200";
                description = "OK";
            }
            catch (Exception ex)
            {
                fileStatus = "Error";
                description = "ERROR: " + ex.Message;
            }

            var returnObject = new
            {
                status = fileStatus,
                description = description
            };
            //return object and serialize in on ASP.NET end
            return returnObject;
        }

        [WebMethod]
        public static object saveNewFile(string fileName, string textContent)
        {
            string fileStatus;
            string description;
            string extension;
            //validate file extension is there and supported
            extension = Path.GetExtension(fileName);
            if (extension == null || extension == string.Empty) 
            { 
                fileStatus = "Error";
                description = "Unspported or incomplete file extension.";
            } else
            {
                try
                {
                    string folderPath = HttpContext.Current.Server.MapPath("~/MyFiles/");
                    string fullPath = Path.Combine(folderPath, fileName);

                    File.WriteAllText(fullPath, textContent);

                    fileStatus = "200";
                    description = "OK";
                }
                catch (Exception ex)
                {
                    fileStatus = "500";
                    description = "Internal Server Error: " + ex.Message;
                }
            }
            var returnObject = new
            {
                status = fileStatus,
                description = description
            };
            return returnObject;
        }
    }
}