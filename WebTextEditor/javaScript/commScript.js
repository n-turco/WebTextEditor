//Nicholas Turco | student#: 9056530 | Assignment 5: JQUERY AND JSON BASED TEXT EDITOR | This an ASP.NET version of a web based text editor.
//This page contains the css and jQuery code used to manage communication between the client and server.

//used for AJAX calls
var jQueryXMLHttpRequest;

//when page loads in
$(document).ready(function () {

    //call server-side method to load file name
    PageMethods.GetTextFiles(loadFiles);

});

// ------------------------------------
// FILLS DROPDOWN WITH FILE NAME LIST
// ------------------------------------
function loadFiles(files) {

    //put dropdown element in a variable
    var select = $("#textFileList"); 

    //no files found
    if (files.length === 0) {
        document.getElementById("statusBar").textContent = "No files found.";
        return;
    }

    //add each .txt file name as an option
    $.each(files, function (i, fileName) {
        select.append(`<option value="${fileName}">${fileName}</option>`);
    });
}

// -----------------------------------
// WHEN USER CLICKS "OPEN FILE" BUTTON
// -----------------------------------
//$("#openFile").click(function openSelectedFile() {


//    document.getElementById("statusBar").textContent = "Loading...";

//    var fileName = $("#textFileList").val();

//    // Validate selection
//    if (fileName === "NoSelection") {
//        document.getElementById("statusBar").textContent = "Please select a file first.";
//        return;
//    }

//    $.ajax({
//        type: "POST",
//        url: "startPage.aspx/ReadFile",
//        data: JSON.stringify({ fileName: fileName }),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",

//        success: function (data) {

//            let response = data.d;
//            //status update
//            document.getElementById("statusBar").textContent =
//                "File Loaded: " + response.status;

//            //write text into text area
//            document.getElementById("notePad").value = response.description;

//            //update label
//            document.getElementById("labelFileName").textContent = fileName;
//        },

//        error: function () {
//            document.getElementById("statusBar").textContent = "Failed to load file.";
//        }
//    });
//});

function openSelectedFile() {
    document.getElementById("statusBar").textContent = "Loading...";

    var fileName = $("#textFileList").val();  // FIX HERE

    if (fileName === "NoSelection") {
        document.getElementById("statusBar").textContent = "Please select a file first.";
        return;
    }

    $.ajax({
        type: "POST",
        url: "startPage.aspx/ReadFile",
        data: JSON.stringify({ fileName: fileName }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            let response = data.d;

            document.getElementById("statusBar").textContent =
                "File Loaded: " + response.status;

            document.getElementById("notePad").value = response.description;

            document.getElementById("labelFileName").textContent = fileName;
        },

        error: function () {
            document.getElementById("statusBar").textContent = "Failed to load file.";
        }
    });
}
