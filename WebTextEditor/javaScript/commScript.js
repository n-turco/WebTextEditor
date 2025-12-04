//Nicholas Turco | student#: 9056530 | Assignment 5: JQUERY AND JSON BASED TEXT EDITOR | This an ASP.NET version of a web based text editor.
//This page contains the css and jQuery code used to manage communication between the client and server.

//used for AJAX calls
var jQueryXMLHttpRequest;

//when page loads in
$(document).ready(function () {

    //call server-side method to load file name
    PageMethods.GetTextFiles(loadFiles);

});

//Method Name: loadFiles
//Description: This function loads all files that are human readable format.
//             It stores the names of the files in a dynamic dropdown list
//Parameters: string files - an array of file names
//Return: void
function loadFiles(files) {

    //put dropdown element in a variable
    var select = $("#textFileList"); 

    //no files found
    if (files.length === 0) {
        document.getElementById("statusBar").innerText = "No files found.";
        return;
    }

    //add each .txt file name as an option
    $.each(files, function (i, fileName) {
        select.append(`<option value="${fileName}">${fileName}</option>`);
    });
}

//Method Name: openSelectedFile
//Description: This function executes with an onClick event (openFile button)
//             It gets the value of the selected file name in the dropdown list
//             Sends an AJAX POST request in JSON format.
//             It takes the response from the server and loads it into the textArea
//Parameters: none
//Return: void
function openSelectedFile() {
    document.getElementById("statusBar").innerText = "Loading...";
    //get the selected file name
    var fileName = $("#textFileList").val();  
    //check if anything is not selected
    if (fileName === "NoSelection") {
        document.getElementById("statusBar").innerText = "Please select a file first.";
        return;
    }
    //send AJAX request
    $.ajax({
        type: "POST",
        url: "startPage.aspx/ReadFile",
        //convert data to JSON
        data: JSON.stringify({ fileName: fileName }),
        contentType: "application/json; charset=utf-8",
        //expect JSON response from server
        dataType: "json",
        
        success: function (data) {
            //put the content of data.d in a variable
            let response = data.d;         
            document.getElementById("statusBar").innerText =
                "File Loaded: " + response.status;
            //put the content in the text box
            document.getElementById("notePad").value = response.description;
            
            document.getElementById("labelFileName").innerText = fileName;
        },
        //display error if unable to load file
        error: function (data) {
            let response = data.d;
            document.getElementById("statusBar").innerText = "Failed to load file." + response.description;
        }
    });
}
// Method Name: saveSelectedFile
// Description: This function executes with an onClick event (saveFile button)
//              Retrieves the active file name from the label and the text content from the textarea.
//              Sends both values to the server using an AJAX POST request in JSON format.
//              Updates the status bar based on the server's response.
// Parameters: None
// Returns: void
function saveSelectedFile() {

    var filename = document.getElementById("labelFileName").innerText;
    if (!filename || filename.trim() === "") {
        document.getElementById("statusBar").innerText = "No file selected.";
        return;
    }
    var fileContent = document.getElementById("notePad").value;
    $.ajax({
        type: "POST",
        url: "startPage.aspx/saveFile",
        data: JSON.stringify({ fileName: filename, textContent: fileContent }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            let response = data.d;
            document.getElementById("statusBar").innerText = "File successfully saved." + response.description;
        },
        error: function (data) {
            let response = data.d;
            document.getElementById("statusBar").innerText = "Failed to save file." + response.description;
        }
    });
}
function SaveAsNewFile() {
    var newFileName = document.getElementById("newFileName").value;
    if (!newFileName || newFileName.trim() === "") {
        document.getElementById("statusBar").innerText = "File name cannot be blank.";
        return;
    }
    
    var fileContent = document.getElementById("notePad").value;
    $.ajax({
        type: "POST",
        url: "startPage.aspx/saveNewFile",
        data: JSON.stringify({ fileName: newFileName, textContent: fileContent }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            let response = data.d;
            document.getElementById("labelFileName").innerText = newFileName;
            document.getElementById("statusBar").innerText = "File successfully saved." + response.description;
        },
        error: function (data) {
            let response = data.d;
            document.getElementById("statusBar").innerText = "Failed to save file." + response.description;
        }
    });
}