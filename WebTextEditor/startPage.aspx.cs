//Nicholas Turco | student#: 9056530 | Assignment 5: JQUERY AND JSON BASED TEXT EDITOR | This an ASP.NET version of a web based text editor.
//This page contains the C# code on the server side that handles file management, it receives a JSON request from the client and performs the 
//requested action. It will save the data to an existing file, a new file or send an existing file in JSON format back to the server.
using FileSignatures;
using FileSignatures.Formats;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTextEditor
{
    public partial class StartPage : System.Web.UI.Page
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
            string[] allowedExtensions = {"*.txt", "*.html", "*.htm", "*.csv", "*.xml", "*.log", "*.json", "*.js", "*.css" };


            //list to hold all readable files
            List<string> files = new List<string>();

            foreach (string ext in allowedExtensions)
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
        //Description: This function builds the file path the requested file
        //             It checks if the file exists, if it does it sets the status to success and saves the contents to a string
        //             If the file does not exist, it returns a message notifying the user
        //Parameters: string fileName - the name of the file to be opened
        //Return: Serialized object - contains the status and content of the file
        [WebMethod]
        public static string ReadFile(string fileName)
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
                    if (!CheckFileSignature(fullPath))
                    {
                        fileStatus = "Failed";
                        fileContents = "File is not a valid text file and cannot be opened.";
                    }
                    else
                    {
                        fileStatus = "Success";
                        fileContents = File.ReadAllText(fullPath);
                    }
                }
                else
                {
                    fileStatus = "Failed";
                    fileContents = "File not Found";
                }
            }
            catch
            {
                fileStatus = "Error";
                fileContents = "Internal Server Error";
            }
            //return serializd object as a JSON string
            return JsonConvert.SerializeObject(new { status = fileStatus, description = fileContents });
        }

        //Method Name: saveFile
        //Description: This function builds the file path to where the file will be saved
        //             It writes the contents to the file, if it does not exist it creates a new file            
        //Parameters: string fileName - the name of the file to be opened
        //Return: Serialized object - contains the status and content of the file
        [WebMethod]
        public static string SaveFile (string fileName, string textContent)
        {
            string fileStatus;
            string description;

            try
            {
                // Build full path
                string folderPath = HttpContext.Current.Server.MapPath("~/MyFiles/");
                string fullPath = Path.Combine(folderPath, fileName);

                // Save to existing file or create a new one and save it
                File.WriteAllText(fullPath, textContent);

                fileStatus = "Success";
                description = "File Saved.";
            }
            catch (Exception ex)
            {
                fileStatus = "Exception";
                description = "Internal Server Error: " + ex.Message;
            }
            //return serializd object as a JSON string
            return JsonConvert.SerializeObject(new { status = fileStatus, description });
        }
        //Method Name: saveNewFile
        //Description: This function builds the file path to where the file will be saved
        //             It writes the contents to the file, if it does not exist it creates a new file            
        //Parameters: string fileName - the name of the file to be opened
        //Return: Serialized object - contains the status and content of the file
        [WebMethod]
        public static string SaveNewFile(string fileName, string textContent)
        {
            string fileStatus;
            string description;
            string extension;
            //validate file extension is there and supported
            extension = Path.GetExtension(fileName);
            if (extension == null || extension == string.Empty) 
            {
                //set default file extension if user does not specify
                fileName += ".txt";
            }          
                try
                {
                    //save new file to directory path
                    string folderPath = HttpContext.Current.Server.MapPath("~/MyFiles/");
                    string fullPath = Path.Combine(folderPath, fileName);

                    File.WriteAllText(fullPath, textContent);

                    fileStatus = "Success";
                    description = "File saved.";
                }
                catch (Exception ex)
                {
                    fileStatus = "Exception";
                    description = "Internal Server Error: " + ex.Message;
                }
            //return serializd object as a JSON string
            return JsonConvert.SerializeObject(new { status = fileStatus, description });
        }
        /// <summary>
        /// This method checks the file signature of the specified file to determine if it is a valid text file or not. 
        /// It uses the FileSignatures library to inspect the file format. Returns true if the file is a valid text file, false otherwise.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool CheckFileSignature(string fileName) 
        {

            FileFormatInspector inspector = new FileFormatInspector();
            using (var stream = File.OpenRead(fileName))
            {
                var format = inspector.DetermineFileFormat(stream);
                //if the format is not null, then it is a valid file type, this means that the file is not a text file and should not be opened in the editor
                if (format != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}