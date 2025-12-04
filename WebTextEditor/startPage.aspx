<!--Nicholas Turco | student#: 9056530 | Assignment 5: JQUERY AND JSON BASED TEXT EDITOR -->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="startPage.aspx.cs" Inherits="WebTextEditor.startPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web Text Editor</title>

    <link rel="stylesheet" href="css/StyleSheet.css" />

    <!-- jQuery -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="javaScript/commScript.js"></script>
</head>

<body>
    <h1>A-05 | Text Editor</h1>

    <form id="textEditorForm" runat="server">
        <!--needed to enable page methods-->
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true" />

        <div class="navigationBar">
            <label class="description">
                Select a Text file:
            <select id="textFileList">
                <option value="NoSelection">Select file</option>
            </select>
            </label>
            <label class="description">
                Current file:
            <label id="labelFileName">Untitled</label>
            </label>
            <button id="openFile" type="button" class="navBtn" onclick="openSelectedFile()">Open File</button>
            <button id="SaveBtn" type="button" class="navBtn" onclick="saveSelectedFile()">Save</button>
            <button id="SaveAsBtn" type="button" class="navBtn" onclick="SaveAsNewFile()">Save As</button>
            <input type="text" id="newFileName" />

            <label id="statusBar">Welcome!</label>
        </div>

        <hr id="pageDivide" />

        <textarea id="notePad" placeholder="Enter your text here..."></textarea>

    </form>
</body>
</html>
