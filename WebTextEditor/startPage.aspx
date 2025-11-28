<!--Nicholas Turco | student#: 9056530 | Assignment 5: JQUERY AND JSON BASED TEXT EDITOR | This an ASP.NET version of a web based text editor.
    This page is the front facing client side UI. This is where all the page elements will be kept that the user/server can access. It contains all the
    html and ASP elements. -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="startPage.aspx.cs" Inherits="WebTextEditor.startPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web Text Editor</title>
    <!--Link to css file-->
    <link rel="stylesheet" href="css/StyleSheet.css" />
    <!--ajax library-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="javaScript/commScript.js"></script>
</head>
<body>
    <h1>A-05 | Text Editor</h1>
    <form id="textEditorForm" runat="server">
        <div>
            <div class="navigationBar">
                <label style="font-weight:bold;">Select a Text file:</label>
                <select id="textFileList">
                    <option value="NoSelection">Select file</option>
                </select>
                <label>Current file:</label>
                <label id="labelFileName">Untitled</label>
                <button id="SaveBtn">Save</button>
                <button id="SaveAsBtn">SaveAs</button>
                <hr id="pageDivide"/>
                <div>
                    &nbsp;<textarea id="notePad" rows="1" cols="20" placeholder="Enter your text here..." name="S1">

                    </textarea></div>
</div>
        </div>
    </form>
</body>
</html>
